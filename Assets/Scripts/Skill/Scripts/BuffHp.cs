using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{

    [CreateAssetMenu(menuName = "KSkill/Action/BuffHP")]
    public class BuffHp : Action
    {
        protected FlatValue HP = 500;
        public override void Act(ICharacter controller, List<ICharacter> targetBehaviour, IVisitor extra = null)
        {
            Debug.LogError(this.HP.Accept(extra));

            if (targetBehaviour.Available())
            {
                foreach (var target in targetBehaviour)
                {
                    target.SkillFunction.ReceiveBuffHP(this.HP.Accept(extra));
                }
            }
        }
    }

}