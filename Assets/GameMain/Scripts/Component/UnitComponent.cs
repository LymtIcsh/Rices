using System.Collections.Generic;
using System.Linq;

namespace Suture
{
    public static class UnitComponentSystem
    {
        public static void Add(this UnitComponent self, Pet unit)
        {
            self.idUnits.Add(unit.Id, unit);
        }

        public static Pet Get(this UnitComponent self, long id)
        {
            Pet unit;
            self.idUnits.TryGetValue(id, out unit);
            return unit;
        }

        public static void Remove(this UnitComponent self, long id)
        {
            Pet unit;
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

        public static Pet[] GetAll(this UnitComponent self)
        {
            return self.idUnits.Values.ToArray();
        }
        
        public static void OnLSF_Tick(this UnitComponent self)
        {
            
        }
    }
    public class UnitComponent : EntityBase
    {
        public Dictionary<long, Pet> idUnits = new Dictionary<long, Pet>();

#if !SERVER
        public Pet MyUnit;
#endif
        public void LSF_Tick()
        {
            this.OnLSF_Tick();
        }
    }
}