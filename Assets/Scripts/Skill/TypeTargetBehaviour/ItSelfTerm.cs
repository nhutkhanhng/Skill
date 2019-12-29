using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/TargetBehaviour/ItSelf")]
    public class ItSelfTerm : TargetBehaviour
    {
        public override List<ICharacter> Func(ICharacter Caster)
        {
            return new List<ICharacter>() { Caster };
        }
    }
}