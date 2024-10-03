namespace Suture
{
    public class EnemyObject:TargetableObject
    {
        protected EnemyData _enemyData;
        
        public override ImpactData GetImpactData()
        {
            return new ImpactData();
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
        }
    }
}