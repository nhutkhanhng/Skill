using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/Decision/CoolDown")]
    public class Cooldown : Decision
    {
        public override bool Decide(ICharacter controller)
        {
            return false;
        }
    }
}
