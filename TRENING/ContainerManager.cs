
using System.Xml.Serialization;

namespace App.Objects
{
    [Serializable]
    [XmlRoot("ProductListContainer", Namespace = "")]
    public class ContainerManager
    {
        private static ContainerManager instance = null;
        private static readonly object padlock = new object();
        private static List<ProductListContainer> _ProductListContainers;

        public ContainerManager()
        {
            _ProductListContainers = new List<ProductListContainer>();
        }


        public static ContainerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ContainerManager();
                        }
                    }
                }
                return instance;
            }
        }
        public static void AddContainer(ProductListContainer container)
        {
            _ProductListContainers.Add(container);
        }

        public ProductListContainer GetContainerWithID(int id)
        {
            foreach (ProductListContainer container in _ProductListContainers)
            {
                if (container.Get_ID() == id)
                {
                    return container;
                }
            }
            return null;
        }

        //public static float GetTotalValueOfSpecificContainer(ProductListContainer container)
        //{
        //    Console.WriteLine("Podaj ID kontenera:");
        //    var id = Console.ReadLine();
        //    int containerID;
        //    float productsValue = 0;
        //    if (int.TryParse(id, out containerID))
        //    {
        //        if (containerID == container.Get_ID())
        //        {
        //            Console.WriteLine($"Wartość kontenera {containerID} to: {container.GetTotalValue()} zł!");
        //        }
        //        else
        //        {
        //            Console.WriteLine("Proszę podać poprawne ID kontenera.");
        //            Console.ReadLine();
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Niepoprawne dane, proszę podać wartość liczbową.");
        //        Console.ReadLine();
        //    }
        //    return productsValue;
        //}

        public float DisplayTotalValue()
        {
            float totalValue = 0;
            foreach (ProductListContainer container in _ProductListContainers)
            {
                totalValue += container.GetTotalValue();
            }
            return totalValue;
        }

        public static void DisplayShoppingLists()
        {
            foreach (ProductListContainer containers in _ProductListContainers)
            {
                Console.WriteLine($"ID Listy: {containers.Get_ID()} , Nazwa Listy: {containers.Get_Name()} , Całkowita Wartość Listy: {containers.GetTotalValue()} , Data Utworzenia: {containers.Get_DateTime()}");
            }            
        }

        public static void SearchForSpecificList(ContainerManager  container)
        {
            Console.WriteLine("Podaj ID Listy:");
            var id = Console.ReadLine();
            int containerID;
            if (int.TryParse(id, out containerID))
            {

                var selectedContainer = container.GetContainerWithID(containerID);
                if (selectedContainer != null)
                {
                    ProductListContainer.Display(selectedContainer.GetProductsList());
                }
                else
                {
                    Console.WriteLine("Nie znaleziono kontenera o podanym ID. Wciśnij dowolny przycisk.");
                    
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            else
            {
                Console.WriteLine("Niepoprawne dane, proszę podać wartość liczbową. Wciśnij dowolny przycisk.");
                Console.ReadKey();
                Console.Clear();
            }
        }

    }

}
