﻿using Quake.ValueObjects;

namespace Quake.Entities
{
    public class KillsByMeans
    {
        public int IdGame { get; protected set; }
        public MeansOfDeath MeansOfDeath { get; protected set; }
        public decimal TotalKills { get; protected set; }
        public virtual Game Game { get; protected set; }

        public KillsByMeans(MeansOfDeath meansOfDeath)
        {
            MeansOfDeath = meansOfDeath;
        }

        public void Sum()
        {
            TotalKills++;
        }
    }
}