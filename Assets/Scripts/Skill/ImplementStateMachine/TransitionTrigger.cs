using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    /// <summary>
    /// Target hướng tới ở đây là dối tượng cần được kiểm tra.
    /// Mỗi một Concrete Transition sẽ là một hàm check cụ thể cho từng Target.
    /// </summary>
    /// <typeparam name="Target"></typeparam>
    [System.Serializable]
    public class STransition<Target> : KTransition<StateAbility>
    {
        public virtual void Init(Target controler)
        {

        }

        public virtual void Exit(Target controller)
        {

        }

        public virtual void WhenTriggerIsTrue(Target controller)
        {

        }
    }

    [System.Serializable]
    public class TransitionCondition<_Decision, Target> : STransition<Target> 
                                                where _Decision : DecisionTrigger<Target>
    {
        protected int CountTriggerIsTrue;
        [Header("Add | OR bit condition")]
        public TypeCondition condition;
        public List<_Decision> Conditions;

        public void TransitionToState(Target controller)
        {
            _IsTrue = true;
            Exit(controller);
        }

        public override void WhenTriggerIsTrue(Target controller)
        {
            CountTriggerIsTrue++;
            if (condition == TypeCondition.AND)
            {
                if (CountTriggerIsTrue == Conditions.Count)
                {
                    TransitionToState(controller);
                }

                return;
            }

            TransitionToState(controller);
        }
        public override void Init(Target controller)
        {
            CountTriggerIsTrue = 0;
            _IsTrue = false;

            if (Conditions.Available())
            {
                foreach (var trigger in Conditions)
                {
                    trigger.Init(controller);
                    trigger.TrueRaise += WhenTriggerIsTrue;
                }
            }
        }

        public override void Exit(Target controller)
        {
            CountTriggerIsTrue = 0;

            foreach (var trigger in Conditions)
            {
                trigger.TrueRaise -= WhenTriggerIsTrue;
                trigger.Exit(controller);
            }
        }
    }

    [System.Serializable]
    public class TransitionTrigger : TransitionCondition<DecisionTrigger<ICharacter>, ICharacter>
    {

    }
}