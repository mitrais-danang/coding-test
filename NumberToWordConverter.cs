using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest
{
    public static class NumberToWordConverter
    {


        //Because we want to process the by every 3 digit, make 3 list for each number level.

        //list for unit level.
        private static List<string> units = new List<string>(new string[] { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN", "TWENTY" });
        // list for tens level .
        private static List<string> tens = new List<string>(new string[] { "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY", "" });
        // because the hundreds is increased every 3 digit puth the hundreds value only on multiples of three index.
        private static List<string> hundreds = new List<string>(new string[] { "", "", "HUNDRED", "THOUSAND", "", "", "MILLION", "", "", "BILLION", "", "", "TRILLION", "", "", "QUADRILLION", "", "", "QUINTILLION", "", "", "SEXTILLION", "", "", "SEPTILLION", "", "", "OCTILLION" });


        public static void ConvertToWord(string param,out string convertWordResult)
        {


            try
            {
                string result = "";

                //split input to divide between dollars value and cents. 
                var splitdecimals = param.Split('.');

                // take zero index as dollars
                var dollars = splitdecimals[0];
                dollars = sanitizeInput(dollars);
               

                // because the lowest hundreds level is on index two initialize hundredcounter to 3
                int hundredcounter = 3;
                string unit = "";
                string ten = "";
                string hundred = "";


                //Process the dollars

                for (int i = 0; i < dollars.Length; i = i + 3)
                {

                    //process every three number
                    var number = int.Parse(dollars.Substring(i, 3));

                    //case for the number if it's value less than 20 because it's still can be handled by units level
                    if (number <= 20)
                    {
                        unit = units[number];
                        result = concatWord(unit, result);
                    }
                    //case for the number if it's length is two digit
                    else if (number > 20 && number < 100)
                    {
                        ten = getTenWord(number, 0, 1);
                        result = concatWord(ten, result);
                        unit = getUnitWord(number, 1, 1);
                        result = concatWordHypens(unit, result);
                    }
                    //case for the number if it's value less than 120 because must be handled be handled by units level
                    else if (number > 100 && number <= 120)
                    {
                        unit = getUnitWord(number, 0, 1);
                        result = concatWord(unit, result);
                        hundred = getHundredWord(2);
                        result = concatWord(hundred, result);
                        result = concatWordAnd(result) + " ";
                        result = result + getUnitWord(number, 1, 2);

                    }
                    else
                    {
                        unit = getUnitWord(number, 0, 1);
                        result = concatWord(unit, result);
                        hundred = getHundredWord(2);
                        result = concatWord(hundred, result);
                        if (number % 100 != 0)
                        {
                            result = concatWordAnd(result);
                        }
                        if (getIntegerDigit(number, 1, 2) < 20)
                        {

                            unit = getUnitWord(number, 1, 2);
                            result = concatWord(unit, result);
                        }
                        else
                        {
                            ten = getTenWord(number, 1, 1);
                            result = concatWord(ten, result);
                            unit = getUnitWord(number, 2, 1);
                            result = concatWordHypens(unit, result);

                        }

                    }


                    hundred = getHundredWord(dollars.Length - hundredcounter);
                    result = concatWord(hundred, result);
                    hundredcounter = hundredcounter + 3;




                }
                result = result + " DOLLARS";

                //Process the cents

                if (splitdecimals.Length > 1)
                {
                    if (splitdecimals[1].Length > 2)
                    {
                        result = "Input Format Not In (dolars.cents)";
                    }
                    else
                    {
                        result = concatWordAnd(result);
                        var number = int.Parse(splitdecimals[1]);
                        //case for the number if it's value less than 20 because it's still can be handled by units level
                        if (number <= 20)
                        {
                            unit = units[number];
                            result = concatWord(unit, result);
                        }
                        else if (number > 20 && number < 100)
                        {
                            ten = getTenWord(number, 0, 1);
                            result = concatWord(ten, result);
                            unit = getUnitWord(number, 1, 1);
                            result = concatWordHypens(unit, result);
                        }

                        result = result + " CENTS";

                    }
                    

                    
                }
                convertWordResult = result;
            }
            catch (Exception e)
            {
                convertWordResult = "Input Format Not In (dolars.cents)";
            }
        }


        private static string concatWord(string param, string result)
        {
            if (param != "")
            {
                result = result + " " + param;
            }
            return result;
        }


        private static string concatWordHypens(string param, string result)
        {
            if (param != "")
            {
                result = result + "-" + param;
            }
            return result;
        }

        private static string concatWordAnd(string param)
        {
            return param + " AND";
        }


        private static string getUnitWord(int number, int start, int end)
        {
            return units[getIntegerDigit(number, start, end)];
        }

        private static string getTenWord(int number, int start, int end)
        {
            return tens[getIntegerDigit(number, start, end)];
        }
        private static string getHundredWord(int index)
        {
            return hundreds[index];
        }

        private static int getIntegerDigit(int number, int start, int end)
        {
            return int.Parse(number.ToString().Substring(start, end));
        }

        private static string sanitizeInput(string input)
        {
            //check if the vallue of the dollars is not zero or less.
            if (input[0] == '0')
            {
                return "Input Format Not In (dolars.cents)";
            }
            // because we want to process the number by every 3 digit, we need to make the input length is multiples of 3.
            if (input.Length % 3 == 1)
            {
                input = "00" + input;
            }
            else if (input.Length % 3 == 2)
            {
                input = "0" + input;
            }

            return input;

        }


    }
}
