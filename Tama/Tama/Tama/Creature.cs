using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tama
{
    public sealed class Creature : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public string CreatureName { get; set; }
        
        public float Hunger { get; set; }
        public float Thirst { get; set; }
        public float Boredom { get; set; }
        public float Loneliness { get; set; }
        public float Stimulated { get; set; }
        public float Tired { get; set; }


        public Creature() {
            Hunger =1;
            Thirst =1;
            Boredom =1;
            Loneliness =1;
            Stimulated = 1;
            Tired =1;
        }

        private static Creature _creatureInstance;
        public static Creature GetCreature()
        {
            if (_creatureInstance == null)
            {
                _creatureInstance = new Creature();
            }
            return _creatureInstance;
        }

        public void LowerStats()
        {
            float rate = 0.005f;
            Hunger -= rate;
            Thirst -= rate;
            Boredom -= rate;
            Loneliness -= rate;
            Stimulated -= rate;
            Tired -= rate;
        }

        public string BGImageName
        {
            get
            {
                return CalculateOverallHappiness switch
                {
                    >= 2.5f => "happy.png",
                    < 2.5f => "sad.png",
                    _ => throw new Exception("Impossible")
                };
            }
        }

        public string StatColor(float stat)
        {
            return stat switch
            {
                >= 0.7f => "#00FF00",
                >= 0.6f => "#66FF00",
                >= 0.5f => "#CCFF00",
                >= 0.4f => "#FFCC00",
                >= 0.3f => "#FF6600",
                >= 0.2f => "#FF3300",
                >= 0.1f => "#FF0000",
                <= 0.1f => "#FF0000",
                _ => throw new Exception("Impossible")
            };
        }

        public float CalculateOverallHappiness
        {
            get => Hunger + Thirst + Boredom + Loneliness + Stimulated + Tired;
        }

        public void UpdateStat(float stat)
        {
            if (stat < 0)
            {
                stat = 0;
            }
            if (stat > 1)
            {
                stat = 1;
            }
        }

        public void ResetCreature(){
            Hunger = 1;
            Thirst = 1;
            Boredom = 1;
            Loneliness = 1;
            Stimulated = 1;
            Tired = 1;
        }
    }
}
