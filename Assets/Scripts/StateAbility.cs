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
            Debug.LogError("EE");
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


    public abstract class Ability : ScriptableObject, IFSM
    {
        public abstract void EnterState();

        public abstract void ExitState();

        public abstract void Procesing();
    }

    /// <summary>
    /// Cái class này hoạt động như sau
    /// Check 
    /// </summary>
    [CreateAssetMenu(menuName = "KSkill/State")]
    public class StateAbility : Ability
    {
        protected ICharacter controller;

        #region Execute
        public TargetBehaviour targetFinder;
        public List<Action> Actions;
        #endregion


        #region Transition handler
        [Header("Condition to change state of skill")]
        public List<TransitionTrigger> Transitions;

        public delegate void ChangeState(AbilityController controller);
        public ChangeState TransitionToChangeState;

        //public Transition CheckTransition(ICharacter controller)
        //{
        //    for (int i = 0; i < Transitions.Count; i++)
        //    {
        //        bool decisionSucceeded = Transitions[i].decision.Decide(controller);

        //        if (decisionSucceeded)
        //            return Transitions[i];
        //    }

        //    return null;
        //}
        //public void TransitionToState(ICharacter controller, System.Action callback = null)
        //{
        //    if (CheckTransition(controller) != null)
        //    {
        //        callback?.Invoke();
        //    }
        //}

        #endregion

        public void Initialize(ICharacter controller)
        {
            this.controller = controller;

            if (Transitions.Available())
            {
                foreach(var trans in Transitions)
                {
                    trans.Init(controller);
                }
            }
        }
        public void TriggerAbility() { }

        public void DoUpdate(ICharacter controller)
        {
            // TransitionToState(controller);

            // Do Something
        }

        public void EnterState(ICharacter controller)
        {
            Initialize(controller);

            EnterState();
        }

        /// <summary>
        /// enter --> processing --> exit
        /// </summary>
        #region State - 
        public override void EnterState()
        {
            if (this.Actions.Available())
            {
                for (int i = 0; i < this.Actions.Count; i++)
                {
                    this.Actions[i].Act(this.controller, this.targetFinder);
                }
            }
        }

        public override void Procesing()
        {

        }

        public override void ExitState()
        {

        }

        #endregion
    }

}
