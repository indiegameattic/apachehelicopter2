using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApacheController : HelicopterBase
{
    private InputController _inputController;
    private MainRotorController _mainRotorController;
    private TailRotorController _tailRotorController;
    private TailFinController _tailFinController;

    protected override void Awake()
    {
        base.Awake();
        _inputController = GetComponent<InputController>();

        _mainRotorController = GetComponentInChildren<MainRotorController>();
        _tailRotorController = GetComponentInChildren<TailRotorController>();
        _tailFinController = GetComponentInChildren<TailFinController>();
    }

    protected override void Start()
    {

    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        base.HandleEngines(_inputController);
        base.HandleMainRotors(_mainRotorController, _inputController);
        base.HandleTailRotors(_tailRotorController, _inputController);
        base.HandleTailFins(_tailFinController, _inputController);

        base.HandleThrottle(_inputController);
        base.HandleCyclicX(Rb, _inputController);
        base.HandleCyclicZ(Rb, _inputController);
        base.HandleCollective(Rb, _inputController);
        base.HandlePedals(Rb, _inputController);
        base.HandleBrake(Rb, _inputController);

        HandleCyclicX(Rb, _inputController);
        HandleCyclicZ(Rb, _inputController);
        HandleCollective(Rb, _inputController);
        HandlePedals(Rb, _inputController);

        AutoLevel();

    }

    protected override void Update()
    {
        base.Update();
    }

    private void AutoLevel()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f), 2f * Time.deltaTime);
    }

    protected override void HandleCollective(Rigidbody rb, InputController input)
    {
        var rotors = _mainRotorController.Rotors;

        rotors[0].localRotation = Quaternion.Euler(15f * input.CollectiveInput, rotors[0].transform.localEulerAngles.y, 0f);
        rotors[1].localRotation = Quaternion.Euler(-15f * input.CollectiveInput, rotors[1].transform.localEulerAngles.y, 0f);
        rotors[2].localRotation = Quaternion.Euler(15f * input.CollectiveInput, rotors[2].transform.localEulerAngles.y, 0f);
        rotors[3].localRotation = Quaternion.Euler(-15f * input.CollectiveInput, rotors[3].transform.localEulerAngles.y, 0f);
    }

    protected override void HandleCyclicX(Rigidbody rb, InputController input)
    {
        if (input.CyclicX != 0f)
        {
            _mainRotorController.RotorPivot.localRotation = Quaternion.Euler(
                _mainRotorController.RotorPivot.transform.localEulerAngles.x,
                _mainRotorController.RotorPivot.transform.localEulerAngles.y,
                6f * -input.CyclicX);
        }

    }

    protected override void HandleCyclicZ(Rigidbody rb, InputController input)
    {

        foreach (var tailFin in _tailFinController.TailFins)
        {
            tailFin.localRotation = Quaternion.Euler(35f * (input.CyclicZ + input.CollectiveInput), tailFin.transform.localEulerAngles.y, 0f);
        }
        _mainRotorController.RotorPivot.localRotation = Quaternion.Euler(
            4f * input.CyclicZ,
            _mainRotorController.RotorPivot.transform.localEulerAngles.y,
            _mainRotorController.RotorPivot.transform.localEulerAngles.z);
    }

    protected override void HandlePedals(Rigidbody rb, InputController input)
    {

        foreach (var rotor in _tailRotorController.Rotors)
        {
            rotor.localRotation = Quaternion.Euler(15f * input.PedalInput, rotor.transform.localEulerAngles.y, 0f);
        }
        if (input.PedalInput != 0f)
        {
            _mainRotorController.RotorPivot.localRotation = Quaternion.Euler(
            _mainRotorController.RotorPivot.transform.localEulerAngles.x,
           _mainRotorController.RotorPivot.transform.localEulerAngles.y,
            4f * -input.PedalInput);
        }

    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }
}
