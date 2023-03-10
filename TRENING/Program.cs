using App.Objects;
using App.Interfaces;
using System.Xml.Serialization;


namespace App
{
    class Program
    {

        public static void SaveContainerToFile(ProductListContainer container)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductListContainer));

            try
            {
                using (var writer = new StreamWriter("ProductsContainersData.xml"))
                {
                    serializer.Serialize(writer, container);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas zapisywania danych do pliku. Błąd: " + ex.Message);
            }
        }

        public static void SaveToManager(ProductListContainer container)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ProductListContainer));

            try
            {
                using (var writer = new StreamWriter("ContainerManagerData.xml"))
                {
                    serializer.Serialize(writer, container);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas zapisywania danych do pliku. Błąd: " + ex.Message);
            }
        }

        public static ContainerManager GetContainersFromFile()
        {
            ContainerManager containersManager = ContainerManager.Instance;
            try
            {
                using (var reader = new StreamReader("ContainerManagerData.xml"))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ContainerManager));
                    containersManager = (ContainerManager)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Wystąpił błąd podczas wczytywania danych z pliku. Błąd: " + ex.Message);
            }
            return containersManager;
        }


        static void Main(string[] args)
        {
            ContainerManager containerManager = GetContainersFromFile();
            List<Product> productsList = new List<Product>();

            IDottable.Stars();
            Console.WriteLine("Witaj w aplikacji LISTA ZAKUPÓW!");
            Console.WriteLine("Lista komend:  :Zakupy: :Historia: :Koniec:");
            IDottable.Stars();
            string userInput = Console.ReadLine();
            while (userInput != "Koniec")
            {
                Console.Clear();
                switch (userInput)
                {
                    case "Zakupy":
                        while (userInput != "Wróć")
                        {
                            IDottable.Stars();
                            Console.WriteLine("Lista komend:  :Nowy: :Lista: :Wartość: :Zapisz: :Kasuj: :Wróć:");
                            IDottable.Stars();
                            userInput = Console.ReadLine();
                            switch (userInput)
                            {
                                case "Nowy":
                                    Console.Clear();
                                    productsList.Add(new Product(productsList));
                                    continue;

                                case "Lista":
                                    Console.Clear();
                                    Product.Display(productsList);
                                    IDottable.Stars();
                                    continue;

                                case "Wartość":
                                    Console.Clear();
                                    Product.DisplayCartValue(productsList);
                                    IDottable.Stars();
                                    continue;

                                case "Zapisz":
                                    Console.Clear();
                                    ProductListContainer Container = new ProductListContainer(productsList);
                                    ContainerManager.AddContainer(Container);
                                    SaveContainerToFile(Container);
                                    SaveToManager(Container);
                                    productsList.Clear();
                                    Console.Clear();
                                    Console.WriteLine("Lista zapisana!");
                                    IDottable.Stars();
                                    continue;

                                case "Kasuj":
                                    productsList.Clear();
                                    Console.Clear();
                                    Console.WriteLine("Lista skasowana!");
                                    IDottable.Stars();
                                    continue;

                                case "Wróć":
                                    Console.Clear();
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine("Niewłaściwa komenda, spróbuj ponownie.");
                                    IDottable.Stars();
                                    continue;
                            }
                        }
                        break;

                    case "Historia":
                        while (userInput != "Wróć")
                        {
                            IDottable.Stars();
                            Console.WriteLine("Lista komend:  :Listy: :Szukaj: :Sortuj: :Usuń: :Wróć:");
                            IDottable.Stars();
                            userInput = Console.ReadLine();
                            switch (userInput)
                            {
                                case "Listy":
                                    Console.Clear();
                                    ContainerManager.DisplayShoppingLists();
                                    IDottable.Stars();
                                    continue;

                                case "Szukaj":
                                    Console.Clear();
                                    ContainerManager.SearchForSpecificList(containerManager);
                                    continue;

                                case "Wróć":
                                    Console.Clear();
                                    break;

                                default:
                                    Console.Clear();
                                    Console.WriteLine("Niewłaściwa komenda, spróbuj ponownie.");
                                    IDottable.Stars();
                                    continue;
                            }
                        }
                        break;

                    case "Koniec":
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Niewłaściwa komenda, spróbuj ponownie.");
                        break;

                }
                IDottable.Stars();
                Console.WriteLine("Witaj w aplikacji LISTA ZAKUPÓW!");
                IDottable.Stars();
                Console.WriteLine("Lista komend:  :Zakupy: :Historia: :Koniec:");
                IDottable.Stars();
                userInput = Console.ReadLine();
            }
            Console.WriteLine("Dziękuję za skorzystanie z aplikacji LISTA ZAKUPÓW!");
            Console.ReadKey();
        }
    }
}