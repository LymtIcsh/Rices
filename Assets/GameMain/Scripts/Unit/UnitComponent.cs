using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;

namespace Suture
{
    public static class UnitComponentSystem
    {
        public static void Add(this UnitComponent self, TargetableObject unit)
        {
            self.idUnits.Add(unit.Id, unit);
        }

        public static TargetableObject Get(this UnitComponent self, long id)
        {
            TargetableObject unit;
            self.idUnits.TryGetValue(id, out unit);
            return unit;
        }

        public static void Remove(this UnitComponent self, long id)
        {
            TargetableObject unit;
            self.idUnits.TryGetValue(id, out unit);
            self.idUnits.Remove(id);
            GameEntry.Entity.HideEntity(unit);
           // unit?.IsDead;
        }

        public static void RemoveAll(this UnitComponent self)
        {
            foreach (var unit in self.idUnits)
            {
                GameEntry.Entity.HideEntity(unit.Value);
             //   unit.Value?.Clear();
            }

            self.idUnits.Clear();
        }

        public static void RemoveNoDispose(this UnitComponent self, long id)
        {
            self.idUnits.Remove(id);
        }

        public static TargetableObject[] GetAll(this UnitComponent self)
        {
            return self.idUnits.Values.ToArray();
        }
        
        public static void OnLSF_Tick(this UnitComponent self)
        {
            
        }
    }
    public class UnitComponent : Entity
    {
        public Dictionary<long, TargetableObject> idUnits = new Dictionary<long, TargetableObject>();

#if !SERVER
        public TargetableObject MyUnit;
#endif
        public void LSF_Tick()
        {
            this.OnLSF_Tick();
        }
    }
}