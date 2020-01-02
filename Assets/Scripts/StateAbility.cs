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
    
    [System.Serializable]
    public class Transition
    {
        protected bool _IsTrue;
        public bool IsTrue
        {
            get { return _IsTrue; }
        }
        public StateAbility TrueState;
        [Header("Hiện tại chưa dùng đâu")]
        public StateAbility FalseState;

        public virtual void Init(ICharacter controler)
        {

        }

        public virtual void Exit(ICharacter controller)
        {
            
        }

        public virtual void WhenTriggerIsTrue(ICharacter controller)
        {

        }
    }
    [System.Serializable]
    public class TransitionCondition : Transition
    {

    }
    [System.Serializable]
    public class TransitionTrigger : Transition
    {
        public enum TypeCondition : byte
        {
            AND = 1,
            OR = 2,
        }


        protected int CountTriggerIsTrue;

        [Header("Add | OR bit condition")]
        public TypeCondition condition;
        public List<DecisionTrigger> Triggers;

        public void TransitionToState(ICharacter controller)
        {
            _IsTrue = true;
            Exit(controller);
        }

        public override void WhenTriggerIsTrue(ICharacter controller)
        {
            CountTriggerIsTrue++;
            if (condition == TypeCondition.AND)
            {
                if (CountTriggerIsTrue == Triggers.Count)
                {
                    TransitionToState(controller);
                }

                return;
            }

            TransitionToState(controller);
        }
        public override void Init(ICharacter controller)
        {
            CountTriggerIsTrue = 0;
            _IsTrue = false;

            if (Triggers.Available())
            {
                foreach (var trigger in Triggers)
                {
                    trigger.Init(controller);
                    trigger.TrueRaise += WhenTriggerIsTrue;
                }
            }
        }
        public override void Exit(ICharacter controller)
        {
            CountTriggerIsTrue = 0;

            foreach (var trigger in Triggers)
            {
                trigger.TrueRaise -= WhenTriggerIsTrue;
                trigger.Exit(controller);
            }
        }
    }

    /// <summary>
    /// Cái class này hoạt động như sau
    /// Check 
    /// </summary>
    [CreateAssetMenu(menuName = "KSkill/State")]
    public class StateAbility : Ability
    {
        #region Execute
        [Header("Find target for Buff")]
        public TargetBehaviour targetFinder;
        [Header("Action Buff")]
        public List<Action> Actions;
        #endregion

        /// <summary>
        /// enter --> processing --> exit
        /// </summary>
        #region State - 
        public override void ExecuteEnterState()
        {
            if (this.Actions.Available())
            {
                for (int i = 0; i < this.Actions.Count; i++)
                {
                    this.Actions[i].Act(this.controller, this.targetFinder.Func(this.controller));
                }
            }
        }

        #endregion
    }

}
