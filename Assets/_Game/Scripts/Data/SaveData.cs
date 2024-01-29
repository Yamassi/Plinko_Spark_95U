using System;
using System.Collections.Generic;
namespace Tretimi
{
    [Serializable]
    public class SaveData
    {
        public int Money;
        public int Record;
        public int AvailableEnergy;
        public bool IsEnergyInfinity;
        public List<string> TimesToAddEnergy;
        public List<int> AvailableBalls;
        public List<int> AvailableMapColors;

    }

}