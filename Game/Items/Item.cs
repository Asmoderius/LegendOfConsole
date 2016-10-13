using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Item
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }

        public bool Stackable { get; set; }
        private int stack = 1;
        public Item(string name, string description, int value, bool stackable = false)
        {
            this.Name = name;
            this.Value = value;
            this.Description = description;
            this.Stackable = stackable;
        }

        public override string ToString()
        {
            string s = Name + " - " + Description + " x"+stack; 
            return s;
        }

        public void AddToStack()
        {
            if(Stackable)
            {
                stack++;
            }
        }

        public void ReduceStack()
        {
            if(Stackable)
            {
                stack--;
            }
        }

        public int GetStackCount()
        {
            return this.stack;
        }


    }
}
