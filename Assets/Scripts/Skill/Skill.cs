using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A
{
    public int t = 1;
}


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

public class Skill : MonoBehaviour
{
    public A a = new A(), b = new A();

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError(a.GetHashCode().ToString() + "|" + b.GetHashCode().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
