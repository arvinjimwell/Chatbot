using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot
{
    internal class MessagePrediction
    {
        public string MessagePredictions { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public double Score { get; set; }
    }
}
