using UnityEngine;
using System.Collections.Generic;
using Suture;

namespace NPBehave
{
    public class SyncContext
    {
        private Dictionary<string, Blackboard> blackboards = new Dictionary<string, Blackboard>();

        private Clock clock = new Clock();
        

        public  Clock GetClock()
        {
            return clock;
        }

        public  Blackboard GetSharedBlackboard(string key)
        {
            if (!blackboards.ContainsKey(key))
            {
             blackboards.Add(key, new Blackboard(clock));
            }
            return blackboards[key];
        }



       public void Update()
        {
            clock.Update();
        }
    }
}