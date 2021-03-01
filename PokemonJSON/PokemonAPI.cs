using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonJSON
{
    public class PokemonAPI
    {
        public List<ResultsObject> results { get; set; }
    }

    public class ResultsObject
    {
        public string name { get; set; }
        public string url { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}