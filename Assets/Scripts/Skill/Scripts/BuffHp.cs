using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{

    [CreateAssetMenu(menuName = "KSkill/Action/BuffHP")]
    public class BuffHp : Action
    {
        public float HP = 500;
        public override void Act(ICharacter controller, List<ICharacter> targetBehaviour)
        {
            Debug.LogError("Do buff");

            if (targetBehaviour.Available())
            {
                foreach (var target in targetBehaviour)
                {
                    target.SkillFunction.ReceiveBuffHP(this.HP);
                }
            }
        }
    }

}