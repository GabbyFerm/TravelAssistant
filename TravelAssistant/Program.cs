using System;
using System.Collections.Generic;
using TravelAssistant.Classes;
using Spectre.Console;
using Figgle;

namespace TravelAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            string welcomeMessage = "Welcome to Trip Advisor!";
            var welcomeBanner = FiggleFonts.Standard.Render(welcomeMessage);
            AnsiConsole.MarkupLine($"[bold lightseagreen]{welcomeBanner}[/]");

            string filePath = "Json/travel_data.json";
            var travelPlanner = new TravelPlanner(filePath);

            while (true)
            {
                var cityMenu = new SelectionPrompt<string>()
                    .Title("[darkcyan]Select a city to plan your trip from:[/]")
                    .HighlightStyle(new Style(foreground: Color.DarkCyan)) 
                    .AddChoices(travelPlanner.GetCityNames()); 

                var selectedCity = AnsiConsole.Prompt(cityMenu);
                Console.WriteLine($"You selected {selectedCity} as your starting city.\n");

                List<string> citiesToVisit = InputHelper.GetValidCitiesToVisit(travelPlanner, selectedCity);
                string preference = InputHelper.GetValidTravelPreference();

                travelPlanner.GetBestRoute(selectedCity, citiesToVisit, preference);

                var continuePrompt = new SelectionPrompt<string>()
                    .Title("\n[lightseagreen]Do you want to plan another trip?[/]")
                    .HighlightStyle(new Style(foreground: Color.DarkCyan))
                    .AddChoices("YES", "NO");

                var response = AnsiConsole.Prompt(continuePrompt);
                if (response == "NO")
                {
                    break;
                }
            }

            AnsiConsole.MarkupLine("[bold lightseagreen]Thank you for using the travel assistant![/]");
        }
    }
}