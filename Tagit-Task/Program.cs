using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Tagit_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            string tagsPath = @"Data/tags.txt";
            string path = Path.Combine(Environment.CurrentDirectory, tagsPath);

            string tagsText = File.ReadAllText(path);
            string[] listOfItems = tagsText.Split(
                new [] { "\n" },
                StringSplitOptions.None
                );

            DecodeHex(listOfItems);
            Console.ReadLine();
        }

        public static void DecodeHex(string[] inputArray)
        {
            Decoder decoder = new Decoder();
            int invalidTags = 0;
            foreach (var item in inputArray)
            {
                string binaryString = "";
                try
                {
                    binaryString = String.Join(
                        String.Empty, item.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0'))
                    );

                    decoder.DecodeBinaryEPC(binaryString);
                }
                catch (Exception e)
                {
                    invalidTags++;
                    //Console.WriteLine(e);
                }
            }

            decoder.FindSerialNumberAndCountOfMilkaOreo(1253252);
            Console.WriteLine("Number of invalid tags : {0}", invalidTags);
        }


    }
}
