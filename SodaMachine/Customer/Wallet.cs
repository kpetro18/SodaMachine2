using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> Coins;
        int startingQuarters = 50;
        int startingDimes = 30;
        int startingNickels = 20;
        int startingPennies = 50;

        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            FillWallet();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillWallet()
        {
            AddQuarters();
            AddDimes();
            AddNickels();
            AddPennies();
        }

        private void AddQuarters()
        {
            for (int i = 0; i < startingQuarters; i++)
            {
                Quarter quarter = new Quarter();
                Coins.Add(quarter);
            }
        }

        private void AddDimes()
        {
            for (int i = 0; i < startingDimes; i++)
            {
            Dime dime = new Dime();
            Coins.Add(dime);
            }
        }

        private void AddNickels()
        {
            for (int i = 0; i < startingDimes; i++)
            {
                Nickel nickel = new Nickel();
                Coins.Add(nickel);
            }
        }

        private void AddPennies()
        {
            for (int i = 0; i < startingPennies; i++)
            {
                Penny penny = new Penny();
                Coins.Add(penny);
            }
        }


    }
}
