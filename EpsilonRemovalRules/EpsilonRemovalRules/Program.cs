using System;
using System.IO;
using System.Collections.Generic;


namespace EpsilonRemovalRules
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<Char> Q = new Queue<char>();

            List<Rules> Grammar = new List<Rules>();

            Grammar = Rules.ReadRulesInTxt();

            Console.WriteLine("Input grammar:");
            foreach (var item in Grammar)
                Console.WriteLine(item.leftSide + " -> " + item.rightSide);

            for (int i = 0; i < Grammar.Count; i++)
            {
                if (Rules.CheckEps(Grammar[i].rightSide))
                {
                    //Grammar[i].isEpsilon = true;
                    Q.Enqueue(Grammar[i].leftSide);
                    Grammar.RemoveAt(i);
                }
                else if (Rules.CheckLowerChars(Grammar[i].rightSide))
                    Grammar.RemoveAt(i);
            }

            while (Q.Count != 0)
            {
                var element = Q.Dequeue();

                for (int i = 0; i < Grammar.Count; i++)
                {
                    for (int j = 0; j < Grammar[i].rightSide.Length; j++)
                    {
                        if (element == Grammar[i].rightSide[j])
                            Grammar[i].counter--;
                    }

                    if (Grammar[i].counter == 0)
                    {
                        Q.Enqueue(Grammar[i].leftSide);
                       // Grammar[i].isEpsilon = true;
                        Grammar.RemoveAt(i);
                    }
                }
            }

            Console.WriteLine("Result");
            foreach (var item in Grammar)
                //if (!item.isEpsilon)
                    Console.WriteLine(item.leftSide + " -> " + item.rightSide);

            Console.ReadKey();
        }
    }
}
