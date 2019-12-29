using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;


[CreateAssetMenu(menuName = "KSkill/Action/BuffHP")]
public class BuffHp : Action
{
    public float HP = 500;
    public override void Act(ICharacter controller, TargetBehaviour targetBehaviour)
    {
        var list = targetBehaviour.Func(controller);

        if (list.Available())
        {
            foreach (var target in list)
            {
                target.SkillFunction.ReceiveBuffHP(this.HP);
            }
        }
    }
}
