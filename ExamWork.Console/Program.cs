using ExamWork.DataAccess;
using ExamWork.Models;
using System.Collections.Generic;

namespace ExamWork.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string userString;
            bool isParsed;

            System.Console.WriteLine("Выберите, что хотите добавить:");
            System.Console.WriteLine("1) Страна");
            System.Console.WriteLine("2) Город");
            System.Console.WriteLine("3) Улица");

            userString = System.Console.ReadLine();

            isParsed = int.TryParse(userString, out int userChoice);

            if(!isParsed)
            {
                System.Console.WriteLine("Неверный ввод!");
                System.Console.ReadLine();
                return;
            }

            switch(userChoice)
            {
                case 1:
                    AddCountry();
                    break;
                case 2:
                    AddCity();
                    break;
                case 3:
                    AddStreet();
                    break;
                default:
                    System.Console.WriteLine("НЕТ");
                    break;
            }

            System.Console.ReadLine();
        }

        public static void AddStreet()
        {
            string userString;
            bool isParsed;

            var street = new Street();

            System.Console.WriteLine("Введите название улицы:");
            userString = System.Console.ReadLine();

            street.Name = userString;

            var cities = new List<City>();

            using (var context = new UnitOfWork())
            {
                cities.AddRange(context.Cities.GetAll());
            }

            int i = 0;

            foreach (var city in cities)
            {
                System.Console.WriteLine(i++ + ") " + city.Name);
            }

            System.Console.WriteLine("Выберите город, в котором существует эта улица:");
            userString = System.Console.ReadLine();

            isParsed = int.TryParse(userString, out int cityNumber);

            if (!isParsed || cityNumber > cities.Count || cityNumber <= 0)
            {
                System.Console.WriteLine("Неверный ввод!");
                return;
            }

            street.City = cities[cityNumber];
            street.CityId = cities[cityNumber].Id;

            using (var context = new UnitOfWork())
            {
                context.Streets.Add(street);
            }
        }

        public static void AddCity()
        {
            string userString;
            bool isParsed;

            var city = new City();

            System.Console.WriteLine("Введите название города:");
            userString = System.Console.ReadLine();

            city.Name = userString;

            var countries = new List<Country>();

            using (var context = new UnitOfWork())
            {
                countries.AddRange(context.Countries.GetAll());
            }

            int i = 0;

            foreach (var country in countries)
            {
                System.Console.WriteLine(i++ + ") " + country.Name);
            }
            
            System.Console.WriteLine("Выберите страну, в которой существует этот город:");
            userString = System.Console.ReadLine();

            isParsed = int.TryParse(userString, out int countryNumber);

            if(!isParsed || countryNumber > countries.Count || countryNumber <= 0)
            {
                System.Console.WriteLine("Неверный ввод!");
                return;
            }

            city.Country = countries[countryNumber];
            city.CountryId = countries[countryNumber].Id;

            using (var context = new UnitOfWork())
            {
                context.Cities.Add(city);
            }
        }

        public static void AddCountry()
        {
            string userString;

            var country = new Country();

            System.Console.WriteLine("Введите название страны:");
            userString = System.Console.ReadLine();

            country.Name = userString;

            using (var context = new UnitOfWork())
            {
                context.Countries.Add(country);
            }
        }
    }
}
