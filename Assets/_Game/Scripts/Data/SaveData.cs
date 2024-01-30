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
        public List<DailyTaskData> DailyTasksData;
        public int LogInCount;
        public string LastLogInTime;
    }

    [Serializable]
    public class DailyGiftData
    {
        public bool IsTaked = false;
        public string TakedTime;
    }

    [Serializable]
    public class DailyTaskData
    {
        public bool IsComplete = false;
        public bool IsTaked = false;
    }
}