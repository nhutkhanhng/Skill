using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[System.Serializable]
public class VFXSettings
{
    [SerializeField]
    protected GameObject Vfx;
    [SerializeField]
    protected Vector3 Scale = Vector3.one;
    [SerializeField]
    protected Quaternion Rotate = Quaternion.identity;
    [SerializeField]
    protected float Duration;

    [SerializeField]
    protected Vector3 Offset = Vector3.zero;

    [HideInInspector] public List<GameObject> VFXs = new List<GameObject>();

    public GameObject Spawn()
    {
        if (this.Vfx == null)
        {
#if UNITY_EDITOR
            //            Debug.LogError("Mất VFX");
#endif
            return null;
        }
        GameObject newVFX = GameObject.Instantiate(Vfx, Scale, Rotate);
        newVFX.transform.localScale = this.Scale;

        this.VFXs.Add(newVFX);

        return newVFX;
    }

    public GameObject Spawn(Transform parent)
    {
        if (parent == null)
            return null;

        var newVFX = Spawn();

        if (newVFX == null)
            return null;

        newVFX.transform.parent = parent;
        newVFX.transform.ResetLocal();

        newVFX.transform.localPosition += Offset;
        newVFX.transform.rotation = this.Rotate;

        return newVFX;
    }

    public GameObject Spawn(Vector3 Position)
    {

        var newVFX = Spawn();

        if (newVFX == null)
            return null;

        newVFX.transform.position = Position;
        newVFX.transform.position += Offset;

        return newVFX;
    }

    public void RemoveAll()
    {
        if (this.VFXs.Available())
        {
            foreach (var fx in this.VFXs)
            {
                GameObject.Destroy(fx.gameObject);
            }

            this.VFXs.Clear();
        }
    }
}