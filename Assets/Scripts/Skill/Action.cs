using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    /// <summary>
    /// Action dựa trên danh sách target truyền vào
    /// </summary>
    public abstract class Action : ScriptableObject
    {
        public abstract void Act(ICharacter controller, TargetBehaviour targetBehaviour);
    }
}