using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ApacheController))]
public class AttitudeController : MonoBehaviour
{
    [SerializeField]
    private GameObject _attitudeRuler = null;

    [SerializeField]
    private float _calibrate;

    [SerializeField]
    private ApacheController _apacheController = null;

    void Start()
    {
        _calibrate = 305f;
        //_apacheController = GetComponent<ApacheController>();
        //_attitudeRuler = GetComponent<GameObject>();
    }


    void Update()
    {
        SetAttitude();
    }

    private void SetAttitude()
    {
        var _localEulerAngles = _apacheController.LocalEulerAngles;
        _attitudeRuler.transform.rotation = Quaternion.Slerp(_attitudeRuler.transform.rotation, Quaternion.Euler(0f, 0f, _localEulerAngles.z), 3f * Time.deltaTime);

        float fixAttitudeY = _calibrate + ((_localEulerAngles.x > 180 ? (360 - _localEulerAngles.x) * -1f : _localEulerAngles.x) * 3f);

        _attitudeRuler.transform.position = new Vector3(_attitudeRuler.transform.position.x, fixAttitudeY, _attitudeRuler.transform.position.z);
    }
}
