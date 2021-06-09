﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.models
{
    public class Choice
    {
        public string desc { get; }
        public float chance { get; }
        public string win { get; }
        public string lose { get; }
        public float[] stats { get; }
        public Buff buff { get; }
        public Item item { get; }

        public Choice(string desc, float chance, string win, string lose, float[] stats, Buff buff, Item item)
        {
            this.desc = desc;
            this.chance = chance;
            this.win = win;
            this.lose = lose;
            this.stats = stats;
            this.buff = buff;
            this.item = item;
        }
    }
}
