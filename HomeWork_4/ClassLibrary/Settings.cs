using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ClassLibrary
{
   public class Settings
    {
        [JsonPropertyName("primesFrom")]
        public int PrimesFrom { get; set; }

        [JsonPropertyName ("primesTo")]
        public int PrimesTo { get; set; }
    }
}
