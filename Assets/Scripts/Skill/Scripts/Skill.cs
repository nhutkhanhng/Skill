using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Flags]
public enum EBuff
{

}

[System.Flags]
public enum EDeBuff
{
    // Kiểu như hôn gió của Ahri.
    Charm = 1,
    Disarm = 1 << 1,
    Silent = 1<< 2,
    Slow = 1 << 3,
    Freeze = 1 << 4,
}

public interface ICastingSkill
{
    float StartCasting();
    void Casting();
    void EndCasting();
}

public interface IPerformSkill
{
    float StartPerform();
    void Performing();
    void EndPerform();
}

public class Skill : MonoBehaviour
{
    public AbilityController _Aility;


    public CBehaviour currentBehaviour;

    public CastSkill Casting;
    public BehaviourInState Perform;
    public void TransitionToBehaviour(CBehaviour next)
    {
        if (next == null)
        {
            Debug.LogError("NULL");
        }

        this.currentBehaviour = next;
        if (this.currentBehaviour != null)
        {
            this.currentBehaviour.Enter(this);
        }
    }


    public void Init()
    {
        if( Casting != null)
        {
            Casting.DoExit = null;
            Casting.DoExit += ChangeToPerform;
        }

        if (Perform != null)
        {
            Perform.DoExit = null;
            Perform.DoExit += SkillComplete;
        }
    }


    public void Exit()
    {
        if (Casting != null)
        {
            Casting.DoExit = null;
        }

        if (Perform != null)
        {
            Perform.DoExit = null;
            Perform.DoExit -= SkillComplete;
        }
    }

    protected bool _isCompleted;
    public bool IsCompleted { get { return _isCompleted; } }

    public void SkillComplete()
    {
        _isCompleted = true;

        this.currentBehaviour = null;
        Debug.LogError("Completed");
    }
    public void ChangeToPerform()
    {
        TransitionToBehaviour(this.Perform);
    }
   

    // Start is called before the first frame update
    void Start()
    {
        Init();
        TransitionToBehaviour(this.Casting);
    }

    private void OnDestroy()
    {
        Exit();
    }
    // Update is called once per frame
    void Update()
    {
        if (this.currentBehaviour != null)
        {
            Debug.Log(currentBehaviour.GetType());
            this.currentBehaviour.DoUpdate(this._Aility, Time.deltaTime);
        }
    }
}
