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
public class AbilityController : MonoBehaviour, ISkillController
{
    public ICharacter controller;
    public StateAbility currentState;
    // Start is called before the first frame update
    public StateAbility InitState;
    public StateAbility endState;

    public void Test()
    {
        ////ICharacter controller = new ICharacter();
        //Type myType = typeof(ICharacter);

        //PropertyInfo[] allproperties = myType.GetProperties();

        //foreach(var pro in allproperties)
        //{
        //    Debug.LogError(pro.PropertyType.GetTypeInfo());
        //}
        //PropertyInfo myFieldInfo = myType.GetProperty("IdTeam"
        //    /*,BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public*/);

        //// Display the string before applying SetValue to the field.
        //Debug.Log(string.Format("\nThe field value of myString is \"{0}\".",
        //myFieldInfo.GetValue(controller)));
        //// Display the SetValue signature used to set the value of a field.
        //Debug.Log("Applying SetValue(Object, Object).");

        //// Change the field value using the SetValue method. 
        //myFieldInfo.SetValue(controller, Convert.ChangeType(10, myFieldInfo.PropertyType.GetTypeInfo().AsType()));
        //// Display the string after applying SetValue to the field.
        //Debug.Log(string.Format("The field value of mystring is \"{0}\".",
        //    myFieldInfo.GetValue(controller)));
    }
    public void Init()
    {
        TransitionToState(InitState);
    }
    public void OnExitState()
    {
        
    }

    public void TransitionToState(StateAbility state)
    {
        currentState = state;
        if (currentState)
            currentState.EnterState(controller);
    }
    public Transition CheckTransition(ICharacter controller)
    {
        int decisionSucceeded = 0;
        for (int i = 0; i < currentState.Transitions.Count; i++)
        {
            decisionSucceeded += currentState.Transitions[i].IsTrue ? 1 : 0;

            if (decisionSucceeded > 0)
                return currentState.Transitions[i];
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
}
