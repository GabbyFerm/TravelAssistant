namespace TravelAssistant.Classes
{
    public class City
    {
        public string Name { get; set; }
        public List<Connection> Connections { get; set; }

        public City(string name)
        {
            Name = name;
            Connections = new List<Connection>();
        }
    }
}