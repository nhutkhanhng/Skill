using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public abstract class Decision : ScriptableObject
    {
        //public abstract void Init(ICharacter controller);
        public abstract bool Decide(ICharacter controller);
        //public abstract void Destroy(ICharacter controller);
    }
}