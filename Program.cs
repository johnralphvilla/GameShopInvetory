using System;
using System.IO;

//Group 4
//COMP1202 - Assignment #2
// Video Game Shop Inventory System

// Group Members:
//John Ralph Villa - Student ID: 101537746
//Mark Berkvich - Student ID: unknown
// Avery Bailey - Student ID: unknown 

// Program completed by: John Ralph Villa

// Date: August 7, 2026


namespace Shop
{
    internal class Program
    {

        static Game[] inventory = new Game[50];//Store up to 50 items/ 50 Lines  


        static int productCount = 0; // Product Count


        static string fileName = "VideoGames.txt"; // File Containing Inventories 


        static Random genItemNumbers = new Random();// Used To Genrate Random item Numbers


        static void Main(string[] args)
        {

            ProductsFiles();


            string choice = "";

            while (choice != "7")
            {




                choice = Menu();


                switch (choice)
                {




                    case "1": AddNewProducts(); break;
                    case "2": SearchItemsByNumber(); break;
                    case "3": SearchItemsByMaxPrice(); break;
                    case "4": SearchByGameRatings(); break;
                    case "5": StatisticalAnalysis(); break;
                    case "6": DisplayInventory(); break;
                    case "7": Console.WriteLine("Thank You For Using The Application"); Environment.Exit(0); break;
                    default: Console.WriteLine("\t\t\t\tInvalid menu option. Please try again."); break;





                }





            }

        }







        public static string Menu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t==========================================================");
            Console.WriteLine("\t\t\t\t\t\t Shops Inventory Menu ");
            Console.WriteLine("\t\t\t\t==========================================================");
            Console.WriteLine("\t\t\t\t\tPlease Enter [ 1 ] to Go Add Items/ Product ");
            Console.WriteLine("\t\t\t\t\tPlease Enter [ 2 ] to Search  Base On Item Numbers");
            Console.WriteLine("\t\t\t\t\tPlease Enter [ 3 ] to Search Base On Maximum Price");
            Console.WriteLine("\t\t\t\t\tPlease Enter [ 4 ] to Search By Game Ratings ");
            Console.WriteLine("\t\t\t\t\tPlease Enter [ 5 ] to Statistical Analysis ");
            Console.WriteLine("\t\t\t\t\tPlease Enter [ 6 ] to Display Shops Inventory ");
            Console.WriteLine("\t\t\t\t\tPlease Enter [ 7 ] to  Exit Enventory ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\t\t\t\t\tEnter Your Choice :\t");
            string choice = Console.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5" && choice != "6" && choice != "7")
            {

                Console.WriteLine("\n\t\t\t\t\t Invalid Choice Please Try Again");
                Console.Write("\n\t\t\t\t\t Type Here");
                choice = Console.ReadLine();





            }





            return choice;
        }




