using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class DecisionTrigger<V> : Decision<V>
    {
        protected int Counter;

        public System.Action<V> TrueRaise;
        public abstract void Init(V controller);

        //public abstract bool Decide(ICharacter controller);
        public abstract void Exit(V controller);

        public virtual void Reset()
        {
            Counter = 0;
            TrueRaise = null;
        }
    }
}