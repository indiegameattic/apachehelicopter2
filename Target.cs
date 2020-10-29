using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetable
{

}

public interface IDriveable
{
    void Drive();
}

public class Target : MonoBehaviour, ITargetable
{
    [SerializeField]
    private float _hitpoints;
    public float HitPoints
    {
        get { return _hitpoints; }
        set { _hitpoints = value; }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {

    }
}
