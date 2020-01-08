using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [CreateAssetMenu(menuName = "KSkill/Trigger/HPTrigger")]
    public class HPTriggerSkill : SkillTrigger
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

        public override bool Decide(Skill controller)
        {
            Debug.LogError((Mathf.Sign(controller.owner.CurrentHpPercent /*- Percent*/)
                /* == ((float)this.Relative))*/));

            if (Mathf.Sign(controller.owner.CurrentHpPercent - Percent) == ((float)this.Relative))
            {
                // Tránh gọi quá nhiều lần mà Skill đó không chuyển trạng thái
                this.Counter++;
                if (this.Counter == 1)
                {
                    TrueRaise?.Invoke(controller);

                    controller.UseSkill();
                    Exit(controller);
                }
                return true;
            }

            return false;
        }


        /// <summary>
        ///  Những chổ kiểu kiểu như này chưa biết làm sao.
        /// </summary>
        public void Check()
        {
            Decide(this.SkillOwner);
        }

        protected Skill SkillOwner;

        public override void Init(Skill controller)
        {
            this.Reset();
            this.SkillOwner = controller;

            if (controller.owner.Available())
            {
                (controller.owner).OnHPChange += Check;
            }
        }

        public override void Exit(Skill controller)
        {
            if(controller.owner.Available())
                controller.owner.OnHPChange -= Check;

            this.SkillOwner = null;
        }
    }
}