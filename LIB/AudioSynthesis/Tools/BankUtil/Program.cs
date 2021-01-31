using System;
namespace BankUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            BankCreator bc = new BankCreator();
            if (args.Length > 1)
                bc.CreateBankFile(args[0].Replace("\"", string.Empty), args[1].Replace("\"", string.Empty));
            else if (args.Length == 1)
                bc.CreateBankFile(args[0].Replace("\"", string.Empty), "");
            Console.WriteLine("Done.");
        }
    }
}
