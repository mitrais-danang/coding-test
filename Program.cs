


using CodingTest;

string input="";
string result = "";
{
    while (input != "0")
    {
        Console.Write("Enter The Number : ");
        input = Console.ReadLine();
        NumberToWordConverter.ConvertToWord(input, out result);
        Console.Write("Result : ");
        Console.WriteLine(result);
    }

}




