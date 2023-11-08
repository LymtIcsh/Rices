using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suture
{
    public class LoadPatternEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadPatternEventArgs).GetHashCode();

        public LoadPatternEventArgs()
        {
            GameMode = GameMode.GameStop;
        }

        public GameMode GameMode
        {
            get;
            private set;
        }

        public (int, string, Vector3) InitPetData
        {
            get;
            private set;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public object UserData
        {
            get;
            private set;
        }

        public static LoadPatternEventArgs Create(GameMode gameMode, (int, string, Vector3) InitPetData,object userData = null)
        {
            LoadPatternEventArgs ne = ReferencePool.Acquire<LoadPatternEventArgs>();
            ne.GameMode = gameMode;
            ne.UserData = userData;
            ne.InitPetData = InitPetData;
            return ne;
        }


        public override void Clear()
        {
            GameMode = GameMode.GameStop;
        }
    }

}
