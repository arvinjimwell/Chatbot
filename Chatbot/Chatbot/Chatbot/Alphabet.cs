using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot;

internal class Alphabet
{
    public List<MessagePrediction> Chatbot = new List<MessagePrediction>()
        {
            new MessagePrediction() {MessagePredictions = "hello", Response = "hi" },
            new MessagePrediction() {MessagePredictions = "how are you", Response = "I'm fine thank you! how about you" },
            new MessagePrediction() {MessagePredictions = "what do you think of this algorithm", Response = "maybe it suck because it lacks info" },
            new MessagePrediction() {MessagePredictions = "is this the price", Response = "The price of what?" },
            new MessagePrediction() {MessagePredictions = "let me ask you something", Response = "No I dont want to" },
            new MessagePrediction() {MessagePredictions = "I dont know why im doing this", Response = "can you teach me how to not know" },
            new MessagePrediction() {MessagePredictions = "this conversation suck", Response = "I'know right" },
            new MessagePrediction() {MessagePredictions = "give me a poem", Response = "shhhhhh someone is watching right now" }
        };
    public string Predict(string sentence)
    {
        string response = string.Empty;
        string bk = sentence;
        sentence = Append(sentence).ToLower();
        double[] vector1 = Vector(sentence);
        double high = default;
        foreach (var m in Chatbot)
        {
            double dotProduct = 0;
            double vec1Length = 0;
            double vec2Length = 0;
            double[] vector = Vector(Append(m.MessagePredictions));
            if (vector.Length > vector1.Length)
            {
                Array.Resize(ref vector, vector1.Length);
            }
            else if(vector.Length < vector1.Length) 
            {
                Array.Resize(ref vector, vector1.Length);
            }

            for (int i = 0; i < vector.Length; i++)
            {
                dotProduct += vector[i] * vector1[i];
                vec1Length += vector[i] * vector[i];
                vec2Length += vector1[i] * vector1[i];
            }

            vec1Length = Math.Sqrt(vec1Length);
            vec2Length = Math.Sqrt(vec2Length);

            m.Score = dotProduct / (vec1Length * vec2Length);
        }
        foreach (var m in Chatbot)
        {
            
            if (m.Score > high)
            {
                if (m.Score > 0.95)
                {
                    response = m.Response;
                }
                high = m.Score;
            }
        }
        if(string.IsNullOrEmpty(response))
        {
            Console.WriteLine( "I'm sorry I'm not that accurate enough!");
            Console.WriteLine("What should you think I should response?");
            Console.Write("Enter: ");
            string t = Console.ReadLine()!;
            if(!string.IsNullOrEmpty(t))
            {
                Chatbot.Add(new() { MessagePredictions = bk, Response = t });
            }
            response = "Thank you I will add it to my database";
        }
        Chatbot.ForEach(o => o.Score = 0);
        return response;
    }

    private double[] Vector(string message)
    {
        int arrayLength = message.Length;
        double[] vectors = new double[arrayLength];
        int i = default;
        foreach (var c in message)
        {
            vectors[i] = c switch
            {
                 'a' => 1,'b' => 2,'c' => 3,'d' => 4, 'e' => 5,'f' => 6,
                 'g' => 7,'h' => 8, 'i' => 9, 'j' => 10, 'k' => 11,
                 'l' => 12, 'm' => 13, 'n' => 14,'o' => 15, 'p' => 16,
                 'q' => 17,'r' => 18,'s' => 19,'t' => 20,'u' => 21,
                 'v' => 22,'w' => 23,'x' => 24,'y' => 25, 'z' => 26, _ => 0
            };
            i++;
        }
        return vectors;
    }
    private string Append(string sentence)
    {
        string[] words = sentence.Split(' ');
        StringBuilder model = new();
        foreach (string word in words)
        {
            foreach (var c in word)
            {
                model.Append(c);
            }
        }
        return model.ToString();
    }

}
