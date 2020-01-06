using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;
using System;
using System.Reflection;

public enum EConditionTransition : byte
{
    NONE = 0,
    OR = 1,
    AND = 2,

}

public class Example
{
    private string myString;

    public float attribute = 1; 
    public Example()
    {
        myString = "Old value";
    }

    public string StringProperty
    {
        get
        {
            return myString;
        }

        set
        {
            myString = value;
            Execute();
        }
    }

    public void Execute()
    {
        Debug.LogError("ABC");
    }
}

[System.Serializable]
public class AbilityController : MonoBehaviour
{
    // Owner có thể null
    public ICharacter _Owner;
    #region Ability Executor
    public Ability currentState;
    [Space]
    [Header("--------------------- Init --------------------- ")]
    [SerializeField] private Ability _InitState;
    // Start is called before the first frame update
    [HideInInspector] public Ability InitState;
    public Ability endState;

    public void Init()
    {
        InitState = UnityEngine.Object.Instantiate(_InitState);
        ChangeToState(InitState);
    }
    public void OnEnd()
    {
        Debug.LogError("Ended");
    }

    /// <summary>
    /// Cái này được State gọi đến phục thuộc vào Transition/TransitionTrigger
    /// </summary>
    /// <param name="state"></param>
    public void ChangeToState(Ability state)
    {
        if(currentState != null)
        {
            currentState.Exit(this);
        }
        Debug.LogError("Change State");

        currentState = state;
        if (currentState != null)
            currentState.EnterState(this);

        if(currentState == null)
        {
            OnEnd();
        }
    }

    public void TransitionToState(System.Action callback = null)
    {
        if (this.currentState != null)
        {
            var transition = this.currentState.CheckTransition(this._Owner);

            if (transition != null)
            {
                callback?.Invoke();
                ChangeToState(transition.TrueState);
            }
        }
    }

    public void DoUpdate(float deltime)
    {
        if (currentState != null)
        {
            currentState.DoUpdate(this, deltime);
            TransitionToState();

            if (currentState.IsCompleted)
                ChangeToState(null);
        }
    }
    #endregion
    private void Update()
    {
        DoUpdate(Time.deltaTime);
    }
}