        static void AddNewProducts()
        {
            // Heading Display
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t\t\t==========================================================");
            Console.WriteLine(" \t\t\t\t\t\tAdding New Products");
            Console.WriteLine("\t\t\t\t==========================================================");




            string addMoreItems = "Y";


            while (addMoreItems == "Y")
            {
                // Stop the method  if inventory is Full 
                if (productCount >= inventory.Length)
                {

                    Console.WriteLine(" \t\t\t\t\t\t File is Full ");

                    return;
                }


                int itemNumber;


                // Asking the User The item Number is known or Not 
                Console.WriteLine("\n\t\t\t\tDo you Know The Item Number? Type [ Y ] For Yes, Type [ N ] For No ");
                Console.Write("\t\t\t\tEnter Your Choice :\t");

                string usersAnswers = Console.ReadLine();

                // Keep asking Until the user Y/N 
                while (usersAnswers.ToUpper() != "Y" && usersAnswers.ToUpper() != "N")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("\n\t\t\t\tInvalid Please Try Again");
                    Console.WriteLine("\n\t\t\t\tDo you Know The Item Number? Type [ Y ] For Yes, Type [ N ] For No ");

                    Console.Write("\t\t\t\tEnter Your Choice :\t");
                    usersAnswers = Console.ReadLine();
                }



                // If User Anser Y[Yes] then user will manually enter the number then the info will be collected 
                if (usersAnswers.ToUpper() == "Y")
                {
                    itemNumber = GetManualItemNumbers();
                }
                // if user answer N [No] Then a random number will be generated then the info will be collected
                else
                {
                    itemNumber = GenerateItemNumbers();
                }


                // Collecting Game/product info  name, price, rate and the amount to stack 
                string productName = ProductsNames();
                double productPrice = ProductPrice();
                double productRatings = ProductsRatings();
                int productAmount = ProductAmount();


                // creating Game Objects 
                Game newPruduct = new Game(itemNumber, productName, productPrice, productRatings, productAmount);


                // adding the new product/Game to the inventory
                inventory[productCount] = newPruduct;
                productCount++;



                // Save or Add New pruduct into existing File/VideoGame.txt
                AddProductToFile(newPruduct);



                Console.WriteLine("\t\t\t\tNew Pruduct has Success Fully Added and Has Been Save Into Inventory");



                Console.WriteLine("\n\n\t\t\t\tWould You Like To Add Another Game [ Y or N ] ");
                Console.Write("\n\t\t\t\tType Here:\t");
                addMoreItems = Console.ReadLine().ToUpper();


                while (addMoreItems != "Y" && addMoreItems != "N")
                {

                    Console.WriteLine("\n\n\t\t\t\tInvalid Please Enter A valid Answer ");
                    Console.WriteLine("\n\n\t\t\t\tWould You Like To Add Another Game [ Y or N] ");
                    Console.Write("\n\t\t\t\tType Here:\t");
                    addMoreItems = Console.ReadLine().ToUpper();



                }

            }


        }


        static void SearchItemsByNumber()
        {

            string searcMoreItems = "Y";


            while (searcMoreItems == "Y")
            {

                // Heading Display
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\t\t\t\t==========================================================");
                Console.WriteLine(" \t\t\t\t\t\tSearching Item/Game/product by Number");
                Console.WriteLine("\t\t\t\t==========================================================");


                // asking the user for item number for the game/product they looking for 
                Console.WriteLine("\n\n\t\t\t Please Enter Item Number ");
                Console.Write("\t\t\t\t Type Here: ");
                string itemNumber = Console.ReadLine();

                int search;


                // validate the item number contain  four digits        
                while (!int.TryParse(itemNumber, out search) || search < 1000 || search > 9999)
                {


                    Console.WriteLine("\t\t\t\t Please Try Again!");
                    Console.WriteLine("\t\t\t\t Please Enter Item Number");
                    Console.Write("\t\t\t\t Enter Here :\t ");
                    itemNumber = Console.ReadLine();



                }


                // Searching  the inventory for matching item number
                bool isItemFound = false;

                for (int x = 0; x < productCount; x++)
                {

                    // Checking current game/product item number 
                    if (inventory[x].GetItemNumber() == search)
                    {

                        Console.WriteLine($"{inventory[x].ToString()}");

                        isItemFound = true;
                        break;

                    }


                }

                // if no match is found  this message will be dispalyed 
                if (!isItemFound)
                {
                    Console.WriteLine("\t\t\t\t\t  Sorry No Such Game is Found");
                }


                Console.WriteLine("\t\t\t\t\tWould You Like To Search More Items   ");
                Console.WriteLine("\t\t\t\t\t Type [ Y ] for YES [ N ] for No  ");
                Console.Write("\n\t\t\t\t Type Here");
                searcMoreItems = Console.ReadLine().ToUpper();

                while (searcMoreItems != "Y" && searcMoreItems != "N")
                {


                    Console.WriteLine("\t\t\t\t\t Invalid Answer ");
                    Console.WriteLine("\t\t\t\t\tWould You Like To Search More Items [ Y / N ]");
                    Console.Write("\n\t\t\t\t Type Here");
                    searcMoreItems = Console.ReadLine().ToUpper();




                }



            }

        }








        static void SearchItemsByMaxPrice()
        {
            string searchMoreByPrice = "Y";
            while (searchMoreByPrice == "Y")
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;

                // Display Heading
                Console.WriteLine("\n\n\t\t\t\t==========================================================");
                Console.WriteLine("\t\t\t\t\t\t Welcome To Seacrh Item/Game/Product By Price ");
                Console.WriteLine("\t\t\t\t==========================================================");

                double searchByMaxPrice;
                bool isItemFound = false;

                //asking the user to enter Max price 
                Console.WriteLine("\t\t\t\t\tPlease Enter Items Price ");
                Console.Write("\t\t\t\t\tEnter Price Here:\t$");
                string itemPrice = Console.ReadLine();



                // validating if the user  enter a valid/non-negative price
                while (!double.TryParse(itemPrice, out searchByMaxPrice) || searchByMaxPrice < 0)
                {

                    Console.WriteLine("\t\t\t\t\tPlease Enter a valid  Price ");
                    Console.Write("\t\t\t\t\tEnter Price Here:\t$");

                    itemPrice = Console.ReadLine();

                }



                // Searching through all the game arround the price that is Entered by the user 
                for (int x = 0; x < productCount; x++)
                {


                    if (inventory[x].GetPrice() <= searchByMaxPrice)
                    {

                        Console.ForegroundColor = ConsoleColor.Magenta;

                        Console.WriteLine($"\t\t\t\t\t{inventory[x].ToString()}");

                        isItemFound = true;
                    }


                }



                // if No game/product is found this will be displayed
                if (!isItemFound)
                {
                    Console.WriteLine("\t\t\t\t\tSorry No Such  Game Is Found");
                }

                Console.WriteLine("\t\t\t\t\tWould You Like To Search More Items  ");
                Console.WriteLine("\t\t\t\t\t Type [ Y ] for YES [ N ] for No  ");
                Console.Write("\n\t\t\t\t Type Here");
                searchMoreByPrice = Console.ReadLine().ToUpper();

                while (searchMoreByPrice != "Y" && searchMoreByPrice != "N")
                {


                    Console.WriteLine("\t\t\t\t\t Invalid Answer ");
                    Console.WriteLine("\t\t\t\t\tWould You Like To Search More Items [Y / N]");
                    Console.Write("\n\t\t\t\t Type Here");
                    searchMoreByPrice = Console.ReadLine().ToUpper();




                }

            }

        }

        static void SearchByGameRatings() //Displaying Games with Rates Only  not The full inventory
        {
            Console.WriteLine("\n\n\t\t\t\t==========================================================");
            Console.WriteLine("\t\t\t\t\t\t Search By Game Ratings ");
            Console.WriteLine("\t\t\t\t==========================================================");

            string searchMore = "Y";
            while (searchMore == "Y")
            {

                Console.WriteLine("\t\t\t\t\t\t Enter Game Rate [0 - 5 ] ");
                Console.Write("\n\t\t\t\t\t\t Type Here :\t");
                string searchRate = Console.ReadLine();

                double searchRatings;

                while (!double.TryParse(searchRate, out searchRatings) || searchRatings < 0 || searchRatings > 5)
                {
                    Console.WriteLine("\t\t\t\t\t\t Ivalid Please Try Again");
                    Console.WriteLine("\t\t\t\t\t\t Enter Game Rate [0 - 5 ] ");
                    Console.Write("\n\t\t\t\t\t\t Type Here :\t");
                    searchRate = Console.ReadLine();

                }




                if (productCount == 0)
                {

                    Console.WriteLine($"\n\t\t\t\t\tIventory is Empty ");

                }


                bool isGameFound = false;

                for (int x = 0; x < productCount; x++)
                {
                    if (inventory[x].GetGameRating() == searchRatings)
                    {


                        Console.WriteLine($"\n\t\t\t\t\tGame Name:  \t{inventory[x].GetItemName()}");
                        Console.WriteLine($"\t\t\t\t\tGame Ratings: \t{inventory[x].GetGameRating()}");
                        Console.WriteLine($"\t\t\t\t\tGame Price: \t${inventory[x].GetPrice()}");
                        Console.WriteLine($"\t\t\t\t\tStock: \t{inventory[x].GetQuantity()}\n\n");
                        isGameFound = true;
                    }

                }

                Console.WriteLine($"\t\t\t\t\t Would Like to Search Another Game [Y / N]");
                Console.Write("\n\t\t\t\t Type Here:\t");
                searchMore = Console.ReadLine().ToUpper();

                while (searchMore != "Y" && searchMore != "N")
                {
                    Console.WriteLine($"\t\t\t\t\t Invalid Option Try Again! ");
                    Console.WriteLine($"\t\t\t\t\t Would Like to Search Another Game [Y / N]");
                    searchMore = Console.ReadLine().ToUpper();





                }


            }
        }




        static void StatisticalAnalysis()
        {


            Console.ForegroundColor = ConsoleColor.DarkYellow;

            // Display Heading
            Console.WriteLine("\n\n\t\t\t\t==========================================================");
            Console.WriteLine("\t\t\t\t\t\t Welcome To Statistical Analysis ");
            Console.WriteLine("\t\t\t\t==========================================================");

            // stop the method if there are no games in the inventory 
            if (productCount == 0)
            {
                Console.WriteLine("\t\t\t\t\t\tEmpty Inventory");
                return;
            }



            // Store the total price and begin with first Game 
            double totalPrice = 0; // intialinzing totalPrice just incase inventory is Empty 

            Game lowPriceGame = inventory[0];
            Game maxPriceGame = inventory[0];


            // Calcualting the total to find the cheapest to most expensive product 
            for (int x = 0; x < productCount; x++)
            {


                totalPrice = totalPrice + inventory[x].GetPrice();


                if (inventory[x].GetPrice() < lowPriceGame.GetPrice())
                {
                    lowPriceGame = inventory[x];

                }
                else if (inventory[x].GetPrice() > maxPriceGame.GetPrice())
                {
                    maxPriceGame = inventory[x];


                }

            }

            //  Calculating average price 
            double averagePrice = totalPrice / productCount;
            // calculating price range 
            double priceRange = maxPriceGame.GetPrice() - lowPriceGame.GetPrice();


            Console.ForegroundColor = ConsoleColor.DarkGreen;

            //Displaying average anda range price
            Console.WriteLine("\n\t\t\t\t\t\tAverage Price :\t${0:f2}", averagePrice);
            Console.WriteLine("\t\t\t\t\t\tRange Price   :\t${0}", priceRange.ToString());

            // displaying Name  and Most expensive product/ game
            Console.WriteLine("\n\n\t\t\t\t\t\tMost Expensive Game :\t{0}", maxPriceGame.GetItemName());
            Console.WriteLine("\t\t\t\t\t\tGame Price          :\t${0}", maxPriceGame.GetPrice().ToString());


            // displaying Name  and cheapest  product/ game
            Console.WriteLine("\n\n\t\t\t\t\t\tCheapest Game   :\t{0}", lowPriceGame.GetItemName());
            Console.WriteLine("\t\t\t\t\t\tGame   Price    :\t${0}", lowPriceGame.GetPrice().ToString());



        }








        static double ProductPrice()
        {



            Console.ForegroundColor = ConsoleColor.Yellow;
            double price;

            Console.WriteLine("\t\t\t\t Please Enter Pruduct/Game Price ?");
            Console.Write("\t\t\t\t Price :\t$");
            string itemPrice = Console.ReadLine();

            while (!double.TryParse(itemPrice, out price) || price < 0)
            {


                Console.WriteLine("\t\t\t\t Please try Again?");
                Console.Write("\t\t\t\t Price :\t$");
                itemPrice = Console.ReadLine();
            }

            return price;
        }




        static string ProductsNames()
        {


            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("\t\t\t\t Please Enter Pruduct/Game Name ?");
            Console.Write("\t\t\t\t Pruduct Name :\t");
            string productName = Console.ReadLine();


            while (string.IsNullOrWhiteSpace(productName))
            {

                Console.WriteLine("\t\t\t\t Please Enter A Valid  Pruduct/Game Name ?");
                Console.Write("\t\t\t\t Type Here :\t");
                productName = Console.ReadLine();

            }


            return productName;



        }





        static double ProductsRatings()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            double ratings;
            Console.WriteLine("\t\t\t\t Please Give Rating to the Game 0 - 5 ");
            Console.Write("\t\t\t\t Rating :\t");

            while (!double.TryParse(Console.ReadLine(), out ratings) || ratings < 0 || ratings > 5)
            {


                Console.WriteLine("\t\t\t\t Rating can not lower than 0 and cannot be greater then 5 ");
                Console.WriteLine("\t\t\t\t Please Give Rating to the Game 0 - 5 ");
                Console.Write("\t\t\t\t Type Here :\t");
            }

            return ratings;
        }





        static int ProductAmount()
        {
            int pruductAmount;

            Console.WriteLine("\t\t\t\t Please Enter Amout to Stock ");
            Console.Write("\t\t\t\t Type Here :\t");
            while (!int.TryParse(Console.ReadLine(), out pruductAmount) || pruductAmount < 0)
            {

                Console.WriteLine("\t\t\t\t Invalid Amount Can not be Less then Zero ");
                Console.WriteLine("\t\t\t\t Please Enter Amout to Stock ");
                Console.Write("\t\t\t\t Type Here :\t");


            }


            return pruductAmount;


        }




        static void DisplayInventory()
        {


            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("\n\n\t\t\t\t==========================================================");
            Console.WriteLine("\t\t\t\t\t\tShops Games | Pruduct  Inventory ");
            Console.WriteLine("\t\t\t\t==========================================================");

            if (productCount == 0)
            {

                Console.WriteLine("\t\t\t\t\t\tShops Games | Pruduct  Inventory  Is Empty");


            }


            for (int x = 0; x < productCount; x++)
            {
                Console.WriteLine(inventory[x].ToString());
            }





            // This Commented Below is another Approach By reading the files straight from the files 
            //string input; 
            //StreamReader myFile = new StreamReader("VideoGames.txt");

            //while ((input = myFile.ReadLine()) != null)
            //{
            //    string[] component = input.Split(',');

            //    Console.WriteLine("\t\t\t==============================");
            //    Console.WriteLine("\t\t\tItem Number : " + component[0]);
            //    Console.WriteLine("\t\t\tName        : " + component[1]);
            //    Console.WriteLine("\t\t\tPrice       :$ " + component[2]);
            //    Console.WriteLine("\t\t\tRating      : " + component[3]);
            //    Console.WriteLine("\t\t\tQuantity    : " + component[4]);
            //}

            //myFile.Close();





        }





        static int GetManualItemNumbers() // Setting Item Number Manually 
        {

            int itemNumber;
            Console.WriteLine("\n\t\t\t\tPlease Enter Pruduct/Item Number ");
            Console.Write("\n\t\t\t\tMust be 4 Digits :\t ");
            string makeItemNumber = Console.ReadLine();

            // Checking the if user type in 4 digits  if number exceeds or number is less then 4 then it will ask the user to type again 
            while (!int.TryParse(makeItemNumber, out itemNumber) || itemNumber < 1000 || itemNumber > 9999)
            {

                Console.WriteLine("\t\t\t\t\t\tPlease Enter A Valid Pruduct/Item Number ");
                Console.Write("\n\t\t\t\t\t\tMust be 4 Digits :\t ");
                makeItemNumber = Console.ReadLine();
            }



            while (ItemNumbExist(itemNumber))
            {

                Console.WriteLine("\t\t\t\tSorry Item Number Already Exist ");
                Console.Write("\n\t\t\t\t A differrent Number:\t ");
                string createAnotherItemNumbe = Console.ReadLine();
                while (!int.TryParse(createAnotherItemNumbe, out itemNumber) || itemNumber < 1000 || itemNumber > 9999)
                {

                    Console.WriteLine("\t\t\t\tPlease Enter A Valid Pruduct/Item Number ");
                    Console.Write("\n\t\t\t\tMust be 4 Digits :\t ");
                    createAnotherItemNumbe = Console.ReadLine();
                }

            }


            return itemNumber;
        }





        static bool ItemNumbExist(int itemNumbers) //Checking ItemNumber If They already Exist
        {
            for (int i = 0; i < productCount; i++)
            {
                if (inventory[i].GetItemNumber() == itemNumbers)
                {
                    return true;
                }

            }

            return false;
        }






        static int GenerateItemNumbers() // If Item Number Is Unknow  And the user would like to  Genarate a ramndom 4 digit number This method will be called and Genrate Product Number
        {
            int itemNumber;

            do
            {
                // this Genraate random item number between 1000 to 9999 (4 Digits)
                itemNumber = genItemNumbers.Next(1000, 9999);


            }
            while (ItemNumbExist(itemNumber));
            return itemNumber;
        }





        static void ProductsFiles()
        {

            StreamReader readItemsByRow = new StreamReader(fileName);
            string rows;

            while ((rows = readItemsByRow.ReadLine()) != null)
            {


                if (rows.Trim() != "")
                {
                    char splitter = ',';
                    string[] info = rows.Split(splitter);

                    if (info.Length == 5)
                    {

                        int itenNumber;
                        double price;
                        double rating;
                        int quantity;




                        bool validItemNumber = int.TryParse(info[0], out itenNumber);
                        bool validItemPrice = double.TryParse(info[2], out price);
                        bool validItemRating = double.TryParse(info[3], out rating);
                        bool validItemQuantiy = int.TryParse(info[4], out quantity);








                        if (validItemNumber && validItemPrice && validItemRating && validItemQuantiy)
                        {
                            Game product = new Game(itenNumber, info[1], price, rating, quantity);
                            if (productCount < inventory.Length)
                            {
                                inventory[productCount] = product;
                                productCount++;
                            }
                        }





                    }
                }

            }

            readItemsByRow.Close();

        }







        static void AddProductToFile(Game game) // Saving Product/Games to file (VideoGames.txt)
        {
            StreamWriter writeItemsToFile = new StreamWriter(fileName, true);

            writeItemsToFile.WriteLine(game.GetItemNumber() + "," + game.GetItemName() + "," + game.GetPrice() + "," + game.GetGameRating() + "," + game.GetQuantity());

            writeItemsToFile.Close();

        }




    }








    class Game
    {
        // Fields/ Attributes /Properties
        private int itemNumber;
        private string itemName;
        private double price;
        private double userRating;
        private int quantity;


        // Constructors
        public Game() { }
        public Game(string itemName)
        {
            this.itemName = itemName;
        }
        // Constructor with Item Name and Number
        public Game(string itemName, int itemNumber)
        {
            this.itemName = itemName;
            this.itemNumber = itemNumber;
        }
        //constructor with price And its Rating
        public Game(double price, double userRating)
        {
            this.price = price;
            this.userRating = userRating;
        }
        //Constructor with item number, nmase and price
        public Game(int itemNumber, string itemName, double price)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.price = price;
        }
        // Constructor  with all field

        public Game(int itemNumber, string itemName, double price, double userRating, int quantity)
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.price = price;
            this.userRating = userRating;
            this.quantity = quantity;

        }


        // getters /Accessor Methods
        public int GetItemNumber() { return itemNumber; }
        public string GetItemName() { return itemName; }
        public double GetPrice() { return price; }
        public double GetGameRating() { return userRating; }
        public int GetQuantity() { return quantity; }



        // setters / Mutators
        public void SetItemNumber(int itemNumber) { this.itemNumber = itemNumber; }
        public void SetItemName(string itemName) { this.itemName = itemName; }
        public void SetPrice(double price) { this.price = price; }
        public void SetUserRating(double userRating) { this.userRating = userRating; }
        public void SetQuantity(int quantity) { this.quantity = quantity; }




        // Methods

        public override string ToString()
        {
            string summary = "";
            summary += "\n\t\t\t\tItem Number:\t" + itemNumber + "\t";
            summary += "\n\t\t\t\tItem Name:\t" + itemName + "\t";
            summary += "\n\t\t\t\tItem Price:\t$" + price + "\t";
            summary += "\n\t\t\t\tUser Rating:\t" + userRating + "\t";
            summary += "\n\t\t\t\tQuantity:\t" + quantity + "\t";
            return summary;

        }



    }

}