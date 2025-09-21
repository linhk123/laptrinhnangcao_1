using System;

class BaseConverter
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n=== CHUONG TRINH DOI HE CO SO (2, 10, 16) ===");
            Console.WriteLine("1. He nhi phan (BIN)");
            Console.WriteLine("2. He thap phan (DEC)");
            Console.WriteLine("3. He thap luc phan (HEX)");
            Console.WriteLine("0. Thoat");

            Console.Write("\nChon he co so dau vao (0 de thoat): ");
            int inputChoice = int.Parse(Console.ReadLine());
            if (inputChoice == 0) break;

            Console.Write("Chon he co so dau ra: ");
            int outputChoice = int.Parse(Console.ReadLine());

            Console.Write("Nhap gia tri can doi: ");
            string inputValue = Console.ReadLine();

            try
            {
                // B1: Chuyen ve he thap phan truoc
                int decimalValue = 0;
                switch (inputChoice)
                {
                    case 1: // Binary -> Decimal
                        decimalValue = Convert.ToInt32(inputValue, 2);
                        break;
                    case 2: // Decimal
                        decimalValue = int.Parse(inputValue);
                        break;
                    case 3: // Hexadecimal -> Decimal
                        decimalValue = Convert.ToInt32(inputValue, 16);
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        continue;
                }

                // B2: Chuyen tu decimal sang he co so mong muon
                string outputValue = "";
                switch (outputChoice)
                {
                    case 1: // Decimal -> Binary
                        outputValue = Convert.ToString(decimalValue, 2);
                        break;
                    case 2: // Decimal
                        outputValue = decimalValue.ToString();
                        break;
                    case 3: // Decimal -> Hexadecimal
                        outputValue = Convert.ToString(decimalValue, 16).ToUpper();
                        break;
                    default:
                        Console.WriteLine("Lua chon khong hop le!");
                        continue;
                }

                Console.WriteLine($"=> Gia tri sau khi doi: {outputValue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
        }
    }
}
