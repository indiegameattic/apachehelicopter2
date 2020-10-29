using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HelicopterBase : MonoBehaviour
{
    

    public float LiftForce = 15f;
    public float TailForce = 45f;
    public float CyclicForce = 6f;
    public float BrakeForce = 0.5f;
    //public float AscentSpeed = 4f;
    //private float EngineRPMs = 2700f;
    //private float FinalPower = 0f;

    [SerializeField]
    [Tooltip("Maximum Amount of Fuel in lbs.")]
    private float _maxFuel = 6256f;
    public float MaxFuel
    {
        get { return _maxFuel; }
        set { _maxFuel = value; }
    }

    [SerializeField]
    [Tooltip("Current Amount of Fuel in lbs.")]
    private float _currentFuel;
    public float CurrentFuel
    {
        get { return _currentFuel; }
        set { _currentFuel = value; }
    }

    protected Rigidbody _rb;
    public Rigidbody Rb
    {
        get { return _rb; }
        set { _rb = value; }
    }

    //[SerializeField]
    private float _distanceToGround;
    public float DistanceToGround
    {
        get { return _distanceToGround; }
        set { _distanceToGround = value; }
    }

    //[SerializeField]
    private Vector3 _localVelocity;
    public Vector3 LocalVelocity
    {
        get { return _localVelocity; }
        set { _localVelocity = value; }
    }

    //[SerializeField]
    private Vector3 _localEulerAngles;
    public Vector3 LocalEulerAngles
    {
        get { return _localEulerAngles; }
        set { _localEulerAngles = value; }
    }

    private Quaternion _localRotation;
    public Quaternion LocalRotation
    {
        get { return _localRotation; }
        set { _localRotation = value; }
    }

    private Transform _localTranform;
    public Transform LocalTransform
    {
        get { return _localTranform; }
        set { _localTranform = value; }
    }

    private EngineController _engineController;

    public float _lift = 0f;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _localTranform = transform;
        _engineController = GetComponentInChildren<EngineController>();
        _currentFuel = _maxFuel;
    }

    protected virtual void Start()
    {
        _localTranform = GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        GetPhysicalCharacteristics();
    }

    protected virtual void FixedUpdate()
    {

    }

    private void GetPhysicalCharacteristics()
    {
        _distanceToGround = Utils.GetDistanceToGround(transform);

        _localVelocity = transform.InverseTransformDirection(Rb.velocity);

        _localEulerAngles = transform.rotation.eulerAngles;

        _localRotation = transform.rotation;

        _localTranform = transform;

        //_lift = 8.333f * _engineController.CurrentHP;
    }

    protected virtual void HandleEngines(InputController input)
    {

    }

    protected virtual void HandleMainRotors(MainRotorController mainRotor, InputController input)
    {
        //mainRotor.RotorSpeed = FinalPower;
    }

    protected virtual void HandleTailRotors(TailRotorController tailRotor, InputController input)
    {
        //tailRotor.RotorSpeed = FinalPower;
    }

    protected virtual void HandleTailFins(TailFinController tailFin, InputController input)
    {

    }

    protected virtual void HandleThrottle(InputController input)
    {
        if (_currentFuel <= 0f)
        {
            input.ThrottleInput = 0f;
        }
        else
        {
            _engineController.Throttle += input.ThrottleInput;
        }
        _engineController.Throttle = Mathf.Clamp(_engineController.Throttle, 0f, 100f);

    }

    protected virtual void HandleCyclicX(Rigidbody rb, InputController input)
    {
        //rb.AddForce(transform.forward * input.Vertical * AscentSpeed);
        //transform.Rotate(new Vector3(input.Vertical * 0.5f, 0f, 0f));
        if (_currentFuel <= 0f)
        {
            input.CyclicX *= UnityEngine.Random.Range(0.02f, 0.08f);
        }
        if (_engineController.Throttle > 85f)
        {
            _engineController.FuelRate = UnityEngine.Random.Range(0.02f, 0.08f);
            _localTranform.Rotate(new Vector3(0f, 0f, 1.75f * -input.CyclicX));
            rb.AddForce(transform.right * input.CyclicX * CyclicForce);
        }
    }

    protected virtual void HandleCyclicZ(Rigidbody rb, InputController input)
    {
        if (_currentFuel <= 0f)
        {
            input.CyclicZ *= UnityEngine.Random.Range(0.2f, 0.8f);
            input.CollectiveInput *= UnityEngine.Random.Range(0.2f, 0.8f);
        }
        if (_engineController.Throttle > 85f)
        {
            _engineController.FuelRate = UnityEngine.Random.Range(0.02f, 0.06f);
            rb.AddForce(transform.forward * input.CyclicZ * CyclicForce);
            _localTranform.Rotate(new Vector3(input.CyclicZ * 0.65f, 0f, 0f));
        }
    }

    protected virtual void HandlePedals(Rigidbody rb, InputController input)
    {
        if (_currentFuel <= 0f)
        {
            input.PedalInput *= UnityEngine.Random.Range(0.2f, 0.8f);
        }
        if (_engineController.Throttle > 85f)
        {
            _engineController.FuelRate = UnityEngine.Random.Range(0.02f, 0.08f);
            _localTranform.rotation = Quaternion.Euler(_localTranform.rotation.eulerAngles + new Vector3(0f, input.PedalInput * TailForce * Time.deltaTime, 0f));
        }
        //transform.RotateAround(transform.position, Vector3.zero, input.PedalInput * TailForce * Time.deltaTime);


        //transform.rotation = Quaternion.LookRotation(transform.right * input.PedalInput * TailForce);
        //Quaternion deltaRotation = Quaternion.Euler(transform.right * input.PedalInput * TailForce * Time.deltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);x

        //rb.AddTorque(Vector3.up * input.PedalInput * TailForce, ForceMode.Acceleration);

        //transform.Rotate(0, input.PedalInput * TailForce * Time.deltaTime, 0);
    }

    protected virtual void HandleBrake(Rigidbody rb, InputController input)
    {
        if (input.BrakeInput > 0.01f)
        {
            _engineController.FuelRate = 0f;
            rb.velocity = Vector3.Slerp(rb.velocity, Vector3.zero, input.BrakeInput * BrakeForce * Time.deltaTime);
            rb.rotation = Quaternion.Slerp(_localTranform.rotation, Quaternion.Euler(0f, 0f, 0f), input.BrakeInput * BrakeForce * Time.deltaTime);
            rb.freezeRotation = true;
        }
    }

    protected virtual void HandleCollective(Rigidbody rb, InputController input)
    {
        //Vector3 liftForce = transform.up * (UnityEngine.Physics.gravity.magnitude + LiftForce) * rb.mass;

        //liftForce *= Mathf.Pow(input.CollectiveInput, 2) * Mathf.Pow(EngineRPMs, 2f);

        //rb.AddForce(transform.up * (Throttle * .01f) * _lift, ForceMode.Force);
        if (_currentFuel <= 0f)
        {
            input.CollectiveInput *= 0.1f;
        }

        if (_engineController.Throttle > 85f)
        {
            _engineController.FuelRate = UnityEngine.Random.Range(0.01f, 0.08f);

            Vector3 liftForce = transform.up * input.CollectiveInput * LiftForce;

            rb.AddForce(liftForce, ForceMode.Force);
        }
        //rb.AddTorque(Vector3.up * input.Horizontal * TailForce, ForceMode.Acceleration);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

        Debug.Log("OnTriggerEnter:" + other.name + "," + LayerMask.LayerToName(other.gameObject.layer));

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            float verticalSpeed = Mathf.Abs(_localVelocity.y);
            if (verticalSpeed > 10f)
            {
                Debug.Log("!!!CRASH!!! " + verticalSpeed + ":" + other.transform.position);
                _localTranform.position = other.transform.position;
                _rb.velocity = Vector3.zero;
                _rb.freezeRotation = true;
                Utils.Explode(_localTranform, 20f, 200f, 50f, "Default");
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter:" + collision.collider.name + "," + LayerMask.LayerToName(collision.collider.gameObject.layer));
    }
}
