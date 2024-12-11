using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace TravelAssistant.Classes
{
    public static class InputHelper
    {
        public static string GetValidYesNoInput()
        {
            string input;
            while (true)
            {
                input = Console.ReadLine()?.ToLower()!;
                if (input == "yes" || input == "no")
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'YES' or 'NO'.");
                }
            }
        }

        public static string GetValidCityName(string prompt, TravelPlanner travelPlanner)
        {
            string cityName = string.Empty;
            while (true)
            {
                Console.Write(prompt);
                cityName = Console.ReadLine()?.Trim()!;
                if (string.IsNullOrEmpty(cityName))
                {
                    Console.WriteLine("City name cannot be empty. Please try again.");
                }
                else
                {
                    var city = travelPlanner.GetCity(cityName);
                    if (city == null)
                    {
                        Console.WriteLine($"The city '{cityName}' does not exist. Please try again.");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return cityName;
        }

        public static List<string> GetValidCitiesToVisit(TravelPlanner travelPlanner, string startCity)
        {
            List<string> citiesToVisit = new List<string>();

            AnsiConsole.MarkupLine($"[italic lightseagreen]You can travel to the following cities from {startCity}:[/]");

            var availableCities = travelPlanner.GetCityNames().Where(city => !city.Equals(startCity, StringComparison.OrdinalIgnoreCase)).ToList();

            AnsiConsole.MarkupLine("[italic lightseagreen]" + string.Join(", ", availableCities) + " [/]\n");

            while (true)
            {
                Console.Write("Enter the cities you want to visit, separated by commas: ");
                var input = Console.ReadLine();

                citiesToVisit = input?.Split(',')
                    .Select(c => c.Trim().ToLower()) 
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToList()!;

                bool validCities = citiesToVisit.All(city =>
                    travelPlanner.GetCityNames().Any(c => c.ToLower() == city) && city != startCity.ToLower());

                if (citiesToVisit.Count > 0 && validCities)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("One or more cities are invalid or empty. Please try again.");
                }
            }

            return citiesToVisit;
        }

        public static string GetValidTravelPreference()
        {
            string preference = string.Empty;
            while (true)
            {
                Console.WriteLine("\nWhat is your route preference? (distance, time, cost): ");
                preference = Console.ReadLine()?.ToLower().Trim()!;
                if (preference == "distance" || preference == "time" || preference == "cost")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid preference. Please enter 'distance', 'time', or 'cost'.");
                }
            }
            return preference;
        }
    }
}