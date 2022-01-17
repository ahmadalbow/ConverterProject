using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ConverterServer
{
    public class Service1 : IService
    {
        //This is the method which the client can call, it takes the amount of the dollars in numbers as string and it returns the amount converted to words as string
        public string ConvertDollarsToText(string amount)
        {
            // In the Task descreption it was not entirel clear which way the amount will be entered, for example 999 999 or 999999 so made the mathod be able to accept both of them
            amount = amount.Replace(" ", "");
            // Here the dollars and the cents will be splited
            string[] InputNumber = amount.Split(',');           
            int Dollars = int.Parse(InputNumber[0]);
            // This Mehtod converts the dollars  
            string ConvertDollars(int Dollar)
            {
                if (Dollar == 1)
                {
                    return NumberToWords(Dollar) + " dollar ";
                }
                return NumberToWords(Dollar) + " dollars ";
            }
            // This Mehtod converts the Cents 
            string ConvertCents(int Cent)
            {
                // Here we check if the cents length is equal to one in order to multiply it by 10, because 0,1 means 10 cents and 0,01 is equal to one cent
                if (InputNumber[1].Length == 1)
                {
                    Cent = 10 * int.Parse(InputNumber[1]);
                }
                else
                {
                    Cent = int.Parse(InputNumber[1]);
                }
                if (Cent == 1)
                {
                    return "and " + NumberToWords(Cent) + " cent";
                }
                if (Cent > 99)
                {
                    Cent = (int)Math.Floor((double)Cent / (Math.Pow(10, InputNumber[1].Length - 2)));
                }
                return "and " + NumberToWords(Cent) + " cents";
            }
            // Here we check if the amount contains cents
            if (InputNumber.Length == 2)
            {
                int cents = int.Parse(InputNumber[1]);
                return ConvertDollars(Dollars) + ConvertCents(cents);
            }
            return ConvertDollars(Dollars);


        }
        /* This Method takes one parameter as integer and it returns it converted to words,
         it works like a recursive function */
        public string NumberToWords(int number)
        {
            // This is a string array which contains the name of the numbers from 1 to 19
            string[] unitsMap = new string[20] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            // This is a string array which contains the name of the tens numbers
            string[] tensMap = new string[10] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            // Here the numbers between 1 and 19 will be converted to words
            if (number < 20)
            {
                return unitsMap[number];
            }
            // Here the numbers between 20 and 99 will be converted to words
            if (number >= 20 && number < 100)
            {
                // Here we check if the numbers is one of the tens in order to return the name of the number without adding zero to it
                if (number % 10 == 0)
                {
                    return tensMap[(int)Math.Floor((decimal)number / 10)];
                }
                else
                {
                    return tensMap[(int)Math.Floor((decimal)number / 10)] + "-" + unitsMap[number % 10];
                }
            }
            // Here the numbers between 100 and 999 will be converted to words
            if (number >= 100 && number < 1000)
            {
                if (number % 100 == 0)
                {
                    return unitsMap[(int)Math.Floor((decimal)number / 100)] + " hundred";
                }
                return unitsMap[(int)Math.Floor((decimal)number / 100)] + " hundred " + NumberToWords(number % 100);
            }
            // Here the numbers between 1000 and 999999 will be converted to words
            if (number >= 1000 && number < 1000000)
            {
                return NumberToWords((int)Math.Floor((decimal)number / 1000)) + " thousand " + NumberToWords(number % 1000);
            }
            // Here the numbers between 1000000 and 999999999 will be converted to words
            if (number >= 1000000 && number < 1000000000)
            {
                return NumberToWords((int)Math.Floor((decimal)number / 1000000)) + " million " + NumberToWords(number % 1000000);
            }
            return "";
        }
    }
}
