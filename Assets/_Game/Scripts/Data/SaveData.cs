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
        public List<DailyGiftData> DailyGiftsData;
    }

    [Serializable]
    public class DailyGiftData
    {
        public bool IsTaked = false;
        public string TakedTime;
    }
}