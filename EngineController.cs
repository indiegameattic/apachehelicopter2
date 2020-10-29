using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EngineController : MonoBehaviour
{
    private ApacheController _apacheController;
    private InputController _inputController;

    [SerializeField]
    private List<EngineTurbineController> _engineTurbineControllers;

    [SerializeField]
    private MainRotorController _mainRotorController;

    [SerializeField]
    private TailRotorController _tailRotorController;

    [SerializeField]
    private HUDUI _hudUI = null;

    //public AnimationCurve PowerCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

    [SerializeField]
    private float _throttle = 0f;
    public float Throttle
    {
        get { return _throttle; }
        set { _throttle = value; }
    }

    [SerializeField]
    private float _currentHp;
    public float CurrentHP
    {
        get { return _currentHp; }
        set { _currentHp = value; }
    }

    [SerializeField]
    private float _fuelRate = 0f;
    public float FuelRate
    {
        get { return _fuelRate; }
        set { _fuelRate = value; }
    }

    [SerializeField]
    private float _mainRotorRPM;
    public float MainRototRPM
    {
        get { return _mainRotorRPM; }
    }

    [SerializeField]
    private float _tailRotorRPM;
    public float TailRototRPM
    {
        get { return _tailRotorRPM; }
    }

    private float _finalFuelRate;

    void Awake()
    {
        _apacheController = GetComponentInParent<ApacheController>();
        _inputController = GetComponentInParent<InputController>();
        _engineTurbineControllers = GetComponentsInChildren<EngineTurbineController>().ToList();
    }

    void Start()
    {
        if (!_apacheController)
        {
            Debug.Log("No _apacheController");
        }
    }

    void Update()
    {
        UpdateEngine(_inputController.ThrottleInput);

        UpdateMainRotor();

        UpdateTailRotor();

        UpdateHUDUI();
    }
    private void UpdateEngine(float throttleInput)
    {
        if (_throttle > 0)
        {
            if (_apacheController.CurrentFuel > 0)
            {
                //_finalFuelRate = (_throttle * UnityEngine.Random.Range(0.005f, 0.015f)) + (_fuelRate / UnityEngine.Random.Range(1.15f, 2.75f));
                _finalFuelRate = (_throttle * UnityEngine.Random.Range(0.005f, 0.015f)) + _fuelRate;
                _apacheController.CurrentFuel -= Time.deltaTime * _finalFuelRate;

                if (_hudUI)
                {
                    if (_apacheController.CurrentFuel / _apacheController.MaxFuel < 0.10f)
                    {
                        UpdateMechanicalMessage("[LOW FUEL: " + Mathf.Round((_apacheController.CurrentFuel / _apacheController.MaxFuel) * 100f).ToString() + "%]");
                    }
                }
            }
            else
            {
                if (_hudUI)
                {
                    UpdateMechanicalMessage("[NO FUEL]");
                }
                _apacheController.Rb.useGravity = true;
            }
        }

        foreach (var turbine in _engineTurbineControllers)
        {
            if (_apacheController.CurrentFuel <= 0 && _currentHp > 0f)
            {
                throttleInput = UnityEngine.Random.Range(-0.25f, 0.02f);
            }
            turbine.ThrottleInput = throttleInput;
            float mainRotorSpeedDivider = _mainRotorController.MaxRotorSpeed / UnityEngine.Random.Range(1.85f, 2.15f);
            float tailRotorSpeedDivider = _tailRotorController.MaxRotorSpeed / UnityEngine.Random.Range(1.85f, 2.15f);

            _currentHp = Mathf.Clamp(turbine.CurrentHP + UnityEngine.Random.Range(-5f, 5f), 0f, 2000f);
            if (_throttle > 0)
            {
                _mainRotorRPM = _currentHp / mainRotorSpeedDivider;
                _tailRotorRPM = _currentHp / tailRotorSpeedDivider;
            }
        }

        //float targetHP = PowerCurve.Evaluate(throttleInput) * MaxHP;
        //_currentHp = Mathf.Lerp(_currentHp, targetHP, Time.deltaTime * PowerDelay);

        //float targetRPM = PowerCurve.Evaluate(throttleInput) * MaxRPM;
        //_currentRPM = Mathf.Lerp(_currentRPM, targetRPM, Time.deltaTime * PowerDelay);
        //_normalizedRPM = Mathf.InverseLerp(0f, MaxRPM, _currentRPM);

        //_hudUI.Engine1Power.text = Mathf.Round(targetRPM).ToString() + " %";

    }

    private void UpdateHUDUI()
    {
        if (_hudUI)
        {
            _hudUI.EngineHP.text = Mathf.Round(_currentHp).ToString();
            _hudUI.Throttle.text = Mathf.Round(_throttle).ToString() + "%";
            _hudUI.Fuel.text = Mathf.Round(_apacheController.CurrentFuel).ToString();
        }
    }

    private void UpdateMechanicalMessage(string message, bool flash = true)
    {
        _hudUI.MechanicalMessage.text = message;
        if (flash)
        {
            _hudUI.MechanicalMessage.alpha = Mathf.Abs(1 * Mathf.Sin(Time.time));
        }
    }

    private void UpdateMainRotor()
    {
        _mainRotorController.RotorSpeed = _mainRotorRPM;
    }

    private void UpdateTailRotor()
    {
        _tailRotorController.RotorSpeed = _tailRotorRPM;
    }
}
