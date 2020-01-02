using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Flags]
public enum EBuff
{

}

[System.Flags]
public enum EDeBuff
{
    // Kiểu như hôn gió của Ahri.
    Charm = 1,
    Disarm = 1 << 1,
    Silent = 1<< 2,
    Slow = 1 << 3,
    Freeze = 1 << 4,
}

public interface ICastingSkill
{
    float StartCasting();
    void Casting();
    void EndCasting();
}

public interface IPerformSkill
{
    float StartPerform();
    void Performing();
    void EndPerform();
}

public class Skill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
