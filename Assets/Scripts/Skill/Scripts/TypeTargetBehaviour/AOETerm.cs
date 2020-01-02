using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/TargetBehaviour/AOE")]
    public class AOETerm : TargetBehaviour
    {
        public float Range;
        public override List<ICharacter> Func(ICharacter Caster)
        {
            return FindCharacter.FindAOECharacter(Caster, Range);
        }
    }
}