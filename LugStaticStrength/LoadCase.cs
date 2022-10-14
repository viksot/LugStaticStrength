using System;
using System.Collections.Generic;

namespace LugStaticStrength
{
    public class LoadCase
    {
        public int ID { get; }
        public string Title { get; }

        public Load Load { get; }

        public LoadCase(int id, string title, Load load)
        {
            ID = id;
            Title = title;
            Load = load;
        }

        public override string ToString()
        {
            return $"ID: {ID}; Title: {Title}; Load: [{Load.ToString()}]";
        }
    }
}
