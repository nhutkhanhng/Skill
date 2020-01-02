using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public static class CApplyDamage
    {
        public enum EApllyDamageCondition
        {
            None = 0,
            isCritical = 1,
            isAccuracy = 2,
            isEvasion = 4,
            isPierceResistance = 8,
            isPierceShield = 16
        }

        public enum ECrowdControlType : int
        {
            Stun = 0,
            Silent = 1,
            Sleep = 2,
            Tie = 3,
            Immortal = 5,
            CannotBeTargeted = 11,
            CannotBeCastedSkill = 6,
            CannotAttack = 7,
            Hypnotize = 8,
            Charm = 9,
            IgnoreRegenFury = 10
        }

        public static void ApplyDamage(ICharacter caster, ICharacter target,
            int damage, EApllyDamageCondition condition)
        {
            #region Check Exception
            if (caster == null)
            {
                Debug.LogError("Why is caster null?");
                return;
            }
            if (target == null)
            {
                Debug.LogError("Why is target null?");
                return;
            }

            if (!target.IsAlive())
                return;
            #endregion


        }
    }
}