﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;
using System;


public enum ECharacterRole : int
{
    DamageDealer = 0,
    Nuker = 1,
    Supporter = 2,
    Tanker = 3
}

public partial class CCharacter : MonoBehaviour, ICharacter, ISkillFunction
{
    public bool isPlayer;

    [SerializeField]
    protected float _currentHP;
    public float CurrentHp
    {
        get { return _currentHP; }
        set
        {
            _currentHP = value;
            OnHpChanged();
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
            return this.CurrentHp / MaxHP   ;
        }
    }

    public ISkillFunction SkillFunction
    {
        get
        {
            return this;
        }
    }

    [SerializeField]
    private byte _IdTeam;
    
    public byte IdTeam
    {
        get { return _IdTeam; }
        set { _IdTeam = value; }
    }

    public AbilityController _Ability;

    private void Awake()
    {
        CCharacterManager.Instance.AllCharacter.Add(this);
    }

    public bool IsAlive()
    {
        return true;
    }
    // Start is called before the first frame upda  te
    void Start()
    {
        if (_Ability)
        {
            _Ability.controller = this;
            _Ability.Init();

            _Ability.Test();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_Ability)
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



    public void OnHpChanged()
    {
        OnHPChange?.Invoke();
    }
}
