using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineTurbineController : MonoBehaviour
{
    //[SerializeField]
    private float _throttleInput;
    public float ThrottleInput
    {
        get { return _throttleInput; }
        set { _throttleInput = value; }
    }

    [SerializeField]
    private float _currentHP;
    public float CurrentHP
    {
        get { return _currentHP; }
        set { _currentHP = value; }
    }

    [SerializeField]
    private float _hpMultiplier = 18f;

    private float _hp;

    void Awake()
    {
        _hp = 0f;
        _currentHP = 0f;
    }

    void Start()
    {

    }

    void Update()
    {
        HandleEngineTurbines(_throttleInput);
    }

    private void HandleEngineTurbines(float throttleInput)
    {
        _hp += throttleInput;
        _hp = Mathf.Clamp(_hp, 0f, 100f);
        _currentHP = _hpMultiplier * _hp;
    }
}
