using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearTrajectory : KTrajectory
{
    public override Vector3 Calculate(float dt)
    {
        m_Time += dt;
        m_Progress = m_Time / m_Duration;
        m_Progress = Mathf.Clamp(m_Progress, 0, 1);

        return m_Change * m_Progress + m_Begin;
    }
}
