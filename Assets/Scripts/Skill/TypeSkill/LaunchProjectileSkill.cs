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


        public Projectile projectile;

        protected void CreatePrjectil()
        {
            projectile = GameObject.Instantiate(projectile);


            //projectile.OnHit += 
        }

        protected void OnProjectileHit(ICharacter target)
        {
            if (this.OnHit.Available())
            {
                for(int i = 0; i < this.OnHit.Count; i++)
                {
                    //OnHit[i].Act
                }
            }
        }
        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void Procesing()
        {
            throw new System.NotImplementedException();
        }
    }
}
