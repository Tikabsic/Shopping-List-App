using App.Interfaces;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace App.Objects
{
    [Serializable]
    [XmlRoot("ProductListContainer", Namespace = "")]
    public class ProductListContainer
    {
        private static int _IDCounter = 0;

        private int _ID { get; set; }
        private string _Name { get; set; }

        private float _TotalValue { get; set; }
        private List<Product> _productsListContainer { get; set; }
        private DateTime _DateOfCreation { get; set; }

        public int Get_ID()
        {
            return _ID;
        }

        public string Get_Name()
        {
            return _Name;
        }

        public float GetTotalValue()
        {
            return _TotalValue;
        }

        public List<Product> GetProductsList()
        {
            return _productsListContainer;
        }

        public DateTime Get_DateTime()
        {
            return _DateOfCreation;
        }

        private static float TotalValue(ProductListContainer container)
        {
            float productsValue = 0;

            foreach (Product Prod in container._productsListContainer)
            {
                if (Prod.GetQuantity() < 1)
                {
                    productsValue += Prod.GetPrice();
                }
                //Condition to check if product have a decimal quantity
                if (Prod.GetQuantity() > 1 && Product.HaveQuantityAComma(Prod) == true)
                {
                    productsValue += Prod.GetPrice();
                }
                else
                {
                    productsValue += Prod.GetPrice() * Prod.GetQuantity();
                }
            }
            return productsValue;
        }

        public static void Display(List<Product> ProductsList)
        {
            foreach (Product Prod in ProductsList.Where(x => x.GetIsNecessary() == true))
            {
                Console.WriteLine($"{Prod.GetName()}, cena wynosi {Prod.GetPrice()}zł, ilość w koszyku {Prod.GetIsNecessary()}");
                IDottable.Stars();
            }
            foreach (Product Prod in ProductsList.Where(x => x.GetIsNecessary() == false))
            {
                Console.WriteLine($"{Prod.GetName()}, cena wynosi {Prod.GetPrice()}zł, ilość w koszyku {Prod.GetIsNecessary()}");
                IDottable.Stars();
            }
            //Counting all unnecessary items with IsNecessary flag False
            int unnecessaryItems = 0;
            foreach (Product Prod in ProductsList)
            {
                if (Prod.GetIsNecessary() == false)
                {
                    unnecessaryItems++;
                }
            }
            //Conditions to display unnecessary items counter
            if (unnecessaryItems >= 1 && unnecessaryItems < 5)
            {
                Console.WriteLine("Całkowita wartość niepotrzebnych przedmiotów: " + unnecessaryItems);
                IDottable.Stars();
                foreach (Product Prod in ProductsList.Where(x => x.GetIsNecessary() == false))
                {
                    Console.WriteLine($"{Prod.GetName()}, cena wynosi {Prod.GetPrice()}zł, ilość w koszyku:{Prod.GetIsNecessary()}");
                    IDottable.Stars();
                }
            }
            else if (unnecessaryItems > 5)
            {
                Console.WriteLine($"Planujesz kupić za dużo niepotrzebnych rzeczy: {unnecessaryItems}");
                IDottable.Stars();
                foreach (Product Prod in ProductsList.Where(x => x.GetIsNecessary() == false))
                {
                    Console.WriteLine($"{Prod.GetName()} , cena wynosi  {Prod.GetPrice()}zł, ilość w koszyku:{Prod.GetIsNecessary()}");
                    IDottable.Stars();
                }
            }
            else
            {
                Console.WriteLine("Brawo, nie wydajesz na głupoty :) !");
                IDottable.Stars();
            }
        }

        public ProductListContainer()
        {
        }

        public ProductListContainer(List<Product> productsList)
            {
                _ID = ++_IDCounter;

                Console.WriteLine("Podaj nazwę listy:");
                _Name = Console.ReadLine();

                _productsListContainer = productsList;

                _TotalValue = TotalValue(this);

            _DateOfCreation = DateTime.Now;

            }
        }
    }
