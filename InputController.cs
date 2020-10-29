using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private float _throttleInput = 0f;
    //private float nextFire = 0f;

    public float ThrottleInput
    {
        get { return _throttleInput; }
        set { _throttleInput = value; }
    }

    private float _collectiveInput = 0f;
    public float CollectiveInput
    {
        get { return _collectiveInput; }
        set { _collectiveInput = value; }
    }

    private float _pedalInput = 0f;
    public float PedalInput
    {
        get { return _pedalInput; }
        set { _pedalInput = value; }
    }

    private float _brakeInput = 0f;
    public float BrakeInput
    {
        get { return _brakeInput; }
        set { _brakeInput = value; }
    }

    private float _cyclicX = 0f;
    public float CyclicX
    {
        get { return _cyclicX; }
        set { _cyclicX = value; }
    }

    private float _cyclicZ = 0f;
    public float CyclicZ
    {
        get { return _cyclicZ; }
        set { _cyclicZ = value; }
    }

    // Scroll through Targets
    private bool _targets = false;
    public bool Targets
    {
        get { return _targets; }
        set { _targets = value; }
    }

    // TADS Camera Zoom (Mousewheel--OnMouseOver)
    private float _tadsCameraZoom = 0f;
    public float TadsCameraZoom
    {
        get { return _tadsCameraZoom; }
        set { _tadsCameraZoom = value; }
    }

    // Load Hellfire Missiles
    private bool _loadHellfireMissilesInput = false;
    public bool LoadHellfireMissilesInput
    {
        get { return _loadHellfireMissilesInput; }
        set { _loadHellfireMissilesInput = value; }
    }

    // Launch Missile
    private float _launchMissleInput = 0f;
    public float LaunchMissileInput
    {
        get { return _launchMissleInput; }
        set { _launchMissleInput = value; }
    }

    // Toggle Munitions
    private bool _toggleMunitionsInput = false;
    public bool ToggleMunitionsInput
    {
        get { return _toggleMunitionsInput; }
        set { _toggleMunitionsInput = value; }
    }

    protected virtual void Update()
    {
        HandleThrottle();
        HandleCyclicX();
        HandleCyclicZ();
        HandleCollective();
        HandlePedal();
        HandleBrake();
        ClampInputs();

        ScrollTargets();
        HandleTadsCameraZoom();

        HandleLoadHellfireMissiles();
        HandleLaunchMissile();

        HandleToggleMunitions();
    }

    protected virtual void FixedUpdate()
    {
        
        
    }

    protected virtual void HandleThrottle()
    {
        _throttleInput = Input.GetAxis("Vertical");
    }

    protected virtual void HandleCyclicX()
    {
        _cyclicX = Input.GetAxis("CyclicX");
    }

    protected virtual void HandleCyclicZ()
    {
        _cyclicZ = Input.GetAxis("CyclicZ");
    }

    protected virtual void HandleCollective()
    {
        _collectiveInput = Input.GetAxis("Collective");
    }

    protected virtual void HandlePedal() {
        _pedalInput = Input.GetAxis("Pedal");
    }

    protected virtual void HandleBrake()
    {
        _brakeInput = Input.GetAxis("Brake");
    }

    private void ClampInputs()
    {
        CollectiveInput = Mathf.Clamp(CollectiveInput, -1f, 1f);
    }

    protected virtual void ScrollTargets()
    {
        _targets = Input.GetKeyDown(KeyCode.Tab);
    }

    protected virtual void HandleTadsCameraZoom()
    {
        _tadsCameraZoom = Input.GetAxis("Mouse ScrollWheel");
    }

    protected virtual void HandleLoadHellfireMissiles()
    {
        _loadHellfireMissilesInput = Input.GetKeyDown(KeyCode.H);
    }

    protected virtual void HandleLaunchMissile()
    {
        //if (Time.time > nextFire)
        //{
            //nextFire = Time.time + fireRate;
            _launchMissleInput = Input.GetAxis("LaunchMissile");
        //}
       
    }

    protected virtual void HandleToggleMunitions()
    {
        _toggleMunitionsInput = Input.GetKeyDown(KeyCode.K);
    }
}
