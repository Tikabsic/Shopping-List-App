using App.Interfaces;

namespace App.Objects
{
    [Serializable]
    public class Product : IDottable, IToFloatParsable
    {
        //Product properties
        private int ID { get; set; }
        public string Name { get; private set; }
        private float Price { get; set; }
        private float Quantity { get; set; }
        private bool IsNecessary { get; set; }



        public float GetPrice()
        {
            return Price;
        }
        public float GetQuantity()
        {
            return Quantity;
        }

        public bool GetIsNecessary()
        {
            return IsNecessary;
        }

        //Method to set the IsNecessary boolean flag correctly
        private bool IsThisNecessary()
        {
            string userInput = Console.ReadLine();

            while (userInput != "Tak" && userInput != "Nie")
            {
                switch (userInput)
                {
                    case "Tak":
                        break;
                    default:
                        Console.WriteLine("Proszę udzielić odpowiedzi Tak lub Nie");
                        break;
                }
                userInput = (Console.ReadLine());
            }
            return userInput == "Tak" ? true :  false;

        }


        //Method to display every single item in shopping list
        public static void Display(List<Product> productsList)
        {
            if (productsList == null)
            {
                Console.WriteLine("Lista jest pusta!");
            }
            foreach (Product Prod in productsList.Where(x => x.IsNecessary == true))
            {
                Console.WriteLine($"{Prod.Name}, cena wynosi {Prod.Price}zł, ilość w koszyku {Prod.Quantity}");
                IDottable.Stars();
            }
            foreach (Product Prod in productsList.Where(x => x.IsNecessary == false))
            {
                Console.WriteLine($"{Prod.Name}, cena wynosi {Prod.Price}zł, ilość w koszyku {Prod.Quantity}");
                IDottable.Stars();
            }
            //Counting all unnecessary items with IsNecessary flag False
            int unnecessaryItems = 0;
            foreach (Product Prod in productsList)
            {
                if (Prod.IsNecessary == false)
                {
                    unnecessaryItems++;
                }
            }
            //Conditions to display unnecessary items counter
            if (unnecessaryItems >= 1 && unnecessaryItems < 5)
            {
                Console.WriteLine("Całkowita wartość niepotrzebnych przedmiotów: " + unnecessaryItems);
                Console.WriteLine("********************************************");
                foreach (Product Prod in productsList.Where(x => x.IsNecessary == false))
                {
                    Console.WriteLine($"{Prod.Name}, cena wynosi {Prod.Price}zł, ilość w koszyku:{Prod.Quantity}");
                    IDottable.Stars();
                }
            }
            else if (unnecessaryItems > 5)
            {
                Console.WriteLine($"Planujesz kupić za dużo niepotrzebnych rzeczy: {unnecessaryItems}");
                IDottable.Stars();
                foreach (Product Prod in productsList.Where(x => x.IsNecessary == false))
                {
                    Console.WriteLine($"{Prod.Name}, cena wynosi {Prod.Price}zł, ilość w koszyku:{Prod.Quantity}");
                    IDottable.Stars();
                }
            }
            else
            {
                Console.WriteLine("Brawo, nie wydajesz na głupoty :) !");
                IDottable.Stars();
            }
        }
        


        //Method to display cart value
        public static void DisplayCartValue(List<Product> productsList)
        {
            //loop to add prices together and display total up value
            float productsValue = 0;
            foreach (Product Prod in productsList)
            {
                if (Prod.Quantity < 1)
                {
                    productsValue += Prod.Price;
                }
                //Condition to check if product have a decimal quantity
                if (Prod.Quantity > 1 && Product.HaveQuantityAComma(Prod) == true)
                {
                    productsValue += Prod.Price;
                }
                else
                {
                    productsValue += Prod.Price * Prod.Quantity;
                }
            }
            Console.WriteLine($"Całkowita wartość twoich zakupów wynosi: {Math.Round(productsValue, 2)}" + "zł!");
            IDottable.Stars();
        }


        //Method to check if Products quantity have a comma (decimal)
        public static bool HaveQuantityAComma(Product Product)
        {
            return Product.Quantity % 1 != 0;
        }


  
        public Product(List<Product> productsList)
        {
            ID = productsList.IndexOf(this) + 1;
            Console.Write("Podaj nazwę produktu: ");
            Name = Console.ReadLine();

            Console.Write("Podaj cenę produktu: ");
            Price = IToFloatParsable.ParseToFloat();

            Console.Write("Podaj ilość: ");
            Quantity = IToFloatParsable.ParseToFloat();

            Console.Write("Czy ten produkt jest Ci potrzebny? odpowiedź Tak lub Nie: ");
            IsNecessary = IsThisNecessary();

            Console.Clear();
        }
    }
}

