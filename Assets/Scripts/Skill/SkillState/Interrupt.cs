using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class Interrupt : ScriptableObject
    {
        [HideInInspector]
        public Skill ofSkill;

        public Func<bool, ICharacter> IsInterup;
        public System.Action whenTrigger;

        public virtual void Init(Skill skill)
        {
            this.ofSkill = skill;
        }

        public abstract bool IsTrigger(Skill _kill);

    }
}
