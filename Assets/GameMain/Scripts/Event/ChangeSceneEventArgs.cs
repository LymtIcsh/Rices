using GameFramework;
using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Suture
{
    public class ChangeSceneEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ChangeSceneEventArgs).GetHashCode();

        public ChangeSceneEventArgs()
        {
            SceneId = 0;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int SceneId
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static ChangeSceneEventArgs Create(int sceneId, object userData = null)
        {
            ChangeSceneEventArgs args = ReferencePool.Acquire<ChangeSceneEventArgs>();
            args.SceneId = sceneId;
            args.UserData = userData;
            return args;
        }


        public override void Clear()
        {
            SceneId = 0;
        }


    }
}
