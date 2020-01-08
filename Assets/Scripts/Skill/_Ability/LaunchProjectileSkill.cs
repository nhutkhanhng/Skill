using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/Projectile/SingleProjectile")]
    public class LaunchProjectileSkill : StateAbility
    {
        [Header("When projectile hit target")]
        public List<Action> OnHit;
        [Header("When projectile be destroyed -- despawn")]
        public List<Action> OnDestroy;

        public Vector3 EndPoint;

        [Header("Just Editor -- for init")]
        [SerializeField] public Projectile projectile;

        [HideInInspector] public Projectile _Projectile;
        protected Projectile CreatePrjectile()
        {
            //return Object.Instantiate(projectile);

            return this.projectile;
        }

        protected void OnProjectileHit(List<ICharacter> targets)
        {
            for (int i = 0; i < this.Actions.Count; i++)
            {
                this.Actions[i].Force(_Projectile.owner, targets);

            }

        }

        public override void Initialize(AbilityController controller)
        {
            this.CurrentState = EState.Init;

            if (TriggerTransitions.Available())
            {
                foreach (var trans in TriggerTransitions)
                {
                    trans.Init(controller);
                }
            }
        }

        protected void EndStateWhenProjectileBeDestroyed()
        {
            this._IsCompleted = true;
        }
        public override void EnterState(AbilityController controller)
        {
            base.EnterState(controller);

            _Projectile = CreatePrjectile() as Projectile;
            _Projectile.owner = controller._Owner;

            _Projectile.OnHit += OnProjectileHit;
            _Projectile.OnDestroy += EndStateWhenProjectileBeDestroyed;

            _Projectile.path.Init(controller._Owner.Position, this.EndPoint);

            _Projectile.DoLaunch();
        }

        public override void Exit(AbilityController controller)
        {
            base.Exit(controller);
            projectile.OnHit -= OnProjectileHit;
            projectile.OnDestroy -= EndStateWhenProjectileBeDestroyed;
        }

        public override void DoActions(AbilityController controller, float deltaTime)
        {
            // nothing
        }
        public override void DoUpdate(AbilityController controller, float deltaTime)
        {
            projectile.OnUpdate(deltaTime);
        }
    }
}
