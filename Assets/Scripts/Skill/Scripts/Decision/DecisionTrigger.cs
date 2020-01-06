using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class DecisionTrigger<Controller> : ScriptableObject
        where Controller : AbilityController
    {
        protected int Counter;

        public System.Action<Controller> TrueRaise;
        public abstract void Init(Controller controller);
        public abstract bool Decide(Controller controller);
        public abstract void Exit(Controller controller);

        public virtual void Reset()
        {
            Counter = 0;
            TrueRaise = null;
        }
    }

    public abstract class SkillDecisionTrigger : DecisionTrigger<AbilityController>
    {

    }
}