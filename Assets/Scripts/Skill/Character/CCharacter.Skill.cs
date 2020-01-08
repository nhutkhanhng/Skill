using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterUseSkill
{
    bool CanUseSkill();

    void StartSKill();

    void EndSkill();
    ICastingSkill Casting();
    IPerformSkill Performing();
}
public partial class CCharacter : ICharacterUseSkill
{
    public bool CanUseSkill()
    {
        return true;
    }

    public ICastingSkill Casting()
    {
        throw new System.NotImplementedException();
    }

    public void EndSkill()
    {
        throw new System.NotImplementedException();
    }

    public void Perform()
    {
        throw new System.NotImplementedException();
    }

    public IPerformSkill Performing()
    {
        throw new System.NotImplementedException();
    }

    public void StartSKill()
    {
        throw new System.NotImplementedException();
    }

    public void StartUsingSkill()
    {
        this.Casting().StartCasting();
    }
}
