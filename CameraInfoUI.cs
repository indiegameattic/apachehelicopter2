using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraInfoUI : MonoBehaviour
{
    [SerializeField]
    private MissileCompassController _missileCompassController;
    public MissileCompassController MissileCompassController
    {
        get { return _missileCompassController; }
        set { _missileCompassController = value; }
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
    private TextMeshProUGUI _fuel;
    public TextMeshProUGUI Fuel
    {
        get { return _fuel; }
        set { _fuel = value; }
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

    [SerializeField]
    private TextMeshProUGUI _rangeToTarget;
    public TextMeshProUGUI RangeToTarget
    {
        get { return _rangeToTarget; }
        set { _rangeToTarget = value; }
    }
}
