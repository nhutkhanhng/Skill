using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KSkill;

public class KObjectSerilized<T> where T : struct
{
    public T _Value;
}


[System.Serializable]
public class CCValue : KObjectSerilized<float>, IComponent
{
    public CCValue()
    {
        this._Value = 0;
    }
    public CCValue(float _v)
    {
        this._Value = _v;
    }

    public float Accept(IVisitor visitor)
    {
        return visitor.Accept(this);
    }

    public static implicit operator CCValue(float flat) => new CCValue(flat);
    public static implicit operator float(CCValue _flat) => _flat._Value;
}

[System.Serializable]
public class FlatValue : KObjectSerilized<float>, IComponent
{
    public FlatValue()
    {
        this._Value = 0;
    }

    public FlatValue(float _v)
    {
        this._Value = _v;
    }

    public float Accept(IVisitor visitor)
    {
        return visitor.Accept(this);
    }

    public static implicit operator FlatValue(float flat) => new FlatValue(flat);
    public static implicit operator float(FlatValue _flat) => _flat._Value;
}


public interface IComponent
{
    float Accept(IVisitor visitor);
}

public interface IVisitor
{
    float Accept(FlatValue ccNormalized);
    float Accept(CCValue dameNormalized);
}


[System.Serializable]
public class ExtraNormalize : IVisitor
{
    public float factor;
    public float flat;

    //[System.NonSerialized]
    public float normalizeTime;

    public virtual float Accept(FlatValue flat)
    {
        return flat._Value;
    }

    public virtual float Accept(CCValue cc)
    {
        return cc._Value;
    }

    public float Result(float _Value)
    {
        return _Value * factor * normalizeTime + flat;
    }
}


[System.Serializable]
public class CCNormalize : ExtraNormalize
{
    public override float Accept(CCValue cc)
    {
        return Result(cc._Value);
    }
}
[System.Serializable]
public class DamageNormalize : ExtraNormalize
{
    public override float Accept(FlatValue flat)
    {
        return Result(flat._Value);
    }
}
[System.Serializable]
public class DefenseNormalize : ExtraNormalize
{

}


[CreateAssetMenu(menuName = "KSkill/Behaviour/Casting")]
public class CastSkill : MonoBehaviour
{
    public CCValue cc;
    public DamageNormalize normalize;

    public KSkill.Action test;
    public ICharacter c = null;
    public List<ICharacter> t;

    [ContextMenu("TEST")]
    public void TestExe()
    {
        test.Act(c,t,normalize);
    }
}
