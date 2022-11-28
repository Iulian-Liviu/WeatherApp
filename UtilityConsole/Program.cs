namespace UtilityConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string weathersCodesjson = new WeatherCodeHelper().ToJson();
                Console.WriteLine(weathersCodesjson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}