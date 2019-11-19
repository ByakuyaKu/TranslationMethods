using System;
using System.IO;
using System.Collections.Generic;


namespace EpsilonRemovalRules
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Char> Q = new List<char>();

            List<Rules> Grammar = new List<Rules>();

            Grammar = Rules.ReadRulesInTxt();

            Console.WriteLine("Input grammar:");
            foreach (var item in Grammar)
                Console.WriteLine(item.leftSide + " -> " + item.rightSide);

            for (int i = 0; i < Grammar.Count; i++)
            {
                if (Rules.CheckEps(Grammar[i].rightSide))
                {
                    Grammar[i].isEpsilon = true;
                    Q.Add(Grammar[i].leftSide);
                }
            }

            for (int k = 0; k < Q.Count; k++) 
            {
                for (int i = 0; i < Grammar.Count; i++)
                {
                    for (int j = 0; j < Grammar[i].rightSide.Length; j++)
                    {
                        if (Q[k] == Grammar[i].rightSide[j] && Grammar[i].isEpsilon != true)
                            Grammar[i].counter--;
                    }

                    if (Grammar[i].counter == 0 && Grammar[i].isEpsilon != true)
                    {
                        Q.Add(Grammar[i].leftSide);
                        Grammar[i].isEpsilon = true;
                    }
                }
            }


                Console.WriteLine("Non-eps:");
            foreach (var item in Grammar)
                if (!item.isEpsilon)
                    Console.WriteLine(item.leftSide + " -> " + item.rightSide);

            for (int i = 0; i < Grammar.Count; i++)
                if (Rules.CheckEps(Grammar[i].rightSide))
                    Grammar.RemoveAt(i);

            Console.WriteLine("Before result:");
            foreach (var item in Grammar)
                Console.WriteLine(item.leftSide + " -> " + item.rightSide);

            string s = "";
            List<Char> Tmp = new List<char>(0);
            int z = Grammar.Count;
            for (int i = 0; i < z-1; i++)
            {
                if (Grammar[i].rightSide.Length != 1)
                {
                    foreach (var element in Q)
                        if (Grammar[i].rightSide.Contains(element) && !Tmp.Contains(element))
                            Tmp.Add(element);


                    foreach (var element in Grammar[i].rightSide)
                        if (char.IsLower(element))
                            s += element;

                    string ss = s;
                    if (s.Length != 0)
                        Grammar.Add(new Rules(Grammar[i].leftSide, s));

                    int k = 0;
                    while (k <= Tmp.Count - 1)
                    {
                        for (int j = k; j < Tmp.Count; j++)
                        {
                            //if ((j == 0))
                            //    Grammar.Add(new Rules(Grammar[i].leftSide, ss + Tmp[j]));
                            Grammar.Add(new Rules(Grammar[i].leftSide, s += Tmp[j]));
                        }
                        k++;
                        s = ss;
                    }
                    Tmp.Clear();
                    Grammar.RemoveAt(i);    
                    s = "";
                }
            }

            Console.WriteLine("Result:");
            foreach (var item in Grammar)
                    Console.WriteLine(item.leftSide + " -> " + item.rightSide);

            Console.ReadKey();
        }
    }
}
