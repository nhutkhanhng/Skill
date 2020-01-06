using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class Interrupt : ScriptableObject
    {
        public Ability ofSkill;

        public Func<bool, ICharacter> IsInterup;

        public void Init(Ability skill)
        {
            this.ofSkill = skill;

            //skill.controller
        }

        public bool Is()
        {
            return false;
        }

    }
}
