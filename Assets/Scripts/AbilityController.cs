using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;

public enum EConditionTransition : byte
{
    NONE = 0,
    OR = 1,
    AND = 2,

}
[System.Serializable]
public class AbilityController : MonoBehaviour, ISkillController
{
    public ICharacter controller;
    public StateAbility currentState;
    // Start is called before the first frame update
    public StateAbility InitState;
    public StateAbility endState;

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
        currentState.EnterState(controller);
    }
    public Transition CheckTransition(ICharacter controller)
    {
        int decisionSucceeded = 0;
        for (int i = 0; i < currentState.Transitions.Count; i++)
        {
             decisionSucceeded += currentState.Transitions[i].decision.Decide(controller) ? 1 : 0;

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
            TransitionToState(transition.trueState);
        }
    }

    public void DoUpdate()
    {
        currentState.DoUpdate(this.controller);
        TransitionToState(this.controller, null);
    }
}
