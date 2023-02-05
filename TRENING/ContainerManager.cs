using App.Objects;

namespace ShoppingList
{
    [Serializable]
    sealed class ContainerManager
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

        public static float GetTotalValueOfSpecificContainer(ProductListContainer container)
        {
            Console.WriteLine("Podaj ID kontenera:");
            var id = Console.ReadLine();
            int containerID;
            float productsValue = 0;
            if (int.TryParse(id, out containerID))
            {
                if (containerID == container.Get_ID())
                {
                    Console.WriteLine($"Wartość kontenera {containerID} to: {container.GetTotalValue()} zł!");
                }
                else
                {
                    Console.WriteLine("Proszę podać poprawne ID kontenera.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Niepoprawne dane, proszę podać wartość liczbową.");
                Console.ReadLine();
            }
            return productsValue;
        }

        public float DisplayTotalValue()
        {
            float totalValue = 0;
            foreach (ProductListContainer container in _ProductListContainers)
            {
                totalValue += container.GetTotalValue();
            }
            return totalValue;
        }

        public static void DisplayShoppingLists(List<ProductListContainer> containers)
        {
            foreach (ProductListContainer container in _ProductListContainers)
            {
                Console.WriteLine($"ID Listy: {container.Get_ID()} , Nazwa Listy: {container.Get_Name()} , Całkowita Wartość Listy: {container.GetTotalValue()} , Data Utworzenia: {container.Get_DateTime()}");
            }
        }

        public static void SearchForSpecificList(List<ProductListContainer> containers)
        {
            Console.WriteLine("Podaj ID Listy:");
            var id = Console.ReadLine();
            int containerID;
            if (int.TryParse(id, out containerID))
            {
                var selectedContainer = containers.FirstOrDefault(c => c.Get_ID() == containerID);
                if (selectedContainer != null)
                {
                    List<Product> productList = selectedContainer.GetProductsList();
                    ProductListContainer.Display(productList);
                }
                else
                {
                    Console.WriteLine("Nie znaleziono kontenera o podanym ID.");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Niepoprawne dane, proszę podać wartość liczbową.");
                Console.ReadLine();
            }
        }

    }

}
