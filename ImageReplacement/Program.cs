using System;

namespace ImageReplacement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Replacement start");
            //ユーザ入力
            Console.WriteLine("Enter source path");
            var srcPath = Console.ReadLine();
            Console.WriteLine("Enter dist path");
            var distPath = Console.ReadLine();
            //入れ替え実施
            Replacement replacement;
            try
            {
                replacement = new Replacement(srcPath, distPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            replacement.Replace();
        }
    }
}
