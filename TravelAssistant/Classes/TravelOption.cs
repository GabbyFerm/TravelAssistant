namespace TravelAssistant.Classes
{
    public class TravelOption
    {
        public string TravelMethod { get; set; }
        public int Distance { get; set; }
        public int Time { get; set; }
        public int Cost { get; set; }

        public TravelOption(string travelMethod, int distance, int time, int cost)
        {
            TravelMethod = travelMethod;
            Distance = distance;
            Time = time;
            Cost = cost;
        }
    }
}