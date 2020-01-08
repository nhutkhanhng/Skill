using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{

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

        public virtual void Init(AbilityController controler)
        {

        }

        public virtual void Exit(AbilityController controller)
        {

        }

        public virtual void WhenTriggerIsTrue(AbilityController controller)
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
        public List<AbilityTrigger> Triggers;

        public void TransitionToState(AbilityController controller)
        {
            _IsTrue = true;
            Exit(controller);

            controller.ChangeToState(TrueState);
        }

        public override void WhenTriggerIsTrue(AbilityController controller)
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
        public override void Init(AbilityController controller)
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
        public override void Exit(AbilityController controller)
        {
            CountTriggerIsTrue = 0;

            foreach (var trigger in Triggers)
            {
                trigger.TrueRaise -= WhenTriggerIsTrue;
                trigger.Exit(controller);
            }
        }
    }
}