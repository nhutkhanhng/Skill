using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(ICharacter controller);
    }
}