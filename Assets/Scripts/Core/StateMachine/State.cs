using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class State<Target> : ScriptableObject
    {
        #region Transition handler
        [Header("Condition to change state of skill")]
        public List<Transition<Target, State<Target>>> Transitions;

        public delegate void ChangeState(AbilityController controller);
        public ChangeState TransitionToChangeState;
        #endregion

        public virtual void Initialize(Target controller)
        {
            if (Transitions.Available())
            {
                foreach (var trans in Transitions)
                {
                    trans.Init(controller);
                }
            }
        }
    }
}
