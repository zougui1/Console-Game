using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.stats;
using ConsoleGame.game;
using ConsoleGame.items;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.json;
using ConsoleGame.misc;
using ConsoleGame.utils;

namespace ConsoleGame.entity
{
    public class Monster : Entity
    {
        /// <summary>
        /// Loots is an array of ValueTuple and represent the items the monster can loot
        /// - Percent is the percent of chance to drop the item
        /// - Id is the id of the item
        /// - DataPath is the path of the json where the item is
        /// </summary>
        public LootTable[] LootsTable { get; set; }
        public int Gold { get; set; } = 0;
        public List<Item> Items { get; set; }

        public Monster(string name) : base(name)
        { }

        [JsonConstructor]
        public Monster(
            string name, EntityStats entityStats, Weapon weapon, List<Spell> spells,
            Shield shield, Armor head, Armor torso, Armor arms, Armor legs, Armor feet
        ) : base(name, entityStats, weapon, spells, shield, head, torso, arms, legs, feet)
        { }

        public override void ChooseAction()
        {
            DoAction();
        }

        // normal attack = non-magic and non-item attack 
        /* 
         * start monster AI
         * one-vs-one: quite good, eventually improve it later
         * ove-vs-many: good enough, eventually improve it later
         * many-vs-many: not so good, to improve
         */
        public void DoAction()
        {
            User user = GameMenu.Game.User;

            // if there's one character alive
            if(user.CharactersAliveCount() == 1)
            {
                ActionForOneCharacter();
            }
            else
            {
                ActionForManyCharacters();
            }
        }

        private void ActionForOneCharacter()
        {
            User user = GameMenu.Game.User;
            Character character = user.FirstCharacterAlive();
            double characterDamages = character.GetDamages(this); // get the character normal attack damages

            // if the monster attack deal enough damages to kill the character
            if (GetDamages(character) >= character.EntityStats.Health)
            {
                Attack(character); // normal attack 
            }
            // if the character attack deal enough damages to one shot the monster
            else if (characterDamages >= EntityStats.MaxHealth)
            {
                Attack(character); // TODO: strongest attack
            }
            // if the character attack deal enough damages to kill the monster
            else if ((characterDamages * 1.5) >= EntityStats.Health)
            {
                // if the monster has any heal item
                if (Items.Any(item => item.IsHeal))
                {
                    ConsumeHeal(FindHealToConsume()); // the monster use the heal to regen himself
                }
                else
                {
                    Attack(character); // TODO: strongest attack
                }
            }
            else
            {
                Attack(character); // TODO: random between: random attack; defend
            }
        }

        private void ActionForManyCharacters()
        {
            User user = GameMenu.Game.User;
            List<Character> characters = user.AllCharactersAlive();
            List<Monster> monsters = user.AllMonstersAlive();
            List<(int cIndex, int mIndex)> characterKillMonsterInfos = new List<(int cIndex, int mIndex)>();
            List<(int cIndex, int mIndex)> characterOneshotMonsterInfos = new List<(int cIndex, int mIndex)>();
            List<(int cIndex, int mIndex)> monsterKillCharacterInfos = new List<(int cIndex, int mIndex)>();
            List<(string listName, int index, int agility)> battleOrder = new List<(string listName, int index, int agility)>();
            int i = 0;

            characters.ForEach(c => battleOrder.Add((listName: "characters", index: i++, agility: (int)c.EntityStats.Agility)));
            i = 0;
            monsters.ForEach(m => battleOrder.Add((listName: "monsters", index: i++, agility: (int)m.EntityStats.Agility)));
            battleOrder = battleOrder.OrderByDescending(e => e.agility).ToList();

            for (int cIndex = 0; cIndex < characters.Count; cIndex++)
            {
                Character character = characters[cIndex];

                for(int mIndex = 0; mIndex < monsters.Count; mIndex++)
                {
                    Monster monster = monsters[mIndex];
                    double characterDamages = character.GetDamages(this);

                    if(characterDamages >= monster.EntityStats.MaxHealth)
                    {
                        characterOneshotMonsterInfos.Add((cIndex, mIndex));
                    }
                    else if((characterDamages * 1.5) >= monster.EntityStats.Health)
                    {
                        characterKillMonsterInfos.Add((cIndex, mIndex));
                    }
                    else if (monster.GetDamages(character) >= character.EntityStats.Health)
                    {
                        monsterKillCharacterInfos.Add((cIndex, mIndex));
                    }
                }
            }

            if(monsters.Count == 1)
            {
                ActionWithOneMonster(
                    characters,
                    characterKillMonsterInfos,
                    characterOneshotMonsterInfos,
                    monsterKillCharacterInfos
                );
            }
            else
            {
                ActionWithManyMonsters(
                    characters,
                    monsters,
                    characterKillMonsterInfos,
                    characterOneshotMonsterInfos,
                    monsterKillCharacterInfos,
                    battleOrder
                );
            }
        }

