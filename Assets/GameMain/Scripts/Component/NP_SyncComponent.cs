using System;
using NPBehave;

namespace Suture
{
    public class NP_SyncComponent:EntityBase
    {
        public SyncContext SyncContext;

        private void Awake()
        {
            SyncContext = new SyncContext();
        }

   

        private void FixedUpdate()
        {
            SyncContext.Update();
        }

        protected override void OnRecycle()
        {
            SyncContext = null;
            base.OnRecycle();
        }
    }
}