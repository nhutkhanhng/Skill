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
        public Decision decision;
        public StateAbility trueState;
        public StateAbility falseState;
    }


    /// <summary>
    /// Cái class này hoạt động như sau
    /// Check 
    /// </summary>
    [CreateAssetMenu(menuName = "KSkill/State")]
    public class StateAbility : ScriptableObject, IFSM
    {
        protected ICharacter controller;
        public TargetBehaviour targetFinder;
        public List<Action> Actions;

        [Header("Condition to change state of skill")]
        public List<Transition> Transitions;

        public delegate void ChangeState(AbilityController controller);
        public ChangeState TransitionToChangeState;

        public Transition CheckTransition(ICharacter controller)
        {
            for (int i = 0; i < Transitions.Count; i++)
            {
                bool decisionSucceeded = Transitions[i].decision.Decide(controller);

                if (decisionSucceeded)
                    return Transitions[i];
            }

            return null;
        }
        public void TransitionToState(ICharacter controller, System.Action callback = null)
        {
            if (CheckTransition(controller) !=null)
            {
                callback?.Invoke();
            }
        }
        public void Initialize(ICharacter obj) { Debug.LogError(this.name); }
        public void TriggerAbility() { }    

        public void DoUpdate(ICharacter controller)
        {
            // TransitionToState(controller);

            // Do Something
        }

        public void EnterState(ICharacter controller)
        {
            this.controller = controller;
            EnterState();
        }
        public void EnterState()
        {
            Debug.LogError("EnterState");

            if(this.Actions.Available())
            {
                for(int i = 0; i < this.Actions.Count; i++)
                {
                    this.Actions[i].Act(this.controller, this.targetFinder);
                }
            }
        }

        public void Procesing()
        {
            
        }

        public void ExitState()
        {
            
        }
    }

}
