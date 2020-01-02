using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ITrajectoryPath
{
    protected Vector3 m_Begin;
    protected Vector3 m_End;

    protected float m_Progress;
    protected float m_Time;

    public abstract Vector3 Calculate(float dt);

    public abstract bool IsComplete();
    public virtual void Start()
    {
        m_Time = 0;
    }

    public float GetProgress()
    {
        return m_Progress;
    }
}

/// <summary>
/// 
/// </summary>
public class KTrajectory : ITrajectoryPath
{
    #region ===== Fields =====


    protected Vector3 m_Vertex;

    protected Vector3 m_Change; 

    protected float m_Duration;
    protected float m_Height;


    #endregion


    public virtual void Init(Vector3 Start, Vector3 End, Vector3 Vertex, Vector3 Change)
    {
        this.m_Begin = Start;
        this.m_End = End;

        this.m_Vertex = Vertex;
        this.m_Change = Change;
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
