using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill
{

    [CreateAssetMenu(menuName = "KSkill/Interrupt/CC")]
    public class InterruptByCC : Interrupt
    {
        public override void Init(Skill skill)
        {
            this.ofSkill = skill;

            if(skill._Ability._Owner != null)
            {
                CCharacter character = (CCharacter)skill._Ability._Owner;
                character.OnStun += Check;
            }
        }

        public void Check()
        {
            Debug.LogError("Checked");

            if(IsTrigger(this.ofSkill))
            {
                this.ofSkill.Interrupt();
            }
        }

        public override bool IsTrigger(Skill _kill)
        {
            return true;
        }

    }
}
