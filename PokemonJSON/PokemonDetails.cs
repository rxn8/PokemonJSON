using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonJSON
{
    public class PokemonDetails
    {
        public int height { get; set; }
        public Sprites sprites { get; set; }
        public int weight { get; set; }
    }

    public class Sprites
    {
        public string front_default { get; set; }
    }
}