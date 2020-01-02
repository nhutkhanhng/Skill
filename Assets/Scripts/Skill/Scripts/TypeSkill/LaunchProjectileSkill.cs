using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/Projectile/SingleProjectile")]
    public class LaunchProjectileSkill : Ability
    {
        [Header("When projectile hit target")]
        public List<Action> OnHit;
        [Header("When projectile be destroyed -- despawn")]
        public List<Action> OnDestroy;

        public Vector3 EndPoint;

        [Header("Just Editor -- for init")]
        [SerializeField] public Projectile projectile;

        [HideInInspector]public Projectile _Projectile;
        protected Projectile CreatePrjectile()
        {
            //return Object.Instantiate(projectile);

            return this.projectile;
        }

        protected void OnProjectileHit(List<ICharacter> targets)
        {
            if (this.OnHit.Available())
            {
                for(int i = 0; i < this.OnHit.Count; i++)
                {
                    this.OnHit[i].Act(this.controller, targets);
                }
            }
        }

        public override void EnterState()
        {
            _Projectile = CreatePrjectile() as Projectile;

            _Projectile.OnHit += OnProjectileHit;
            _Projectile.OnDestroy += ExitState;

            _Projectile.path.Init(this.controller.Position, this.EndPoint);

            _Projectile.DoLaunch();
        }

        public override void ExitState()
        {
            projectile.OnHit -= OnProjectileHit;
            projectile.OnDestroy -= ExitState;
        }

        public override void Procesing()
        {
            projectile.OnUpdate();
        }

        public override void ExecuteEnterState()
        {
            throw new System.NotImplementedException();
        }
    }
}
