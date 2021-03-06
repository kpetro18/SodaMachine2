﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;
        int startingQuarters = 20;
        int startingDimes = 10;
        int startingNickels = 20;
        int startingPennies = 50;
        int startingRootBeer = 5;
        int startingCola = 3;
        int startingOrangeSoda = 0;

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
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
            _register.Add(quarter);
            }
        }

        private void AddDimes()
        {
            for (int i = 0; i < startingDimes; i++)
            {
                Dime dime = new Dime();
                _register.Add(dime);
            }
        }

        private void AddNickels()
        {
            for (int i = 0; i < startingNickels; i++)
            {
                Nickel nickel = new Nickel();
                _register.Add(nickel);
            }
        }

        private void AddPennies()
        {
            for (int i = 0; i < startingPennies; i++)
            {
                Penny penny = new Penny();
                _register.Add(penny);
            }
        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {
            AddRootBeer();
            AddCola();
            AddOrangeSoda();
        }

        private void AddRootBeer()
        {
            for (int i = 0; i < startingRootBeer; i++)
            {
                RootBeer rootBeer = new RootBeer();
                _inventory.Add(rootBeer);
            }
        }

        private void AddCola()
        {
            for (int i = 0; i < startingCola; i++)
            {
            Cola cola = new Cola();
            _inventory.Add(cola);
            }
        }

        private void AddOrangeSoda()
        {
            for (int i = 0; i < startingOrangeSoda; i++)
            {
                OrangeSoda orangeSoda = new OrangeSoda();
                _inventory.Add(orangeSoda);
            }
        }


        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            string chosenSoda = UserInterface.SodaSelection(_inventory);
            Can chosenCan = GetSodaFromInventory(chosenSoda);
            List<Coin> payment = customer.GatherCoinsFromWallet(chosenCan);
            CalculateTransaction(payment, chosenCan, customer);
           
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda)
        {
            foreach (Can can in _inventory)
            {
                if (can.Name == nameOfSoda)
                {
                    return can;
                }
                else
                {
                    Console.WriteLine("Soda is out, please select another.");
                }
            }
            return null;
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Dispense soda.
        //If the payment does not meet the cost of the soda: dispense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)// fix the && _inventory.Count > 0 to properly determine if there is a soda can available
        {
            double paymentValue = TotalCoinValue(payment);

            if (paymentValue < chosenSoda.Price)
            {
                UserInterface.DisplayError("Not a enough money\n\nTransaction was not completed.\n\nPress enter to continue");
                customer.AddCoinsIntoWallet(payment);
            }
            else if (paymentValue == chosenSoda.Price && _inventory.Count > 0)
            {
                DepositCoinsIntoRegister(payment);
                customer.AddCanToBackpack(chosenSoda);
                _inventory.Remove(chosenSoda);
            }

            else if (paymentValue > chosenSoda.Price && _inventory.Count > 0)
            {
                DepositCoinsIntoRegister(payment);
                double changeValue = DetermineChange(paymentValue, chosenSoda.Price);
                List<Coin> returnCoins = GatherChange(changeValue);
                customer.AddCoinsIntoWallet(returnCoins);
                customer.AddCanToBackpack(chosenSoda);
                _inventory.Remove(chosenSoda);
            }
            if (paymentValue > chosenSoda.Price && _inventory.Count == 0)
            {
                UserInterface.DisplayError("Soda is out of stock.\n\nCannot complete the transaction.");
                customer.AddCoinsIntoWallet(payment);
            }
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue) //need follow up logic in final else statement if register does not have enough to make proper change
        {
            List<Coin> changeToBeReturned = new List<Coin>();

            while (changeValue > 0)
            {
                if (changeValue > 0.25 && RegisterHasCoin("Quarter") == true)
                {
                    Coin quarter = GetCoinFromRegister("Quarter");
                    changeToBeReturned.Add(quarter);
                    changeValue -= quarter.Value;
                }
                else if (changeValue > 0.10 && RegisterHasCoin("Dime") == true)
                {
                    Coin dime = GetCoinFromRegister("Dime");
                    changeToBeReturned.Add(dime);
                    changeValue -= dime.Value;
                }
                else if (changeValue > 0.05 && RegisterHasCoin("Nickel") == true)
                {
                    Coin nickel = GetCoinFromRegister("Nickel");
                    changeToBeReturned.Add(nickel);
                    changeValue -= nickel.Value;
                }
                else if (changeValue > 0 && RegisterHasCoin("Penny") == true)
                {
                    Coin penny = GetCoinFromRegister("Penny");
                    changeToBeReturned.Add(penny);
                    changeValue -= penny.Value;
                }
                else
                {
                    Console.WriteLine("SodaMachine does not have enough change. \n\n Please enter exact change");
                }
            }
            return changeToBeReturned;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            foreach (Coin coin in _register)
            {
                if (coin.Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name)
        {
            foreach (Coin coin in _register)
            {
                if (coin.Name == name)
                {
                    _register.Remove(coin);
                    return coin;
                }
            }
            return null;
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            double returnChange;
            returnChange = (totalPayment - canPrice);

            return returnChange;
        }
        //Takes in a list of coins to returnt he total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double totalValue = 0;
            foreach (Coin coin in payment)
            {
                totalValue += coin.Value;
            }
            return totalValue;
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
            for (int i = 0; i < coins.Count; i++)
            {
                _register.Add(coins[i]);
            }
        }
    }
}
