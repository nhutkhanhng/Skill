using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KSkill
{
    #region Transition
    [System.Serializable]
    public class KTransition<T> where T : class
    {
        protected bool _IsTrue;
        public bool IsTrue
        {
            get { return _IsTrue; }
        }
        public T TrueState;
        [Header("Hiện tại chưa dùng đâu")]
        public T FalseState;
    }

    public enum TypeCondition : byte
    {
        AND = 1,
        OR = 2,
    }


    /// <summary>
    /// Target hướng tới ở đây là dối tượng cần được kiểm tra.
    /// Mỗi một Concrete Transition sẽ là một hàm check cụ thể cho từng Target.
    /// </summary>
    /// <typeparam name="Target"></typeparam>
    [System.Serializable]
    public class Transition<Target, State> : KTransition<State> where State : class
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
    #endregion
}