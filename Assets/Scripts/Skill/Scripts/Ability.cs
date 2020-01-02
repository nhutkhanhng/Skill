using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{

    [System.Serializable]
    public abstract class Ability : ScriptableObject, IFSM
    {
        public ICharacter controller;

        public EState CurrentState = EState.Init;

        [System.NonSerialized]
        protected float m_Progress;

        public float Progress
        {
            get { return m_Progress; }

        }
        public abstract void ExecuteEnterState();
        public virtual void EnterState()
        {
            ExecuteEnterState();
        }

        public virtual void ExitState()
        {

        }

        public virtual void Procesing()
        {

        }

        public List<Interup> Interups;

        public void InitInterups(ICharacter controller)
        {
            foreach (var exception in Interups)
            {
                exception.Init(this);
            }
        }

        #region Transition handler
        [Header("Condition to change state of skill")]
        public List<TransitionTrigger> Transitions;

        public delegate void ChangeState(AbilityController controller);
        public ChangeState TransitionToChangeState;
        #endregion

        public virtual void Initialize(ICharacter controller)
        {
            this.CurrentState = EState.Init;

            this.controller = controller;

            if (Transitions.Available())
            {
                foreach (var trans in Transitions)
                {
                    trans.Init(controller);
                }
            }
        }
        public virtual void TriggerAbility() { }

        public virtual void DoUpdate(ICharacter controller)
        {
            // TransitionToState(controller);

            // Do Something
            Procesing();
        }

        public void EnterState(ICharacter controller)
        {
            Initialize(controller);
            EnterState();
        }
    }

}