using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    public interface ISkillFunction
    {
        void ReceiveBuffHP(float HP);
    }

    public interface ICharacter
    {
        Vector3 Position { get; }

        float CurrentHpPercent { get; }

        ISkillFunction SkillFunction { get; }
    }

    public interface ISkillController
    {
        void OnExitState();
        void TransitionToState(StateAbility state);
    }

    public enum ETargetBehaviourType
    {
        None = 0,
        IsSelf = 1,
        AOE,
    }

    public abstract class TargetBehaviour : ScriptableObject
    {
        public abstract List<ICharacter> Func(ICharacter Caster);
    }
}