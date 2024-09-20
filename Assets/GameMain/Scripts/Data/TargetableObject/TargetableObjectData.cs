using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Suture
{
    [Serializable]
    public abstract class TargetableObjectData : EntityData
    {
        protected TargetableObjectData() : base()
        {
        }
    }
}