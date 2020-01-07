using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KSkill
{
    [System.Serializable]
    public class ActionController
    {

        [System.NonSerialized]
        protected int amount;

        /// <summary>
        /// Interval sẽ được coi là thơi gian CastPoint của một skill bình thường.
        /// </summary>
        [Header("Nếu mà Interval lớn hơn TotalTime hoặc cả 2 == 0 thì thực thi 1 lần đầu rồi thôi")]
        public float interval;
        public float TotalTime;

        [Header("Find target for Buff")]
        public TargetBehaviour targetFinder;
        [Header("Action Buff")]
        public List<Action> Actions;

        protected bool _IsCompleted = false;
        [System.NonSerialized]
        protected float currentOverTime = 0f;

        public bool IsCompleted
        {
            get { return _IsCompleted; }
        }
        public System.Action Done;

        public virtual void DoActions(float deltaTime, ICharacter controller)
        {
            DoUpdate(deltaTime, controller);
        }

        public void Force(ICharacter owner, List<ICharacter> victims)
        {
            if (this.Actions.Available())
            {
                for (int i = 0; i < this.Actions.Count; i++)
                {
                    this.Actions[i].Act(owner, victims);
                }
            }
        }

        public virtual void DoAction(ICharacter controller)
        {
            if (this.Actions.Available())
            {
                for (int i = 0; i < this.Actions.Count; i++)
                {
                    this.Actions[i].Act(controller, this.targetFinder.Func(controller));
                }
            }
        }
        public virtual void Reset()
        {
            if (this.Actions.Available())
            {
                //foreach(var act in this.Actions)
                //{
                //    act.Reset
                //}
            }
        }
        protected void Completed()
        {
            Debug.LogError("Completed");
            _IsCompleted = true;
            Done?.Invoke();
        }
        public virtual void DoUpdate(float deltaTime, ICharacter controller)
        {
            if (this.interval > this.TotalTime 
                || (this.interval == 0 && this.TotalTime == 0))
            {
                Debug.LogError(this.interval + "|||" + this.TotalTime);

                DoAction(controller);
                Completed();

                return;
            }
            /// Làm kiểu này thì sẽ chắc chắn thực hiện đủ số lần cần làm.
            if (Mathf.CeilToInt(this.currentOverTime / interval) >= ( amount + 1 )
                && (this.currentOverTime - this.TotalTime) <= deltaTime)
            {
                amount++;
                Debug.LogError(this.currentOverTime + "|||" + this.interval + "||| " + this.amount);

                DoAction(controller);
            }

            this.currentOverTime += deltaTime;

            if ((this.currentOverTime - this.TotalTime) > deltaTime)
            {
                Completed();
            }
        }
    }


    public class ActionOverTime : ActionController
    { 
        public override void DoUpdate(float deltaTime, ICharacter controller)
        {
            /// Làm kiểu này thì sẽ chắc chắn thực hiện đủ số lần cần làm.
            if (Mathf.CeilToInt(this.currentOverTime / interval) >= amount
                && (this.currentOverTime - this.TotalTime) <= deltaTime && (this.currentOverTime - this.TotalTime) <= 0)
            {
                amount++;

                DoActions(deltaTime, controller);
            }

            this.currentOverTime += deltaTime;
            if ((this.currentOverTime - this.TotalTime) > deltaTime)
            {
                Completed();
            }
        }
    }

    [System.Serializable]
    [CreateAssetMenu(menuName = "KSkill/State/Ability")]
    public class Ability : ScriptableObject
    {
        #region  Attribute ability controller
        public EState CurrentState = EState.Init;

        [System.NonSerialized]
        protected float m_Progress;

        public float Progress
        {
            get { return m_Progress; }

        }

        #endregion

        #region Interrupts
        public List<Interrupt> Interupts;

        public void InitInterups(ICharacter controller)
        {
            foreach (var exception in Interupts)
            {
                exception.Init(this);
            }
        }
        #endregion
       
        #region Actions
        public List<ActionController> Actions;
        protected int counterActionComplete;

        public virtual void DoActions(AbilityController controller, float deltaTime)
        {
            if (this.Actions.Available())
            {
                for (int i = 0; i < this.Actions.Count; i++)
                {
                    if(this.Actions[i].IsCompleted == false)
                        this.Actions[i].DoActions(deltaTime, controller._Owner);
                }
            }
        }
        #endregion
        protected void CounterActionWhenCompleted()
        {
            counterActionComplete++;

            if (this.counterActionComplete == this.Actions.Count)
            {
                this._IsCompleted = true;
            }
        }

        protected void CheckAllActions(AbilityController controller)
        {
            if(this._IsCompleted)
            {
                controller.ChangeToState(null);
            }
        }
        public virtual void Initialize(AbilityController controller)
        {
            this.CurrentState = EState.Init;
            counterActionComplete = 0;

            if (this.Actions.Available())
            {
                foreach(var act in this.Actions)
                {
                    act.Done += CounterActionWhenCompleted;
                }
            }
        }

        public virtual void EnterState(AbilityController controller)
        {
            this.CurrentState = EState.Enter;
            Initialize(controller);
        }

        public virtual void DoUpdate(AbilityController controller, float deltaTime)
        {
            DoActions(controller, deltaTime);
        }

        public virtual Transition CheckTransition(ICharacter controller)
        {
            return null;
        }
        
        protected bool _IsCompleted;
        public bool IsCompleted
        {
            get { return _IsCompleted; }
        }
        // Reset All Transition
        public virtual void Exit(AbilityController controller)
        {
            if (this.Actions.Available())
            {
                foreach (var act in this.Actions)
                {
                    act.Reset();
                }
            }
        }
    }

}