using System;
using System.Collections.Generic;

namespace VehicleDemo
{
    class MainClass
    {
        // Deklarera klassvariabler
        public static List<Vehicle> vehicleList = new List<Vehicle>();

        // Metod: main
        // Metoden som startar applikationen
        public static void Main(string[] args)
        {
            char menuSelection;

            addVehicleAtStart(); // Skapar några objekt till vår lista

            do
            {
                switch (menuSelection = menu())
                {
                    case '0':   // Avsluta programmet
                        break;
                    case '1':   // Skriv ut lista
                        printList();
                        break;
                    case '2':   // Lägg till bil
                        addCar();
                        break;
                    case '3':   // Lägg till lastbil
                        addLorry();
                        break;
                    case '4':   // Ta bort fordon
                        removeVehicle();
                        break;
                    case '5':   // Töm hela listan
                        emptyList();
                        break;
                    default:    // Felaktiga val
                        break;
                }
            } while (menuSelection != '0');
        }

        // Metod: addCar
        public static void addCar()
        {
            Console.WriteLine("\n\nAnge information om bilen");

            Console.Write("Registreringsnummer: ");
            String regNr = Console.ReadLine();

            Console.Write("Bilmärke: ");
            String make = Console.ReadLine();

            Console.Write("Modell: ");
            String model = Console.ReadLine();

            Console.Write("Årsmodell: ");
            int year = Convert.ToInt16(Console.ReadLine());

            Console.Write("Till salu (J/N): ");
            char ch = Convert.ToChar(Console.Read());

            bool forSale = false;
            if (Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            vehicleList.Add(new Car(regNr, make, model, year, forSale));
        }

        // Metod: addLorry
        public static void addLorry()
        {
            Console.WriteLine("\n\nAnge information om lastbilen");

            Console.Write("Registreringsnummer: ");
            String regNr = Console.ReadLine();

            Console.Write("Märke: ");
            String make = Console.ReadLine();

            Console.Write("Modell: ");
            String model = Console.ReadLine();

            Console.Write("Årsmodell: ");
            int year = Convert.ToInt16(Console.ReadLine());

            Console.Write("Lastkapacitet: ");
            int load = Convert.ToInt32(Console.ReadLine());

            Console.Write("Till salu (J/N): ");
            char ch = Convert.ToChar(Console.Read());

            bool forSale = false;
            if (Char.ToUpper(ch) == 'J')
            {
                forSale = true;
            }

            vehicleList.Add(new Lorry(regNr, make, model, year, forSale, load));
        }

        // Metod: addVehicleAtStart
        public static void addVehicleAtStart()
        {
            vehicleList.Add(new Car("ABC123", "Volvo", "V70", 2012, false));
            vehicleList.Add(new Lorry("DEF456", "BMW", "520", 2011, true, 15000));
            vehicleList.Add(new Car("GHI789", "Saab", "95", 2006, false));
        }

        // Metod: emptyList
        public static void emptyList()
        {
            vehicleList.Clear();
        }

        // Metod: menu
        public static char menu()
        {
            String menu = "\n\n##############################" +
                          "\n##" +
                          "\n## Programmeny ##" +
                          "\n## Antal fordon: " + vehicleList.Count + " st. ##" +
                          "\n##" +
                          "\n##############################" +
                          "\n1. Skriv ut listan" +
                          "\n2. Lägg till bil" +
                          "\n3. Lägg till lastbil" +
                          "\n4. Ta bort fordon" +
                          "\n5. Töm hela listan" +
                          "\n0. Avsluta" +
                          "\nAnge ditt val: ";

            Console.Write(menu);
            return Console.ReadKey().KeyChar;
        }

        // Metod: printList
        public static void printList()
        {
            int i = 1;

            Console.WriteLine("\n\nNr\tRegNr\tMärke\tModell\tÅrsmodell\tTill salu?");

            foreach (Vehicle v in vehicleList)
            {
                Console.Write(i++);
                Console.WriteLine(v.ToString());
            }
        }

        // Metod: removeVehicle
        public static void removeVehicle()
        {
            Console.WriteLine("Dessa fordon finns i din lista");

            printList();

            Console.Write("\nVälj ett fordon att ta bort från listan [0 ångrar]: ");
            int removeIndex = Convert.ToInt16(Console.ReadLine());

            if (removeIndex != 0)
            {
                vehicleList.RemoveAt(removeIndex - 1);
            }
        }
    }
}
