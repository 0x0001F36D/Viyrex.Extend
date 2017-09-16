using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Operation;
using System.Threading.Tasks;

namespace Viyrex.Extend.ConsoleDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtraStringArray a = new[] { "1", "2", "3", "4", "5" };
            ExtraStringArray b = "ABCD";
            ExtraString c = "#";

            var result = c + a * 3 + b;

            foreach (string item in result)
                Console.WriteLine(item);
            //output:
            //#111A
            //#222B
            //#333C
            //#444D

            result[0, 0] =
            result[1, 1] =
            result[2, 2] =
            result[3][3] = '@';

            foreach (string item in result)
                Console.WriteLine(item);
            //output:
            //@111A
            //#@22B
            //#3@3C
            //#44@D

            var arranged = result.Arrange(separator: null);
            Console.WriteLine(arranged);
            //output:
            //@111A#@22B#3@3C#44@D

            Console.WriteLine(arranged.Range(startIndex: 0, stopIndex: 50, step: 6));
            //output:
            //@@@@

            ExtraString d = "ABCDEFG";
            ExtraString e = "ABCD_F_";
            Console.WriteLine(d.Similarity(e));
            //output:
            //0.357142857142857
            Console.ReadKey();
            return;
        }
    }
}
