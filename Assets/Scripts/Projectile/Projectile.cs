using KSkill;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Projectile
{
    public System.Action<List<ICharacter>> OnHit;
    public System.Action OnDestroy;

    [SerializeField]
    public LinearTrajectory path;

    [HideInInspector] public float timeStamp;


    /*[HideInInspector]*/ public GameObject beAdd;
    [SerializeField] private GameObject _beAdd;

    public void Init()
    {

    }
    public void DoLaunch()
    {
        path.GetDirection();

        this.beAdd = GameObject.Instantiate(_beAdd);
        this.beAdd.transform.position = path.m_Begin;
    }

    private void Awake()
    {
        //path = GetComponent<KTrajectory>();
    }
    //private void Start()
    //{
    //    path = new LinearTrajectory();

    //    DoLaunch();
    //}
    public void OnUpdate() { Update(); }
    public void Update()
    {
        timeStamp = Time.deltaTime;
        this.beAdd.transform.position = path.Calculate(this.timeStamp);
    }
}
