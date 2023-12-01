using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EpicArenaBattle.Hero;

namespace EpicArenaBattle
{
    internal class Arena
    {
        int NumberOfHeroes;
        private List<Hero> HeroList;
        private List<Hero> DeadHeroList;
        private Hero Attacker;
        private Hero Defender;

        //initializes the arena, generates the heroes
        public Arena(int numOfHeroes)
        {
            NumberOfHeroes = numOfHeroes;
            HeroList = new List<Hero>();
            DeadHeroList = new List<Hero>();
            AddHeros(NumberOfHeroes);
        }

        //Adds random heroes to the hero list
        public void AddHeros(int NumberOfHeroes) 
        {
            for (int i = 0; i<NumberOfHeroes; i++)
            {
                Array values = Enum.GetValues(typeof(Hero.eHeroType));
                Random random = new Random();
                eHeroType type = (eHeroType)values.GetValue(random.Next(values.Length));

                HeroList.Add(new Hero(type, i));
            }
        }

        //Writes every hero to the console
        public void WriteAllHeroes()
        {
            foreach (var hero in HeroList.OrderBy(h => h.Id).ToList())
            {
                WriteHero(hero);
            }
        }

        //Writes one hero to the console
        public void WriteHero(Hero hero)
        {
            string HeroDescription = "";
            HeroDescription += "Id: " + hero.Id.ToString() + " ";
            HeroDescription += "Type: " + hero.Type.ToString() + " ";
            HeroDescription += "Max Hp: " + hero.MaxHp.ToString() + " ";
            HeroDescription += "Current Hp: " + hero.Hp.ToString() + " ";
            HeroDescription += "Is alive: " + hero.IsAlive.ToString() + " ";
            General.LogEvents(HeroDescription);
        }

        /// <summary>
        /// Simulates the fight
        /// Shuffles the hero list, then picks the first two for the fight
        /// Does the rest to the others
        /// Writes to the console if any of the heroes die
        /// After fight writes every heroes to the console 
        /// </summary>
        public void DoAllFights()
        {
            int roundCounter = 1;
            while (HeroList.Count(item => item.IsAlive = true) > 1)
            {
                General.LogEvents("Round " + roundCounter.ToString());
                General.LogEvents("Live Heroes in arena:");
                WriteAllHeroes();
                SelectHeroesForFight();
                DoRestForOthers();

                General.LogEvents("Fight! Attacker: " + Attacker.Id.ToString() + " Defender: " + Defender.Id.ToString());
                
                Attacker.Fight(Defender);

                if (Attacker.IsAlive == false)
                {
                    HeroList.Remove(Attacker);
                    DeadHeroList.Add(Attacker);
                    General.LogEvents("Attacker died (Id:" + Attacker.Id.ToString() + ").");
                }
                if (Defender.IsAlive == false)
                {
                    HeroList.Remove(Defender);
                    DeadHeroList.Add(Defender);
                    General.LogEvents("Defender died (Id:" + Defender.Id.ToString() + ").");
                }
                General.LogEvents("Attacker:");
                WriteHero(Attacker);
                General.LogEvents("Defender:");
                WriteHero(Defender);
                General.LogEvents("\n");
                roundCounter++;
            }
            DeclareWinner();
        }
        //Announces the winner if there is one
        public void DeclareWinner()
        {
            if(HeroList.Count == 1)
            {
                General.LogEvents("Winner: ");
                WriteHero(HeroList[0]);
            }
            if (HeroList.Count == 0)
            {
                General.LogEvents("There is no winner. Every hero died :(");
            }
        }
        //Picks the 2 heroes to fight
        public void SelectHeroesForFight() 
        {
            ShuffleList();
            Attacker = HeroList[0];
            Defender = HeroList[1];
        }

        //Does the 10 HP heal for the rested heroes
        public void DoRestForOthers()
        {
            foreach (var hero in HeroList)
            {
                if (!(hero == Attacker || hero == Defender))
                {
                    hero.Rest();
                }
            }
        }

        //randomizes the hero list 
        public void ShuffleList()
        {
            Random rand = new Random();
            HeroList = HeroList.OrderBy(_ => rand.Next()).ToList();
        }
    }
}
