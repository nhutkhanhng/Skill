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
    public CCharacter owner;

    public AbilityController _Ability;
    
    public CBehaviour currentBehaviour;

    public CastSkill Casting;

    public bool IsPerformWhenInterrupt;
    public bool CanLaunchWhenPerform;

    public PerformSkill Perform;
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

    public void Interrupt()
    {
        Debug.LogError("Interrupt");

        if (this.currentBehaviour == Casting)
        {

            Debug.LogError(this.currentBehaviour.Progress());

            if (this.IsPerformWhenInterrupt)
            {
                this.currentBehaviour.Force(_Ability);
                ChangeToPerform();
            }
            else
                this.SkillComplete();
        }
        else
        if(this.currentBehaviour == Perform)
        {
            if(CanLaunchWhenPerform)
            {
                this.currentBehaviour.Force(_Ability);
            }

            this.SkillComplete();
        }
    }
    public void Init()
    {
        if (_Ability != null)
        {
            if (owner != null)
            {
                this._Ability._Owner = owner;
            }
        }

        if ( Casting != null)
        {
            Casting.DoExit = null;
            Casting.DoExit += ChangeToPerform;
            //Casting.DoInterrupt += Interrupt;
        }

        if (Perform != null)
        {
            Perform.DoExit = null;
            Perform.DoExit += SkillComplete;
            //Perform.DoInterrupt += Interrupt;
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

    public void DoUpdate(float deltaTime)
    {
        if (this.currentBehaviour != null)
        {
            this.currentBehaviour.DoUpdate(this._Ability, deltaTime);
        }
    }
    // Update is called once per frame
    void Update()
    {
        DoUpdate(Time.deltaTime);
    }
}
