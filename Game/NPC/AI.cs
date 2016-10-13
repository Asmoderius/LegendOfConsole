using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class AI
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public bool Friendly { get; set; }
        public char Icon { get; set; }
        public AI(string name, string description, int level, bool friendly, char icon)
        {
            Name = name;
            Description = description;
            Level = level;
            Friendly = friendly;
            Icon = icon; 
        }
    }
}
