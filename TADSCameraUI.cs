using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TADSCameraUI : MonoBehaviour
{
    [SerializeField]
    private TADSCompassController _tadsCompassController;
    public TADSCompassController TADSCompassCtlr
    {
        get { return _tadsCompassController; }
        set { _tadsCompassController = value; }
    }

    [SerializeField]
    private RawImage _tadsCamera;
    public RawImage TADSCamera
    {
        get { return _tadsCamera; }
        set { _tadsCamera = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _zoom;
    public TextMeshProUGUI Zoom
    {
        get { return _zoom; }
        set { _zoom = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _targetLock;
    public TextMeshProUGUI TargetLock
    {
        get { return _targetLock; }
        set { _targetLock = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _targetName;
    public TextMeshProUGUI TargetName
    {
        get { return _targetName; }
        set { _targetName = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _distance;
    public TextMeshProUGUI Distance
    {
        get { return _distance; }
        set { _distance = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _altitude;
    public TextMeshProUGUI Altitude
    {
        get { return _altitude; }
        set { _altitude = value; }
    }
    /*
    [SerializeField]
    private TextMeshProUGUI _rangeToTarget;
    public TextMeshProUGUI RangeToTarget
    {
        get { return _rangeToTarget; }
        set { _rangeToTarget = value; }
    }
    */
    [SerializeField]
    private TextMeshProUGUI _tadsCameraZoom;
    public TextMeshProUGUI TADSCameraZoom
    {
        get { return _tadsCameraZoom; }
        set { _tadsCameraZoom = value; }
    }
}
