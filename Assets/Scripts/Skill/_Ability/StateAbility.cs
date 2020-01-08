using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill
{
    public interface IFSM
    {
        void EnterState();
        void Procesing();
        void ExitState();
    }

    public enum EState
    {
        Init = 0,
        Enter = 1,
        Processing = 2,
        ExiState = 3,
    }

    /// <summary>
    /// Cái class này hoạt động như sau
    /// Check 
    /// </summary>
    [CreateAssetMenu(menuName = "KSkill/State/AbilityTrigger")]
    public class StateAbility : Ability
    {
        #region Transition handler
        [Header("Mấy cái tran này là trans thuộc dạng Update liên tục, không phụ thuộc vào bất kì callback nào")]
        [Header("Transition này được controller upadte, chứ không phải chính state xử lí")]
        public List<TransitionCondition> Transitions;

        [Header("Condition to change state of skill")]
        public List<TransitionTrigger> TriggerTransitions;

        public delegate void ChangeState(AbilityController controller);
        public ChangeState TransitionToChangeState;
        #endregion
        public override Transition CheckTransition(ICharacter controller)
        {
            int decisionSucceeded = 0;
            if (this.Transitions.Available())
            {
                for (int i = 0; i < this.Transitions.Count; i++)
                {
                    decisionSucceeded += this.Transitions[i].IsTrue ? 1 : 0;

                    if (decisionSucceeded > 0)
                        return Transitions[i];
                }
            }

            return null;
        }

        public override void Initialize(AbilityController controller)
        {
            base.Initialize(controller);

            if (TriggerTransitions.Available())
            {
                foreach (var trans in TriggerTransitions)
                {
                    trans.Init(controller);
                }
            }
        }

        public override void Exit(AbilityController controller)
        {
            base.Exit(controller);

            if (this.TriggerTransitions.Available())
            {
                foreach (var tran in this.TriggerTransitions)
                {
                    tran.Exit(controller);
                }
            }
        }
    }

}
