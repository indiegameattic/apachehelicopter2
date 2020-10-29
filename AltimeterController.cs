using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ApacheController))]
public class AltimeterController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _altimeterNeedles;
    public List<GameObject> AltimeterNeedles
    {
        get { return _altimeterNeedles; }
        set { _altimeterNeedles = value; }
    }

    [SerializeField]
    private ApacheController _apacheController = null;

    [SerializeField]
    private TextMeshProUGUI _altimeterThousands = null;

    [SerializeField]
    private TextMeshProUGUI _altimeterHundreds = null;

    [SerializeField]
    private TextMeshProUGUI _altimeterVerticalVelocity = null;

    private float _distanceToGround = 0f;
    private string _thousands = "";
    private string _hundreds = "";

    void Start()
    {
        //_apacheController = GetComponent<ApacheController>();
        //_altimeterThousands = GetComponent<TextMeshProUGUI>();
        //_altimeterHundreds = GetComponent<TextMeshProUGUI>();
        //_altimeterVerticalVelocity = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //Debug.Log(_apacheController.DistanceToGround);
        SetAltimeter();
    }

    private void SetAltimeter()
    {
        // Altitude
        _distanceToGround = _apacheController.DistanceToGround;
        _altimeterNeedles[0].transform.eulerAngles = new Vector3(0f, 0f, _distanceToGround * -0.036f);
        _altimeterNeedles[1].transform.eulerAngles = new Vector3(0f, 0f, _distanceToGround * -0.36f);

        _thousands = (_distanceToGround / 1000).ToString();
        if (_thousands.IndexOf(".") > 0)
        {
            _thousands = _thousands.Substring(0, _thousands.IndexOf("."));
        }
        _altimeterThousands.text = _thousands.ToString();

        _hundreds = (_distanceToGround / 1000).ToString("F3");
        if (_hundreds.IndexOf(".") > 0)
        {
            _hundreds = _hundreds.Substring(_hundreds.IndexOf(".") + 1, (_hundreds.Length - _hundreds.IndexOf(".") - 1));
        }
        _altimeterHundreds.text = _hundreds.ToString();

        // Vertical Speed/Rate
        float verticalSpeed = Mathf.Abs(_apacheController.LocalVelocity.y);
        _altimeterVerticalVelocity.text = String.Format("{0:000.0}", verticalSpeed);
    }
}
