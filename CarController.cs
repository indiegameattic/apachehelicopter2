using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : Target, IDriveable
{
    [SerializeField]
    private float _speed = 50f;
    public List<Axle> axles;
    public float maxMotorTorque;
    public float maxSteeringAngle;

    private Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();

    }
    void Start()
    {
        
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        Drive();
        
        //transform.position += new Vector3(0f, 0f, 0.15f);
        //_rb.MovePosition(transform.position + new Vector3(0f, 0f, 0.15f));
    }

    public void Drive()
    {
        float motor = _speed;

        foreach (Axle axle in axles)
        {
            if (axle.motor)
            {
                axle.leftWheel.motorTorque = motor;
                axle.rightWheel.motorTorque = motor;
            }
        }
    }
}

[Serializable]
public class Axle
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}