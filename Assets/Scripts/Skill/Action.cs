using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(ICharacter controller, TargetBehaviour targetBehaviour);
    }
}