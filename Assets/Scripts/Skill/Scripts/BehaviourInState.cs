using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "KSkill/Behaviour/Casting")]
public class BehaviourInState : ScriptableObject
{
    public CCharacter onCharacter;

    public void Enter()
    {
        Debug.LogError("EnterBehaviour");

        this.onCharacter.GetComponent<Renderer>().sharedMaterial.color = Color.red;
    }
    public void DoUpdate(float delTatime)
    {
        
    }


    public void Exit()
    {
        this.onCharacter.GetComponent<Renderer>().sharedMaterial.color = Color.white;
    }
    
}
