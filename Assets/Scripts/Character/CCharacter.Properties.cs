﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacter
{
    protected int _Fury;
    public void OnFuryChanged() { }
    public int Fury
    {
        get { return _Fury; }
        set { _Fury = value;  OnFuryChanged(); }
    }

    protected int maxHP;
    /// <summary>
    /// Khai báo callback ở trong hàm này.
    /// </summary>
    public void OnMaxHpChanged() { }

    public int MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; OnMaxHpChanged(); }
    }
}