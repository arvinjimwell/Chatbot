using Chatbot;
using static System.Console;
using static Chatbot.Alphabet;
Alphabet a = new();
do
{
    WriteLine("Enter:");
    string message = ReadLine()!;
    if (!string.IsNullOrEmpty(message))
    {
        WriteLine(a.Predict(message));
    }
} while (true);

