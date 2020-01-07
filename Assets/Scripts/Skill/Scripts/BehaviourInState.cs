using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;

//[System.Serializable]
public class CBehaviour
{

    #region Interrupts
    public List<Interrupt> Interupts;

    public void InitInterups(Skill controller)
    {
        Debug.LogError(this.GetType());

        foreach (var exception in Interupts)
        {
            exception.Init(controller);
        }
    }
    #endregion

    public float interval;
    public float TotalTime;

    [System.NonSerialized]
    protected float currentOverTime;

    [System.NonSerialized]
    protected int amount;
    protected virtual void DoAction(AbilityController controller)
    {
        controller.Init();
    }
    public virtual void Enter(Skill _skill)
    {
        Reset();

        InitInterups(_skill);
    }


    public virtual void Reset()
    {
        this.amount = 0;
        this.currentOverTime = 0;
        this._isCompleted = false;
    }

    public float Progress()
    {
        if (this.TotalTime == 0)
            return 0;

        return Mathf.Clamp01(this.currentOverTime / this.TotalTime);

    }
    protected bool _isCompleted;
    public bool IsCompleted
    {
        get { return _isCompleted; }
        set
        {
            _isCompleted = value;
        }
    }

    public System.Action DoInterrupt;
    public virtual void Force(AbilityController controller)
    {
        DoAction(controller);
        IsCompleted = true;

        Debug.LogError("FOrce");
        DoInterrupt?.Invoke();
        // Không sử dụng hàm complete vì nó đã xong đâu. Chỉ là quit thôi.
    }
    public virtual void DoUpdate(AbilityController controller, float deltaTime)
    {
        if (_isCompleted)
            return;

        if (this.interval > this.TotalTime
              || (this.interval == 0 && this.TotalTime == 0))
        {
            DoAction(controller);
            Completed();

            return;
        }

        if (Mathf.FloorToInt(this.currentOverTime / interval) >= (amount + 1)
            && (this.currentOverTime - this.TotalTime) <= deltaTime)
        {
            // chưa biết có nên dùng không nữa.

            DoAction(controller);
            amount++;
        }

        this.currentOverTime += deltaTime;

        if ((this.currentOverTime - this.TotalTime) > deltaTime)
        {
            Completed();
        }
    }

    public void Completed()
    {
        IsCompleted = true;

        Exit();
    }


    public System.Action DoExit;
    public virtual void Exit()
    {
        DoExit?.Invoke();
    }
}
//[CreateAssetMenu(menuName = "KSkill/Behaviour/Behaviour")]
[System.Serializable]
public class PerformSkill : CBehaviour
{
    public CCharacter onCharacter;

    public override void DoUpdate(AbilityController controller, float deltaTime)
    {
        if (_isCompleted)
            return;

        if (this.interval > this.TotalTime
              || (this.interval == 0 && this.TotalTime == 0))
        {
            DoAction(controller);
            Completed();

            return;
        }

        if (Mathf.FloorToInt(this.currentOverTime / interval) >= (amount + 1)
            && (this.currentOverTime - this.TotalTime) <= deltaTime)
        {
            // chưa biết có nên dùng không nữa.

            DoAction(controller);
            amount++;
        }

        this.currentOverTime += deltaTime;

        if ((this.currentOverTime - this.TotalTime) > deltaTime)
        {
            Completed();
        }
    }

    public override void Force(AbilityController controller)
    {
        base.Force(controller);
        Completed();
    }
    public override void Enter(Skill _skill)
    {
        base.Enter(_skill);

        _skill.gameObject.GetComponent<Renderer>().sharedMaterial.color = Color.black;
    }

}