        private void ActionWithOneMonster(
            List<Character> characters,
            List<(int cIndex, int mIndex)> characterKillMonsterInfos,
            List<(int cIndex, int mIndex)> characterOneshotMonsterInfos,
            List<(int cIndex, int mIndex)> monsterKillCharacterInfos
        )
        {
            bool actionDone = false;

            // if the monster has no chance to live he will do its best to kill a character
            if(characterOneshotMonsterInfos.Count >= 1)
            {
                // try to kill a character that will oneshot him
                characterOneshotMonsterInfos.ForEach(info =>
                {
                    Character character = characters[info.cIndex];

                    if (GetDamages(character) >= character.EntityStats.Health)
                    {
                        Attack(character); // normal attack
                        actionDone = true;
                    }
                });

                if (!actionDone)
                {
                    // try to kill a monster that will kill him
                    characterKillMonsterInfos.ForEach(info =>
                    {
                        Character character = characters[info.cIndex];

                        if (GetDamages(character) >= character.EntityStats.Health)
                        {
                            Attack(character); // normal attack
                            actionDone = true;
                        }
                    });
                }

                if (!actionDone)
                {
                    // if there's only one character that can oneshot him, he will try to kill him
                    if(characterOneshotMonsterInfos.Count == 1)
                    {
                        Character oneshoter = characters[characterOneshotMonsterInfos[0].cIndex];
                        // if 150% of the normal damages is enough to kill him, then he will try to kill him with his strongest attack
                        if ((GetDamages(oneshoter) * 1.5) >= oneshoter.EntityStats.Health)
                        {
                            Attack(oneshoter); // TODO: strongest attack
                            actionDone = true;
                        }
                    }

                    if (!actionDone)
                    {
                        // attack the lowest character in health with his strongest attack
                        List<Character> charactersOrderedByLowestHealth = characters.OrderByDescending(character => character.EntityStats.Health).ToList();
                        Attack(charactersOrderedByLowestHealth[0]); // TODO: strongest attack
                        actionDone = true;
                    }
                }
            }

            // if the monster can be killed, he will try to heal himself or try to kill on of those who can kill him if he can't heal himself
            if(characterKillMonsterInfos.Count >=1)
            {
                // if the monster has any heal item
                if (Items.Any(item => item.IsHeal))
                {
                    ConsumeHeal(FindHealToConsume()); // the monster use the heal to regen himself
                }
                else
                {
                    if (!actionDone)
                    {
                        // try to kill a character that can kill him
                        characterKillMonsterInfos.ForEach(info =>
                        {
                            Character character = characters[info.cIndex];

                            if (GetDamages(character) >= character.EntityStats.Health)
                            {
                                Attack(character); // normal attack
                                actionDone = true;
                            }
                        });
                    }

                    if (!actionDone)
                    {
                        // attack the lowest character in heal with his strongest attack
                        List<Character> charactersOrderedByLowestHealth = characters.OrderBy(character => character.EntityStats.Health).ToList();
                        Attack(charactersOrderedByLowestHealth[0]); // TODO: strongest attack
                        actionDone = true;
                    }
                }
            }

            // if the monster can kill a character then he will kill the character that can deal the best amount of damages
            if (!actionDone && monsterKillCharacterInfos.Count >= 1)
            {
                List<Character> killableCharacters = new List<Character>();
                monsterKillCharacterInfos.ForEach(info => killableCharacters.Add(characters[info.cIndex]));

                // order by the most strong character to the less strong one, to attack the strongest one
                killableCharacters = GetMostDangerousCharacters(killableCharacters);

                Attack(killableCharacters[0]); // TODO: strongest attack
                actionDone = true;
            }

            if (!actionDone)
            {
                // order by the most strong character to the less strong one, to attack the strongest one
                List<Character> strongestCharacters = GetMostDangerousCharacters(characters);
                Attack(strongestCharacters[0]); // TODO: random between: random attack; defend
                actionDone = true;
            }
        }

