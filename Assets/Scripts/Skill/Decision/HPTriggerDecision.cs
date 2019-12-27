using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/Decision/HPTriggerDecision")]
    public class HPTriggerDecision : Decision
    {
        [SerializeField]
        public enum RelativeHP
        {
            Large = 1,
            Equal = 0,
            Smaller = -1,
        }

        public RelativeHP Relative = RelativeHP.Smaller;
        public float Percent = 10;

        public override bool Decide(ICharacter controller)
        {
            Debug.LogError(Mathf.Sign(controller.CurrentHpPercent - Percent));
            return Mathf.Sign(controller.CurrentHpPercent - Percent) != ((float)this.Relative);
        }
    }
}

