using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Cypher_Decrypt.Classes
{
    class Column
    {
        private float value;
        private String name;

        public float Value { get => value; set => this.value = value; }
        public string Name { get => name; set => name = value; }

        public Column(float value, String name)
        {
            Value = value;
            Name = name;
        }
    }
}
