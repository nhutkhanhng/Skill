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
public class AbilityController : MonoBehaviour, ISkillController, ICastingSkill, IPerformSkill
{
    public ICharacter controller;
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
        TransitionToState(InitState);
    }
    public void OnExitState()
    {
        
    }

    public void TransitionToState(Ability state)
    {
        currentState = state;
        if (currentState != null)
            currentState.EnterState(controller);
    }
    public Transition CheckTransition(ICharacter controller)
    {
        int decisionSucceeded = 0;
        if (this.currentState.Transitions.Available())
        {
            for (int i = 0; i < currentState.Transitions.Count; i++)
            {
                decisionSucceeded += currentState.Transitions[i].IsTrue ? 1 : 0;

                if (decisionSucceeded > 0)
                    return currentState.Transitions[i];
            }
        }

        return null;
    }
    public void TransitionToState(ICharacter controller, System.Action callback = null)
    {
        var transition = CheckTransition(controller);

        if (transition != null)
        {
            callback?.Invoke();
            TransitionToState(transition.TrueState);
        }
    }

    public void DoUpdate()
    {
        currentState?.DoUpdate(this.controller);
        TransitionToState(this.controller, null);
    }
    #endregion
    private void Update()
    {
        DoUpdate();
    }
    public void TransitionToState(StateAbility state)
    {
        
    }

    public float StartCasting()
    {
        throw new NotImplementedException();
    }

    public void Casting()
    {
        throw new NotImplementedException();
    }

    public void EndCasting()
    {
        throw new NotImplementedException();
    }

    public float StartPerform()
    {
        throw new NotImplementedException();
    }

    public void Performing()
    {
        throw new NotImplementedException();
    }

    public void EndPerform()
    {
        throw new NotImplementedException();
    }
}