        private void ActionWithManyMonsters(
            List<Character> characters,
            List<Monster> monsters,
            List<(int cIndex, int mIndex)> characterKillMonsterInfos,
            List<(int cIndex, int mIndex)> characterOneshotMonsterInfos,
            List<(int cIndex, int mIndex)> monsterKillCharacterInfos,
            List<(string listName, int index, int agility)> battleOrder
        )
        {
            IEnumerable<IGrouping<int, (int cIndex, int mIndex)>> groupedCharacterKillMonsterInfos = characterKillMonsterInfos.GroupBy(info => info.mIndex);
            IEnumerable<IGrouping<int, (int cIndex, int mIndex)>> groupedCharacterOneshotMonsterInfos = characterOneshotMonsterInfos.GroupBy(info => info.mIndex);
            IEnumerable<IGrouping<int, (int cIndex, int mIndex)>> groupedMonsterKillCharacterInfos = monsterKillCharacterInfos.GroupBy(info => info.cIndex);
            bool actionDone = false;

            // if there is only one character that can oneshot a monster
            // then check if all the monsters can kill him before he can attack
            if (characterOneshotMonsterInfos.Count == 1)
            {
                Character target = characters[characterOneshotMonsterInfos[0].cIndex];
                bool canKillTheKiller = CanKillACharacterWithManyMonsters(
                    monsters,
                    characters,
                    target,
                    groupedCharacterKillMonsterInfos,
                    battleOrder
                );

                if (canKillTheKiller)
                {
                    Attack(target); // TODO: strongest attack
                    actionDone = true;
                }
            }
            else if(characterOneshotMonsterInfos.Count > 1)
            {
                List<Character> charactersLowestHealth = characters.OrderBy(c => c.EntityStats.Health).ToList();
                Attack(charactersLowestHealth[0]); // TODO: strongest attack
                actionDone = true;
            }

            if (!actionDone && characterKillMonsterInfos.Count == 1)
            {
                Character target = characters[characterKillMonsterInfos[0].cIndex];
                bool canKillTheKiller = CanKillACharacterWithManyMonsters(
                    monsters,
                    characters,
                    target,
                    groupedCharacterKillMonsterInfos,
                    battleOrder
                );

                // if the monster has any heal item, he heal himself
                if (Items.Any(item => item.IsHeal))
                {
                    ConsumeHeal(FindHealToConsume()); // the monster use the heal to regen himself
                }
                // otherwise see if he can kill the killer with other monsters help
                else if (canKillTheKiller)
                {
                    Attack(target); // TODO: strongest attack
                    actionDone = true;
                }
            }

            if(!actionDone && monsterKillCharacterInfos.Count >= 1)
            {
                foreach(IGrouping<int, (int cIndex, int mIndex)> monster in groupedMonsterKillCharacterInfos)
                {
                    if(monsters[monster.Key] == this)
                    {
                        Attack(characters[monster.ElementAt(0).cIndex]); // normal attack
                    }
                }
            }
        }

