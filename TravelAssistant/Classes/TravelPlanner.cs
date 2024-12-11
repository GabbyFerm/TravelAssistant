using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TravelAssistant.Classes
{
    public class TravelPlanner
    {
        private List<City> _cities;

        public TravelPlanner(string filePath)
        {
            string jsonData = File.ReadAllText(filePath);
            _cities = JsonConvert.DeserializeObject<List<City>>(jsonData)!;
        }

        public City GetCity(string name)
        {
            return _cities.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase))!;
        }
        public List<string> GetCityNames()
        {
            return _cities.Select(c => c.Name).ToList();
        }

        public void GetBestRoute(string startCity, List<string> citiesToVisit, string preference)
        {
            var start = GetCity(startCity);
            if (start == null)
            {
                Console.WriteLine("The start city does not exist.");
                return;
            }

            int totalDistance = 0;
            int totalTime = 0;
            int totalCost = 0;

            string currentCity = startCity;

            foreach (var cityName in citiesToVisit)
            {
                var destination = GetCity(cityName);
                if (destination == null)
                {
                    Console.WriteLine($"The city {cityName} does not exist.");
                    continue;
                }

                var connection = start.Connections?.FirstOrDefault(c => c.Destination.Equals(cityName, StringComparison.OrdinalIgnoreCase));

                if (connection != null)
                {
                    Console.WriteLine($"Travel from {currentCity} to {cityName}:");

                    TravelOption ?bestOption = null;
                    switch (preference)
                    {
                        case "distance":
                            bestOption = connection.TravelOptions.OrderBy(o => o.Distance).First();
                            break;
                        case "time":
                            bestOption = connection.TravelOptions.OrderBy(o => o.Time).First();
                            break;
                        case "cost":
                            bestOption = connection.TravelOptions.OrderBy(o => o.Cost).First();
                            break;
                    }

                    if (bestOption != null)
                    {
                        Console.WriteLine($"Best option: {bestOption.TravelMethod}, Distance: {bestOption.Distance} km, Time: {bestOption.Time} minutes, Cost: {bestOption.Cost} kr");
                        totalDistance += bestOption.Distance;
                        totalTime += bestOption.Time;
                        totalCost += bestOption.Cost;
                    }

                    currentCity = cityName;
                }
                else
                {
                    Console.WriteLine($"No connection from {currentCity} to {cityName}");
                }
            }

            Console.WriteLine("\nSummary:");
            Console.WriteLine($"Total distance: {totalDistance} km");
            Console.WriteLine($"Total travel time: {totalTime} minutes");
            Console.WriteLine($"Total cost: {totalCost} kr");
        }
    }
}