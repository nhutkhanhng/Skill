using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/Decision/HPTriggerDecision")]
    public class HPTriggerDecision : AbilityTrigger
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

        public override bool Decide(AbilityController controller)
        {
            Debug.LogError((Mathf.Sign(controller._Owner.CurrentHpPercent /*- Percent*/)
                /* == ((float)this.Relative))*/));

            if(Mathf.Sign(controller._Owner.CurrentHpPercent - Percent) == ((float)this.Relative))
            {
                this.Counter++;
                if (this.Counter == 1)
                {
                    TrueRaise?.Invoke(controller);
                }
                Debug.LogError("TRUE");
                return true;
            }

            return false;
        }

        public override void Init(AbilityController controller)
        {
            this.Reset();

            if (controller._Owner is CCharacter)
            {
                ((CCharacter)controller._Owner).OnHPChange += () =>
                {
                    Decide(controller);
                };
            }
        }

        public override void Exit(AbilityController controller)
        {
            
        }
    }
}

