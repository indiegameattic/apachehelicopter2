using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRotorController : RotorBase
{
    private Transform _rotorPivot;
    public Transform RotorPivot
    {
        get { return _rotorPivot; }
        set { _rotorPivot = value; }
    }

    [SerializeField]
    private Transform _rotorMast;
    public Transform RotorMast
    {
        get { return _rotorMast; }
        set { _rotorMast = value; }
    }

    private void Start()
    {
        _rotorPivot = GetComponent<Transform>();
    }

    void Update()
    {
        base.HandleRotor(_rotorMast, base.RotorSpeed);
    }

    protected virtual void HandleRotor()
    {
        
    }
}
