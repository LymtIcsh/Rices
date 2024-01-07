using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace Suture
{
    [BsonIgnoreExtraElements]
    public sealed class Unit : Entity
    {
        /// <summary>
        /// 配置表id
        /// </summary>
        public int ConfigId;

        private Vector3 position;

        /// <summary>
        /// 坐标
        /// </summary>
        public Vector3 Position
        {
            get => this.position;
            set { this.position = value; }
        }

        private Quaternion rotation = Quaternion.identity;

        public Quaternion Rotation
        {
            get => this.rotation;
            set { this.rotation = value; }
        }

        [BsonIgnore]
        public Vector3 Forward
        {
            get => this.Rotation * Vector3.forward;
            set => this.Rotation = Quaternion.LookRotation(value, Vector3.up);
        }
        
// #if !SERVER

        private Vector3 viewPosition; //坐标

        public Vector3 ViewPosition
        {
            get => this.viewPosition;
            set
            {
                this.viewPosition = value;
                GameEntry.Event.FireNow(this,UnitEventArgs.Create(this));
                //Game.EventSystem.Publish(new EventType.ChangePosition() {Unit = this}).Coroutine();
            }
        }
 
        private Quaternion viewRotation = Quaternion.identity;

        public Quaternion ViewRotation
        {
            get => this.viewRotation;
            set
            {
                this.viewRotation = value;
                GameEntry.Event.FireNow(this,UnitEventArgs.Create(this));
            }
        }

// #endif
    }
}