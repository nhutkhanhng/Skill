using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/Decision/HPTriggerDecision")]
    public class HPTriggerDecision : DecisionTrigger
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
            if(Mathf.Sign(controller.CurrentHpPercent - Percent) == ((float)this.Relative))
            {
                this.Counter++;
                if (this.Counter == 1)
                {
                    TrueRaise?.Invoke(controller);
                }

                return true;
            }

            return false;
        }

        public override void Init(ICharacter controller)
        {
            this.Reset();

            if (controller is CCharacter)
            {
                ((CCharacter)controller).OnHPChange += () =>
                {
                    Decide(controller);
                };
            }
        }

        public override void Exit(ICharacter controller)
        {
            
        }
    }
}

