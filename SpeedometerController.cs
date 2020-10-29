using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ApacheController))]
public class SpeedometerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _altimeterNeedles = null;

    [SerializeField]
    private float _calibrate = 0f;

    public GameObject AltimeterNeedles
    {
        get { return _altimeterNeedles; }
        set { _altimeterNeedles = value; }
    }

    [SerializeField]
    private ApacheController _apacheController = null;

    void Start()
    {
        _calibrate = -3f;
        //_apacheController = GetComponent<ApacheController>();
        //_altimeterNeedles = GetComponent<GameObject>();
    }

    void Update()
    {
        SetSpeedometer();
    }

    private void SetSpeedometer()
    {
        float airSpeedKmph = Mathf.Abs(_apacheController.LocalVelocity.z);

        _altimeterNeedles.transform.eulerAngles = new Vector3(0f, 0f, airSpeedKmph * _calibrate);
    }
}
