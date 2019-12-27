using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;

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
        state.Initialize(controller);
    }
    public Transition CheckTransition(ICharacter controller)
    {
        for (int i = 0; i < currentState.Transitions.Count; i++)
        {
            bool decisionSucceeded = currentState.Transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
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
