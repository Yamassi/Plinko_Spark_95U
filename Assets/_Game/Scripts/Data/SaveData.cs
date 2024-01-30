using System;
using System.Collections.Generic;

namespace Tretimi
{
    [Serializable]
    public class SaveData
    {
        public int Coins;
        public List<int> AvailableBalls;
        public List<int> AvailableMaps;
    }
}