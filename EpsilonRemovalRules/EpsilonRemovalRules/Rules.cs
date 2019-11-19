using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace EpsilonRemovalRules
{
    public class Rules
    {
        public Char leftSide;
        public string rightSide;
        public bool isEpsilon;
        public int counter;

        public Rules(Char c, string s)
        {
            leftSide = c;
            rightSide = s;
            counter = s.Length;
            isEpsilon = false;
        }

        public static bool CheckEps(string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (s[i] == 'E')
                    return true;

            return false;
        }

        public static List<Rules> ReadRulesInTxt()
        {
            string path = @"D:\TranslationMethods\EpsilonRemovalRules\text1.txt";
            var CurLine = "";
            List<Rules> Grammar = new List<Rules>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        CurLine = sr.ReadLine();
                        Grammar.Add(new Rules(CurLine[0], CurLine.Substring(1, CurLine.Length -1)));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return Grammar;
        }
    }
}
