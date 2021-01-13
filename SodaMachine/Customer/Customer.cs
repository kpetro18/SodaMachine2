using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet Wallet;
        public Backpack Backpack;

        //Constructor (Spawner)
        public Customer()
        {
            Wallet = new Wallet();
            Backpack = new Backpack();
        }
        //Member Methods (Can Do)

        //This method will be the main logic for a customer to retrieve coins form their wallet.
        //Takes in the selected can for price reference
        //Will need to get user input for coins they would like to add.
        //When all is said and done this method will return a list of coin objects that the customer will use a payment for their soda.
        public List<Coin> GatherCoinsFromWallet(Can selectedCan)
        {
            List<Coin> coinsForSoda = new List<Coin>();
            double gatheredCoinsValue = 0;
            string selectedCoin;

            while (gatheredCoinsValue < selectedCan.Price)
            {
                selectedCoin = UserInterface.CoinSelection(selectedCan, coinsForSoda);

                if (selectedCoin == "Quarter")
                {
                    Coin quarter = GetCoinFromWallet("Quarter");
                    coinsForSoda.Add(quarter);
                    gatheredCoinsValue += quarter.Value;
                }
                else if (selectedCoin == "Dime")
                {
                    Coin dime = GetCoinFromWallet("Dime");
                    coinsForSoda.Add(dime);
                    gatheredCoinsValue += dime.Value;
                }
                else if (selectedCoin == "Nickel")
                {
                    Coin nickel = GetCoinFromWallet("Nickel");
                    coinsForSoda.Add(nickel);
                    gatheredCoinsValue += nickel.Value;
                }
                else if (selectedCoin == "Penny")
                {
                    Coin penny = GetCoinFromWallet("Penny");
                    coinsForSoda.Add(penny);
                    gatheredCoinsValue += penny.Value;
                }
                else coinsForSoda = null;
            }
            return coinsForSoda;
        }

        //Returns a coin object from the wallet based on the name passed into it.
        //Returns null if no coin can be found
        public Coin GetCoinFromWallet(string coinName)
        {
            for (int i = 0; i < Wallet.Coins.Count; i++)
            {
                if (coinName == Wallet.Coins[i].Name)
                {
                    Wallet.Coins.Remove(Wallet.Coins[i]);
                    return Wallet.Coins[i];
                }
            }
            return null;
        }
        //Takes in a list of coin objects to add into the customers wallet.
        public void AddCoinsIntoWallet(List<Coin> coinsToAdd)
        {
            for (int i = 0; i < coinsToAdd.Count; i++)
            {
                Wallet.Coins.Add(coinsToAdd[i]);
            }
        }
        //Takes in a can object to add to the customers backpack.
        public void AddCanToBackpack(Can purchasedCan)
        {
            Backpack.cans.Add(purchasedCan);
        }
    }
}
