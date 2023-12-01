using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicArenaBattle
{
    static class General
    {

        //generates random number between 1 and 10 - for archer versus knigth fight
        public static int GenerateRandom()
        {
            Random rnd = new Random();
            return rnd.Next(1, 11);
        }
        public static void LogEvents(string line)
        {
            Console.WriteLine(line);
        }
    }
}
