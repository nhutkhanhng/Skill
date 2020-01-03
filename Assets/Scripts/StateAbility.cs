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
