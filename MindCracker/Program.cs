using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MindCracker
{
    class Program
    {
        public static int gl_DigitLowerBound = 0;
        public static int gl_DigitUpperBound = 10;
        public static int gl_SolvedCriteria = 4;
        public static int gl_Seed = Environment.TickCount;

        public class ValidationResult
        {
            private int countHasRightPlace;
            private int countHasRightValue;

            public int CountHasRightPlace
            {
                get { return countHasRightPlace; }
                set { countHasRightPlace = CountHasRightPlace; }
            }

            public int CountHasRightValue
            {
                get { return countHasRightValue; }
                set { countHasRightValue = CountHasRightValue; }
            }

            public ValidationResult()
            {
                countHasRightPlace = 0;
                countHasRightValue = 0;
            }

            public ValidationResult(int CountPlace, int CountValue)
            {
                countHasRightPlace = CountPlace;
                countHasRightValue = CountValue;
            }
        }

        public class Number
        {
            private int firstDigit;
            private int secondDigit;
            private int thirdDigit;
            private int fourthDigit;

            public int FirstDigit
            {
                get { return firstDigit; }
                set { firstDigit = FirstDigit; }
            }

            public int SecondDigit
            {
                get { return secondDigit; }
                set { secondDigit = SecondDigit; }
            }

            public int ThirdDigit
            {
                get { return thirdDigit; }
                set { thirdDigit = ThirdDigit; }
            }

            public int FourthDigit
            {
                get { return fourthDigit; }
                set { fourthDigit = FourthDigit; }
            }

            public Number()
            {
                Random rnd = new Random(gl_Seed);
                firstDigit = rnd.Next(gl_DigitLowerBound, gl_DigitUpperBound);
                secondDigit = rnd.Next(gl_DigitLowerBound, gl_DigitUpperBound);
                thirdDigit = rnd.Next(gl_DigitLowerBound, gl_DigitUpperBound);
                fourthDigit = rnd.Next(gl_DigitLowerBound, gl_DigitUpperBound);

                gl_Seed++;
            }

            public Number(int FstDigit, int SndDigit, int TrdDigit, int FthDigit)
            {
                firstDigit = FstDigit;
                secondDigit = SndDigit;
                thirdDigit = TrdDigit;
                fourthDigit = FthDigit;
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start new puzzle...");
            Console.ReadKey();
            Number puzzle = new Number();
            //Console.WriteLine("Number: {0}{1}{2}{3}", puzzle.FirstDigit, puzzle.SecondDigit, puzzle.ThirdDigit, puzzle.FourthDigit);

            Console.WriteLine("Try to guess this 4 digit number. You have 7 tries.");
            for (int i = 1; i < 8; i++)
            {
                string enteredValue = String.Empty;
                while (true)
                {
                    Console.Write("Try #{0}: ", i);
                    string value = Console.ReadLine();
                    Regex rgx = new Regex(@"^\d{4}$");
                    Match match = rgx.Match(value);
                    if (match.Success) 
                    {
                        enteredValue = match.Value;
                        break;
                    }
                    Console.WriteLine("\r\nIncorrect input!\r\n");
                }
                Number enteredNumber = new Number();
                string str = enteredValue.Substring(0, 1);

                enteredNumber = new Number(Convert.ToInt32(enteredValue.Substring(0, 1)), Convert.ToInt32(enteredValue.Substring(1, 1)),
                    Convert.ToInt32(enteredValue.Substring(2, 1)), Convert.ToInt32(enteredValue.Substring(3, 1)));

                //int val = Convert.ToInt32(str);
                //enteredNumber.FirstDigit = val;
                //str = enteredValue.Substring(1, 1);
                //val = Convert.ToInt32(str);
                //enteredNumber.SecondDigit = val;
                //enteredNumber.ThirdDigit = Convert.ToInt32(enteredValue.Substring(2, 1));
                //enteredNumber.FourthDigit = Convert.ToInt32(enteredValue.Substring(3, 1));

                ValidationResult res = ValidateEnteredValue(puzzle, enteredNumber);
                if (IsPuzzleSolved(res))
                {
                    Console.WriteLine("Congratulations!!! You are victorious!!! \r\n");
                    break;
                }
                else Console.WriteLine("Nice! At right place: {0}; Has right value: {1}\r\n", res.CountHasRightPlace, res.CountHasRightValue);
            }

            Console.WriteLine("Number: {0}{1}{2}{3}", puzzle.FirstDigit, puzzle.SecondDigit, puzzle.ThirdDigit, puzzle.FourthDigit);
            Console.WriteLine("Good bye! See you soon! Now press any key to quit...");
            Console.ReadKey();
        }

        private static bool IsPuzzleSolved(ValidationResult Result)
        {
            if (Result.CountHasRightPlace == gl_SolvedCriteria &&
                Result.CountHasRightValue == gl_SolvedCriteria) return true;
            else return false;
        }

        private static ValidationResult ValidateEnteredValue(Number Puzzle, Number EnteredNumber)
        {
            int cPlace = 0;
            int cValue = 0;

            List<int> puzzleArray = new List<int>();
            puzzleArray.Add(Puzzle.FirstDigit);
            puzzleArray.Add(Puzzle.SecondDigit);
            puzzleArray.Add(Puzzle.ThirdDigit);
            puzzleArray.Add(Puzzle.FourthDigit);

            if (Puzzle.FirstDigit == EnteredNumber.FirstDigit) cPlace++;
            if (Puzzle.SecondDigit == EnteredNumber.SecondDigit) cPlace++;
            if (Puzzle.ThirdDigit == EnteredNumber.ThirdDigit) cPlace++;
            if (Puzzle.FourthDigit == EnteredNumber.FourthDigit) cPlace++;

            foreach (int dgt in puzzleArray)
            {
                if (EnteredNumber.FirstDigit == dgt)
                {
                    cValue++;
                    puzzleArray.Remove(dgt);
                    break;
                }
            }
            foreach (int dgt in puzzleArray)
            {
                if (EnteredNumber.SecondDigit == dgt)
                {
                    cValue++;
                    puzzleArray.Remove(dgt);
                    break;
                }
            }
            foreach (int dgt in puzzleArray)
            {
                if (EnteredNumber.ThirdDigit == dgt)
                {
                    cValue++;
                    puzzleArray.Remove(dgt);
                    break;
                }
            }
            foreach (int dgt in puzzleArray)
            {
                if (EnteredNumber.FourthDigit == dgt)
                {
                    cValue++;
                    puzzleArray.Remove(dgt);
                    break;
                }
            }            

            return new ValidationResult(cPlace, cValue);
        }
    }
}
