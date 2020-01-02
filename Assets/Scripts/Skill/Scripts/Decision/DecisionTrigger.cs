using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class DecisionTrigger : ScriptableObject
    {
        protected int Counter;

        public System.Action<ICharacter> TrueRaise;
        public abstract void Init(ICharacter controller);
        public abstract bool Decide(ICharacter controller);
        public abstract void Exit(ICharacter controller);

        public virtual void Reset()
        {
            Counter = 0;
            TrueRaise = null;
        }
    }
}