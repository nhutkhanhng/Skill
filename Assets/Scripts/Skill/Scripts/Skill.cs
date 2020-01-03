using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Flags]
public enum EBuff
{

}

[System.Flags]
public enum EDeBuff
{
    // Kiểu như hôn gió của Ahri.
    Charm = 1,
    Disarm = 1 << 1,
    Silent = 1<< 2,
    Slow = 1 << 3,
    Freeze = 1 << 4,
}

public interface ICastingSkill
{
    float StartCasting();
    void Casting();
    void EndCasting();
}

public interface IPerformSkill
{
    float StartPerform();
    void Performing();
    void EndPerform();
}

namespace KSkill
{
    public enum ESKillState
    {
        Init,
        Casting,
        Perform,
    }

    public class DecisionSkillState : Decision<CSKillState>
    {
        public override bool Decide(CSKillState controller)
        {
            return true;
        }
    }

    [System.Serializable]
    public class TransitionSKillState : KTransition<CSKillState>
    {
        public DecisionSkillState _Decisiions;
    }

    [System.Serializable]
    public class CSKillState
    {
        public List<TransitionSKillState> _Transitions;
    }


    public class CPerformSkill : CSKillState
    {

    }

    public class COvertimeSkill : CSKillState
    {

    }


    public partial class Skill : MonoBehaviour, IFSM
    {
        /// <summary>
        /// Tổng hợp điều kiện để có thể start một skill
        /// ==================== Đang phân vân là có nên để skill tự trigger hay là check trong character ================================
        /// == Nghĩ là nên để trong Character
        /// </summary>
        //public List<Decision> Decisions;

        public System.Action<ICharacter> AnimatorSync;
        public System.Action<ICharacter> PauseTime;

        public EState CurrentState;

        /// <summary>
        /// Init casting
        /// </summary>
        public void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public void Procesing()
        {
            throw new System.NotImplementedException();
        }
    }

}