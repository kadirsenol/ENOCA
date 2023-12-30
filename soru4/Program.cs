namespace soru4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string yildiz = "**";
            string star = "";
            Console.WriteLine("*");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{star = (yildiz + star)}");
            }
        }
    }
}
