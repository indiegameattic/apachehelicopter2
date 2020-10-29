using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MunitionsController : MonoBehaviour
{
    private ApacheController _apacheController;

    private InputController _inputController;

    private TADSCameraController _tadsCameraController;

    [SerializeField]
    private TADSCameraUI _tadsCameraUI = null;

    [SerializeField]
    private HUDUI _hudUI = null;

    [Header("Hellfire Missiles")]
    [SerializeField]
    private Transform _hellfireMissileQuad1 = null;

    [SerializeField]
    private Transform _hellfireMissileQuad2 = null;

    [SerializeField]
    private GameObject _hellfireMissile = null;

    [SerializeField]
    private List<GameObject> _hellfireMissiles;

    [Header("Missile Camera")]
    [SerializeField]
    private MissileCameraUI _missileCameraUI = null;
    public MissileCameraUI MissileCameraUI
    {
        get { return _missileCameraUI; }
        set { _missileCameraUI = value; }
    }

    [SerializeField]
    private RenderTexture _missileCameraRT;
    public RenderTexture MissileCameraRT
    {
        get { return _missileCameraRT; }
        set { _missileCameraRT = value; }
    }

    [SerializeField]
    private RawImage _missileCameraRI;

    public List<GameObject> HellfireMissiles
    {
        get { return _hellfireMissiles; }
        set { _hellfireMissiles = value; }
    }

    [Header("Hydra Rockets")]
    [SerializeField]
    private Transform _hydraRocketPod1 = null;

    [SerializeField]
    private Transform _hydraRocketPod2 = null;

    [SerializeField]
    private List<GameObject> _hydraRockets;

    [SerializeField]
    private Transform _hydraRocket;


    [Header("Munitions")]
    [SerializeField]
    private MunitionsUI _munitionsUI = null;

    [SerializeField]
    private MunitionsMapUI _munitionsMapUI = null;

    [SerializeField]
    private int _currentMunition;
    public int CurrentMunition
    {
        get { return _currentMunition; }
        set { _currentMunition = value; }
    }

    private bool _allowLoadMunitions = true;
    private bool allowToggleMuntion = true;
    private bool allowLoadHellfireMissiles = true;
    private bool allowMissileLaunch = true;
    private int hellfireCount = 0;

    void Awake()
    {
        _apacheController = GetComponent<ApacheController>();
        _inputController = GetComponent<InputController>();
        _tadsCameraController = GetComponentInChildren<TADSCameraController>();
    }

    void Start()
    {
        hellfireCount = 0;
    }

    void Update()
    {
        HandleLoadHellfireMissiles();
        HandleLaunchMissile();
        HandleToggleMunitions();
    }

    private void HandleLoadHellfireMissiles()
    {
        if (_inputController.LoadHellfireMissilesInput && allowLoadHellfireMissiles)
        {

            StartCoroutine(LoadMunitions(_hellfireMissileQuad1, _hellfireMissile));
            StartCoroutine(LoadMunitions(_hellfireMissileQuad2, _hellfireMissile));

            for (int i = 0; i < _hellfireMissileQuad1.childCount; i++)
            {
                Transform mount = _hellfireMissileQuad1.GetChild(i);

                if (mount.GetChild(0))
                {
                    _hellfireMissiles.Add(mount.GetChild(0).gameObject);
                }
            }

            for (int i = 0; i < _hellfireMissileQuad2.childCount; i++)
            {
                Transform mount = _hellfireMissileQuad2.GetChild(i);

                if (mount.GetChild(0))
                {
                    _hellfireMissiles.Add(mount.GetChild(0).gameObject);
                }
            }
        }
    }

    private void HandleToggleMunitions()
    {
        if (_inputController.ToggleMunitionsInput && allowToggleMuntion)
        {
            StartCoroutine(ToggleMunition());
        }
    }

    IEnumerator ToggleMunition()
    {
        allowToggleMuntion = false;

        _currentMunition++;

        if (_currentMunition > 2)
        {
            _currentMunition = 0;
        }

        yield return new WaitForSeconds(0.1f);

        allowToggleMuntion = true;
    }

    private void HandleLaunchMissile()
    {
        if (_inputController.LaunchMissileInput > 0f)
        {
            if (allowMissileLaunch)
            {
                if (_hellfireMissiles.Count > 0 && _hellfireMissiles[0].transform)
                {
                    if (_apacheController.DistanceToGround > 5f)
                    {
                        //Debug.Log("Fire! " + missile.name);
                        //Debug.Log("Source: " + missileController.Source);
                        //Debug.Log("Target: " + missileController.Target + ":" + _tadsCameraController.CurrentTarget);
                        //Debug.Log("Target:" + missile.GetComponent<HellfireMissileController>().Target);

                        StartCoroutine(Launch());
                    }
                    else
                    {
                        _hudUI.MunitionsMessage.text = "[LOW ALT]";
                        StartCoroutine(ClearText(_hudUI.MunitionsMessage));
                    }
                }
                else
                {
                    _hudUI.MunitionsMessage.text = "[OUT OF HFM]";
                    StartCoroutine(ClearText(_hudUI.MunitionsMessage, 3f));
                }
            }
            else
            {
                _hudUI.MunitionsMessage.text = "";
            }
        }
    }

    IEnumerator ClearText(TextMeshProUGUI tmp, float delay = 5f)
    {
        yield return new WaitForSeconds(delay);

        tmp.text = "";
    }

    IEnumerator Launch()
    {
        _hudUI.MunitionsMessage.text = "";

        allowMissileLaunch = false;

        HellfireMissileController missileController = _hellfireMissiles[0].GetComponent<HellfireMissileController>();

        GameObject missileGO = missileController.Launch(
            _hellfireMissile,
            _hellfireMissiles[0],
            _apacheController.LocalTransform,
            _tadsCameraController.Targets[_tadsCameraController.CurrentTarget].transform,
            _missileCameraUI);

        Destroy(_hellfireMissiles[0]);

        _munitionsMapUI.HellfireMissiles[_hellfireMissiles.Count - 1].text = "0";
        _hellfireMissiles.RemoveAt(0);

        yield return new WaitForSeconds(3f);

        allowMissileLaunch = true;
    }

    IEnumerator LoadMunitions(Transform munitionMount, GameObject munition)
    {
        _allowLoadMunitions = false;

        for (int i = 0; i < munitionMount.childCount; i++)
        {
            Transform mount = munitionMount.GetChild(i);
            GameObject missile = Instantiate(munition, mount.position, Quaternion.identity);
            missile.transform.rotation = _apacheController.LocalRotation;
            missile.transform.parent = mount;
            missile.transform.name = munition.name + i + "-" + Guid.NewGuid();
        }

        yield return new WaitForSeconds(30f);

        _allowLoadMunitions = true;
    }
    /*
    IEnumerator LoadHellfireMissiles(Transform munitionMount, GameObject munition)
    {
        allowLoadHellfireMissiles = false;

        for (int i = 0; i < munitionMount.childCount; i++)
        {
            Transform mount = munitionMount.GetChild(i);
            GameObject missile = Instantiate(munition, mount.position, Quaternion.identity);
            missile.transform.rotation = _apacheController.LocalRotation;
            missile.transform.parent = mount;
            missile.transform.name = munition.name + i + "-" + Guid.NewGuid();

            _hellfireMissiles.Add(missile);
            _munitionsMapUI.HellfireMissiles[hellfireCount].text = "H";
            hellfireCount++;
        }

        yield return new WaitForSeconds(30f);

        allowLoadHellfireMissiles = true;
    }
    */
}
