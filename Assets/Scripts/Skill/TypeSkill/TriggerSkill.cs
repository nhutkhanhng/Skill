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
        protected ICharacter controller;


        #region Transition handler
        [Header("Condition to change state of skill")]
        public List<TransitionTrigger> Transitions;

        public delegate void ChangeState(AbilityController controller);
        public ChangeState TransitionToChangeState;
        #endregion

        public void Initialize(ICharacter controller)
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
        public void TriggerAbility() { }

        public void DoUpdate(ICharacter controller)
        {
            // TransitionToState(controller);

            // Do Something
        }

        public void EnterState(ICharacter controller)
        {
            Initialize(controller);

            EnterState();
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
    }
}
