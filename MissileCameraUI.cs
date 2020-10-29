using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissileCameraUI : MonoBehaviour
{
    [SerializeField]
    private CameraInfoUI _cameraInfoUI;
    public CameraInfoUI CameraInfoUI
    {
        get { return _cameraInfoUI; }
        set { _cameraInfoUI = value; }
    }

    [SerializeField]
    private Transform _missileCameraRIs;
    public Transform MissileCameraRIs
    {
        get { return _missileCameraRIs; }
        set { _missileCameraRIs = value; }
    }

    /*
    [SerializeField]
    private Camera _missileCamera;
    public Camera MissileCamera
    {
        get { return _missileCamera; }
        set { _missileCamera = value; }
    }
    */

    //[SerializeField]
    /*
    private RawImage _missileCameraRI;
    public RawImage MissileCameraRI
    {
        get { return _missileCameraRI; }
        set { _missileCameraRI = value; }
    }

    

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
    private TextMeshProUGUI _rangeToTarget;
    public TextMeshProUGUI RangeToTarget
    {
        get { return _rangeToTarget; }
        set { _rangeToTarget = value; }
    }
    */

    void Start()
    {
        //_missileCameraRI = GetComponentInChildren<RawImage>();
    }

    void Update()
    {
        
    }
}
