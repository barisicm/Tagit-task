using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tagit_Task
{
    class Decoder
    {
        private StreamReader reader = new StreamReader(File.OpenRead(@"Data/data.csv"));
        private Dictionary<int, int> PartitionValueTableCompany = new Dictionary<int, int>
        {
            { 0,40 },
            { 1,37 },
            { 2,34 },
            { 3,30 },
            { 4,27 },
            { 5,24 },
            { 6,20 }
        };
        private Dictionary<int, int> PartitionValueTableItem = new Dictionary<int, int>
        {
            { 0,4 },
            { 1,7 },
            { 2,10 },
            { 3,14 },
            { 4,17 },
            { 5,20 },
            { 6,24 }
        };
        private List<int[]> itemList = new List<int[]>();

        private void ConvertBinaryStringsToInt(string company, string item, string serialNumber)
        {
            int companyConverted = Convert.ToInt32(company, 2);
            int itemConverted = Convert.ToInt32(item, 2);
            int serialNumberConverted = Convert.ToInt32(serialNumber, 2);

            int[] array = new int[] { companyConverted, itemConverted, serialNumberConverted };

            itemList.Add(array);
        }

        public void DecodeBinaryEPC(string binaryEPC)
        {
            string EPCNoHeader = binaryEPC.Remove(0, 11);

            string partitionString = EPCNoHeader.Substring(0, 3);
            int partitionInt = Convert.ToInt32(partitionString, 2);

            int companyPrefixSize = PartitionValueTableCompany[partitionInt];
            int itemPrefixSize = PartitionValueTableItem[partitionInt];

            string EPCNoPartition = EPCNoHeader.Remove(0, 3);
            string company = EPCNoPartition.Substring(0, companyPrefixSize);

            string EPCNoCompany = EPCNoPartition.Remove(0, companyPrefixSize);
            string item = EPCNoCompany.Substring(0, itemPrefixSize);

            string serialNumber = EPCNoCompany.Remove(0, itemPrefixSize);
            ConvertBinaryStringsToInt(company, item, serialNumber);
        }

        public void FindSerialNumberAndCountOfMilkaOreo(int itemReference)
        {
            int oreoCount = 0;
            foreach (var item in itemList)
            {
                if (item[1] == itemReference)
                {
                    oreoCount++;
                    Console.WriteLine("Milka Oreo Serial Number: {0}; Company prefix : {1}", item[2], item[0]);
                }
            }
            Console.WriteLine("Number of milka oreos in the market: {0}", oreoCount);
        }

    }
}