        private bool CanKillACharacterWithManyMonsters(
            List<Monster> monsters,
            List<Character> characters,
            Character target,
            IEnumerable<IGrouping<int, (int cIndex, int mIndex)>> groupedCharacterKillMonsterInfos,
            List<(string listName, int index, int agility)> battleOrder
        )
        {
            int orderIndex = 0;
            double totalDamages = 0;

            // as long as the monsters can do an action before the target
            while (orderIndex < battleOrder.FindIndex(c => c.listName == "characters" && characters[c.index] == target))
            {
                // the current entity
                (string listName, int index, int agility) = battleOrder[orderIndex];

                if (listName == "monsters")
                {
                    // if there is no character that can kill the current monster, we add its damages to the total damages
                    if (!groupedCharacterKillMonsterInfos.Any(group => group.Key == index))
                        totalDamages += monsters[index].GetDamages(target);
                }
                orderIndex++;
            }

            // we check if 150% of the total damages is enough to kill the target
            // if so, then the monster attack him with its strongest attack
            if ((totalDamages * 1.5) >= target.EntityStats.Health)
            {
                return true;
            }

            return false;
        }

        private List<Character> GetMostDangerousCharacters(List<Character> characters)
        {
            return characters.OrderByDescending(character =>
            {
                (double magicalMight, double strength) = character.EntityStats;

                // some classes has spells but have more strength than magical might
                return (character.HasSpells && magicalMight >= strength)
                    ? magicalMight
                    : strength;
            }).ToList();
        }

        public Item FindHealToConsume()
        {
            int maxHealth = (int)EntityStats.MaxHealth;
            //                              get all items that can heal      order by ASC depending on how much health it can restore
            List<Item> healsEffectiveness = Items.FindAll(item => item.IsHeal).OrderBy(item => item.Restore).ToList();
            Item heal = null;

            if(healsEffectiveness.Count == 1)
            {
                return healsEffectiveness[0];
            }

            // return the item where true is returned, if false is always returned, it returns null
            heal = healsEffectiveness.FirstOrDefault(item =>
            {
                // get the health of the monster once it gets healed by the current heal
                int afterHeal = (int)EntityStats.Health + item.Restore;

                if(afterHeal == maxHealth)
                {
                    return true;
                }
                else if (afterHeal <= maxHealth)
                {
                    // if after the heal the monster is at 60% of his max health, he use it
                    if(afterHeal >= (maxHealth * 0.6))
                    {
                        return true;
                    }
                }
                // if heal is not too powerfull, he use it
                else if(afterHeal <= (maxHealth * 1.2))
                {
                    return true;
                }

                return false;
            });

            if(heal == null)
            {
                heal = healsEffectiveness[0];
            }

            return heal;
        }

        public void ConsumeHeal(Item item)
        {
            Regen(item.Restore);
            Items.Remove(item);
            Console.WriteLine("{0} consume {1}", Name, item.Name);
            Utils.Cconsole.Color("DarkGreen").WriteLine("{0} has now {1} health points.", Name, EntityStats.Health);
        }

        public void ConsumeHeal(Monster monster, Item item)
        {
            monster.Regen(item.Restore);
            Items.Remove(item);
            Console.WriteLine("{0} consume {1}", monster.Name, item.Name);
            Utils.Cconsole.Color("DarkGreen").WriteLine("{0} has now {1} health points.", monster.Name, monster.EntityStats.Health);
        }

        public List<Item> Loots()
        {
            List<Item> drops = new List<Item>();
            
            for(int i = 0; i < LootsTable.Length; ++i)
            {
                (double percent, int id, string dataType) = LootsTable[i];
                double random = RandomNumber.Between(0, 100) + new Random().NextDouble();
                
                if(random > 100)
                {
                    random = 100;
                }
                
                if(random <= percent)
                {
                    Item item = Json.GetRightItem(id, dataType);
                    drops.Add(item);
                    Utils.Cconsole.Color("Blue").WriteLine("{0} dropped a {1}", Name, item.Name);
                }
            }

            return drops;
        }
    }
}
