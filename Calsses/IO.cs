namespace Calsses
{
    public class IO
    {
        public void Print<T>(T value)
        {
            Console.WriteLine(value);
        }

        public void PrintAt<T>(T value)
        {
            Console.Write(value);
        }

        public int GetInt()
        {
            return Convert.ToInt32(Console.ReadLine());
        }

        public string GetStr()
        {
            return Console.ReadLine();
        }
    }
}