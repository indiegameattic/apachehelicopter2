using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TADSCameraController : MonoBehaviour
{
    [SerializeField]
    private float _tadsCameraSpeed = 0f;

    [SerializeField]
    private List<GameObject> _targets;
    public List<GameObject> Targets
    {
        get { return _targets; }
        set { _targets = value; }
    }

    [SerializeField]
    private int _currentTarget;
    public int CurrentTarget
    {
        get { return _currentTarget; }
        set { _currentTarget = value; }
    }

    private Camera _tadsCamera;

    [SerializeField]
    private float _zoom = 30f;

    private MunitionsController _munitionsController;

    private InputController _inputController;

    [SerializeField]
    private TADSCameraUI _tadsCameraUI = null;

    [SerializeField]
    private UnityEvent _tadsCameraZoomOnMouseOver = new UnityEvent();

    void Awake()
    {
        _tadsCamera = GetComponent<Camera>();
        _munitionsController = GetComponentInParent<MunitionsController>();
        _inputController = GetComponentInParent<InputController>();
    }

    void Start()
    {
        _tadsCameraSpeed = 30f;
        _targets = new List<GameObject>();

        //_apacheController = GetComponent<ApacheController>();
        //_inputController = GetComponent<InputController>();
        //_tadsCameraUI = GetComponent<TADSCameraUI>();

        StartCoroutine(GetTargets());
    }

    void Update()
    {
        HandleTarget();

        //var x = EventSystem.current.IsPointerOverGameObject();

        //Debug.Log(raysastResults);

        //HandleCameraZoom();

        HandleTadsCameraZoom();
    }

    private void OnMouseOverTADSCameraZoom()
    {
        
    }

    private void HandleCameraZoom()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        //EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        Debug.Log(results.Count);

        foreach (var item in results)
        {
            Debug.Log(item.gameObject.name);
        }
    }

    private void HandleTarget()
    {
        // Tab key
        if (_targets.Count > 0 && _inputController.Targets)
        {
            _currentTarget++;
        }
    }

    private void HandleTadsCameraZoom()
    {
        if (_targets.Count > 0)
        {
            //Debug.Log(_inputController.TadsCameraZoom);
            if (_inputController.TadsCameraZoom != 0)
            {
                _zoom += -10f * _inputController.TadsCameraZoom;

                _zoom = Mathf.Clamp(_zoom, 1f, 60f);

                _tadsCamera.fieldOfView = _zoom;
            }
            _tadsCameraUI.Zoom.text = _zoom.ToString();
        }
    }

    void FixedUpdate()
    {
        if (_targets.Count > 0)
        {
            _tadsCameraUI.gameObject.SetActive(true);

            if (_currentTarget > _targets.Count - 1)
                _currentTarget = 0;

            SetCameraTarget(_currentTarget);

            GetTargetLock(_targets[_currentTarget]);

            SetTADSCameraUI(_targets[_currentTarget]);
        }
        else
        {
            _tadsCameraUI.gameObject.SetActive(false);
        }
    }

    private void GetTargetLock(GameObject target)
    {
        if (target)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (target.transform.position - transform.position), out hit))
            {
                if (hit.collider.gameObject == target)
                {
                    _tadsCameraUI.TargetLock.text = "Locked:" + Utils.ToFeet(hit.distance).ToString() + " ft";
                    _tadsCameraUI.TargetLock.alpha = 1f;
                }
                else
                {
                    _tadsCameraUI.TargetLock.text = "[NO LOCK]";
                    _tadsCameraUI.TargetLock.alpha = Mathf.Abs(1 * Mathf.Sin(Time.time));
                }
            }
        }
    }

    private void SetTADSCameraUI(GameObject target)
    {
        if (target)
        {
            _tadsCameraUI.TADSCompassCtlr.Target = target;
            //_tadsCameraUI.Angle.text = Vector3.Angle(target.transform.position - _apacheController.LocalTransform.position, _apacheController.LocalTransform.right).ToString();
            _tadsCameraUI.TargetName.text = target.name;
            _tadsCameraUI.Distance.text = Utils.GetDistanceToTarget(transform, target.transform).ToString();

            //_tadsCameraUI.RangeToTarget.text = Utils.GetDistanceToTarget(transform, target.transform).ToString() + " ft";
        }
        
    }

    private void SetCameraTarget(int currentTarget)
    {
        if (_targets[currentTarget])
        {
            GameObject target = _targets[currentTarget];

            Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

            Quaternion tadsRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _tadsCameraSpeed * Time.deltaTime);
            transform.rotation = tadsRotation;
        }
        
    }

    private IEnumerator GetTargets()
    {
        // While Application is "enabled"
        while (enabled)
        {
            // Pause for 3 seconds
            yield return new WaitForSeconds(3);

            _targets = new List<GameObject>();
            var _targetables = GameObject.FindObjectsOfType<Target>();
            foreach (var t in _targetables)
            {
                
                if (t.gameObject.activeSelf)
                {
                    _targets.Add(t.gameObject);
                }
            }

        }

    }
}
