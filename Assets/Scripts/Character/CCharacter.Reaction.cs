using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class CCharacter
{
    public event Action<CCharacter> OnKilled;
    public event Action<CCharacter> OnKill;

    public event Action OnHPChange;
    public event Action OnFuryChange;

    public event Action<float> OnPercentHPChange;
    public event Action<ushort> OnPhysicalShieldChange;
    public event Action<ushort> OnFireShieldChange;
    public event Action<ushort> OnIceShieldChange;
    public event Action<ushort> OnLightningShieldChange;
    public event Action<ushort> OnPoisonShieldChange;
    public event Action<ushort> OnDarknessShieldChange;
    public event Action<ushort> OnArcaneShieldChange;
    public event Action<ushort> OnLightShieldChange;
    public event Action<ushort> OnFuryFull;
    public event Action<ushort> OnFuryNotFull;
    public event Action<ushort> OnFuryChargeNewPiece;

    //public event Action<CCharacter, EHitType> OnPreAttack;
    //public event Action<CCharacter, EHitType> OnPosAttack;
    //public event Action<CCharacter, EHitType, Dictionary<EElemental, int>> OnAttacked;
    public event Action<int> OnDamaged;

    //public event Action<CCharacter, int, EHitType> OnReceiveDamage;

    public event Action OnReborn;

    public event Action<CCharacter> OnSwitchTarget;

    public event Action OnIntoPiece;
    public event Action OnEndPiece;

    public event Action OnPressCombination;
    public event Action<float> OnUpdateCommandSkillValue;
    public event Action OnUpdateCommandSkillUI;
    public event Action OnLockActiveSkill;
    public event Action OnUnlockActiveSkill;

    public event Action OnCC;
}
