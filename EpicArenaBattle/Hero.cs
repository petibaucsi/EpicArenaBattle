using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicArenaBattle
{
    internal class Hero
    {
        //The three hero types
        public enum eHeroType{
            eArcher,
            eKnight,
            eFootman
        }

        public int Id {get; set;}
        public double Hp { get; set; }
        public eHeroType Type { get; set; }
        public double MaxHp {  get; set; }

        private bool isAlive;
        public bool IsAlive
        {
            get { return isAlive; }
            set
            {
                isAlive = value;
                if (value == false)
                {
                    Hp = 0;
                }
            }
        }

        //Creates a hero with properties depending on its type
        public Hero(eHeroType heroType, int id) 
        {
            Type = heroType;
            Id= id;
            switch (Type)
            {
                case eHeroType.eArcher:
                    Hp = 100;
                    MaxHp = 100;
                    break;
                case eHeroType.eKnight:
                    Hp = 150;
                    MaxHp = 150;
                    break;
                case eHeroType.eFootman:
                    Hp = 120;
                    MaxHp = 120;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Does the fight between the two selected heroes
        /// After the figth the living heroes (if any) take their half HP damage
        /// </summary>
        /// <param name="enemy"></param>
        public void Fight(Hero enemy)
        {
            if (!this.IsAlive) return;

            if(this.Type == eHeroType.eArcher)
            {
                if (enemy.Type == eHeroType.eKnight) 
                {
                    int i = General.GenerateRandom();
                    enemy.IsAlive = i <= 6;
                    General.LogEvents("The random value is " + i.ToString() + "(1-6 Knight survives, 7-10 knight dies)");
                }
                if (enemy.Type == eHeroType.eFootman) { enemy.IsAlive = false; }
                if (enemy.Type == eHeroType.eArcher) { enemy.IsAlive = false; }
            }
            if (this.Type == eHeroType.eFootman)
            {
                if (enemy.Type == eHeroType.eKnight) {  }
                if (enemy.Type == eHeroType.eFootman) { enemy.IsAlive = false; }
                if (enemy.Type == eHeroType.eArcher) { enemy.IsAlive = false; }
            }
            if (this.Type == eHeroType.eKnight)
            {
                if (enemy.Type == eHeroType.eKnight) { enemy.IsAlive = false; }
                if (enemy.Type == eHeroType.eFootman) { this.IsAlive = false; }
                if (enemy.Type == eHeroType.eArcher) { enemy.IsAlive = false; }
            }
            DemageDealt(this);
            DemageDealt(enemy);
        }

        //Heals the Hero 10 HP, but can't go over the initial HP
        public void Rest()
        {
            Hp += 10;
            if (Hp > MaxHp) {  Hp = MaxHp; }
        }

        //Does the damage to the hero. If HP is below 1/4 of initial HP, the hero dies
        private void DemageDealt(Hero hero)
        {
            hero.Hp = hero.Hp / 2;
            if (hero.Hp < hero.MaxHp/4) { hero.IsAlive = false; }
        }
    }

}
