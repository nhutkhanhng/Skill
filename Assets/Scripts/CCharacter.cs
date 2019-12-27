using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;
public class CCharacter : MonoBehaviour, KSkill.ICharacter, ISkillFunction
{
    public float MaxHP = 100;
    [SerializeField]
    protected float _currentHP;
    public float CurrentHp
    {
        get { return _currentHP; }
        set
        {
            _currentHP = value;
        }
    }

    public Vector3 Position {
        get
        {
            return this.transform.position;
        }
    }

    public float CurrentHpPercent
    {
        get
        {
            return this.CurrentHp / MaxHP * 100;
        }
    }

    public ISkillFunction SkillFunction
    {
        get
        {
            return this;
        }
    }

    public AbilityController _Ability;

    // Start is called before the first frame update
    void Start()
    {
        _Ability.controller = this;
        _Ability.Init();
    }

    // Update is called once per frame
    void Update()
    {
        _Ability.DoUpdate();
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.CurrentHp = 5;
        }
    }

    public void ReceiveBuffHP(float HP)
    {
        this.CurrentHp += HP;
    }
}
