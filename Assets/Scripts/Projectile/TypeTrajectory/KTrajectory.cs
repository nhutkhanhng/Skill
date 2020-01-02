using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ITrajectoryPath
{
    public TrajectoryData Data;

    public Vector3 m_Begin;

    public Vector3 m_End;

    [System.NonSerialized]
    protected float m_Progress;
    [System.NonSerialized]
    protected float m_Time;

    public abstract Vector3 Calculate(float dt);

    public abstract bool IsComplete();
    public virtual void ResetData()
    {
        m_Time = 0;
        m_Progress = 0f;
    }

    public float GetProgress()
    {
        return m_Progress;
    }
}

[System.Serializable]
public class TrajectoryData
{
    public float m_Duration;
    public float m_Height;
}


[System.Serializable]
public class KTrajectory : ITrajectoryPath
{
    #region ===== Fields =====
    protected Vector3 m_Change;
    public float m_Duration { get { return Data.m_Duration; } set { Data.m_Duration = value; } }

    #endregion

    public virtual void Init(Vector3 Start, Vector3 End)
    {
        this.m_Begin = Start;
        this.m_End = End;

        //this.ResetData();
        m_Change = GetDirection();
    }

    public virtual Vector3 GetDirection()
    {
        return m_End - m_Begin;
    }

    public override Vector3 Calculate(float dt)
    {
        m_Time += dt;
        m_Progress = m_Time / m_Duration;
        m_Progress = Mathf.Clamp(m_Progress, 0, 1);

        return m_Begin;
    }

    public override bool IsComplete()
    {
        return m_Progress >= 1;
    }
}
