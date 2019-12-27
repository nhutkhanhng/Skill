using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/TargetBehaviour/AOE")]
    public class AOETerm : TargetBehaviour
    {
        public override List<ICharacter> Func(ICharacter Caster)
        {
            return null;
        }
    }
}