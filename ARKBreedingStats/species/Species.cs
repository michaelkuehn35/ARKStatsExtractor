﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ARKBreedingStats
{
    [DataContract]
    public class Species
    {
        [DataMember]
        public string name;
        [DataMember]
        public double?[][] statsRaw; // without multipliers
        public List<CreatureStat> stats;
        [DataMember]
        public List<ColorRegion> colors; // each creature has up to 6 colorregions
        [DataMember]
        public TamingData taming;
        [DataMember]
        public BreedingData breeding;

        /// <summary>
        /// creates properties that are not created during deserialization. They are set later with the raw-values with the multipliers applied.
        /// </summary>
        public void initialize()
        {
            stats = new List<CreatureStat>();
            double?[][] completeRaws = new double?[8][];
            for (int s = 0; s < 8; s++)
            {
                stats.Add(new CreatureStat((StatName)s));
                completeRaws[s] = new double?[] { 0, 0, 0, 0, 0 };
                if (statsRaw.Length > s && statsRaw[s] != null)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (statsRaw[s].Length > i)
                            completeRaws[s][i] = statsRaw[s][i] != null ? statsRaw[s][i] : 0;
                    }
                }
            }
            statsRaw = completeRaws;
        }
    }
}