using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;

[CreateAssetMenu(menuName = "KSkill/Behaviour/Behaviour")]
public class BehaviourInState : ScriptableObject
{
    public CCharacter onCharacter;

    /// <summary>
    /// Cái này đang bị confuse giữa việc thiết kế.
    /// Nên dùng một Ability có khả năng Overtime.
    /// Hay sử dụng Behaviour gọi Ability theo interval ????
    /// I don't know, 
    /// </summary>
    public Ability abilityUsed;
    public float interval;
    public float TotalTime;

    [System.NonSerialized]
    protected float currentOverTime;

    [System.NonSerialized]
    protected int amount;
    protected void DoAction(AbilityController controller)
    {
        Debug.LogError("DoAction");
        controller.Init();
    }
    public void Enter()
    {
        Reset();
        this.onCharacter.GetComponent<Renderer>().sharedMaterial.color = Color.red;
    }

    public void Reset()
    {
        this.amount = 0;
        this.currentOverTime = 0;
        this._isCompleted = false;
    }

    protected bool _isCompleted;
    public bool IsCompleted { get { return _isCompleted; }
        set {
            _isCompleted = value;
        }
    }

    public void DoUpdate(AbilityController controller, float deltaTime)
    {
        if (_isCompleted)
            return;

        if (this.interval > this.TotalTime
              || (this.interval == 0 && this.TotalTime == 0))
        {
            Debug.LogError(this.interval + "|||" + this.TotalTime);

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
    public void Exit()
    {
        this.onCharacter.GetComponent<Renderer>().sharedMaterial.color = Color.white;
    }
    
}
