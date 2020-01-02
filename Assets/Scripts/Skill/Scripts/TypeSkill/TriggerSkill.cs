using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill
{
    /// <summary>
    /// Cái class này hoạt động như sau
    /// Check 
    /// </summary>
    [CreateAssetMenu(menuName = "KSkill/TriggerSkill")]
    public class TriggerSkill : Ability
    {
        public override void Initialize(ICharacter controller)
        {
            this.controller = controller;

            if (Transitions.Available())
            {
                foreach (var trans in Transitions)
                {
                    trans.Init(controller);
                }
            }
        }

        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override void Procesing()
        {
            
        }

        public override void ExecuteEnterState()
        {
            throw new System.NotImplementedException();
        }
    }
}
