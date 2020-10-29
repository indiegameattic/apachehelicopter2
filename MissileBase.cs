using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMissile
{

}

public class MissileBase : MonoBehaviour, IMissile
{
    [SerializeField]
    private Transform _source;
    public Transform Source
    {
        get { return _source; }
        set { _source = value; }
    }

    [SerializeField]
    private Transform _target;
    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }

    [SerializeField]
    private bool _isArmed = false;
    public bool IsArmed
    {
        get { return _isArmed; }
        set { _isArmed = value; }
    }

    [SerializeField]
    private bool _isLaunched = false;
    public bool IsLaunched
    {
        get { return _isLaunched; }
        set { _isLaunched = value; }
    }

    [SerializeField]
    private float _speed = 10f;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField]
    private float _turnSpeed = 0.65f;
    public float TurnSpeed
    {
        get { return _turnSpeed; }
        set { _turnSpeed = value; }
    }

    [SerializeField]
    private float _peakHeight = 250f;
    public float PeakHeight
    {
        get { return _peakHeight; }
        set { _peakHeight = value; }
    }

    [SerializeField]
    private float _peakHeightDistance = 150f;
    public float PeakHeightDistance
    {
        get { return _peakHeightDistance; }
        set { _peakHeightDistance = value; }
    }

    [SerializeField]
    private float _maxFuel = 60f;
    public float MaxFuel
    {
        get { return _maxFuel; }
        set { _maxFuel = value; }
    }

    [SerializeField]
    private float _fuel;
    public float Fuel
    {
        get { return _fuel; }
        set { _fuel = value; }
    }

    private Rigidbody _rb;
    public Rigidbody Rb
    {
        get { return _rb; }
        set { _rb = value; }
    }

    private Quaternion _missileRotation;
    public Quaternion MissileRotation
    {
        get { return _missileRotation; }
        set { _missileRotation = value; }
    }

    private bool _hasMissileCamera;
    public bool HasMissileCamera
    {
        get { return _hasMissileCamera; }
        set { _hasMissileCamera = value; }
    }

    private bool _isLaserGuided;
    public bool IsLaserGuided
    {
        get { return _isLaserGuided; }
        set { _isLaserGuided = value; }
    }

    /*
    private bool _missileCameraExists;
    public bool MissileCameraExists
    {
        get { return _missileCameraExists; }
        set { _missileCameraExists = value; }
    }
    */

    [Header("Particle Systems")]
    [SerializeField]
    private ParticleSystem _rocketEngine;
    public ParticleSystem RocketEngine
    {
        get { return _rocketEngine; }
        set { _rocketEngine = value; }
    }

    [SerializeField]
    private ParticleSystem _rocketExhaust;
    public ParticleSystem RocketExhaust
    {
        get { return _rocketExhaust; }
        set { _rocketExhaust = value; }
    }

    private Light _rocketEngineLight;

    [SerializeField]
    private ParticleSystem _rocketLaunch;
    public ParticleSystem RocketLaunch
    {
        get { return _rocketLaunch; }
        set { _rocketLaunch = value; }
    }

    [SerializeField]
    private ParticleSystem _smokeTower;
    public ParticleSystem SmokeTower
    {
        get { return _smokeTower; }
        set { _smokeTower = value; }
    }


    protected virtual void Awake()
    {
        _hasMissileCamera = false;
        _isLaserGuided = true;
        //_missileCameraExists = false;
        _fuel = _maxFuel;
    }

    protected virtual void Start()
    {
        _rocketEngine = GetComponentsInChildren<ParticleSystem>()[0];
        _rocketExhaust = GetComponentsInChildren<ParticleSystem>()[1];
        _rocketLaunch = GetComponentsInChildren<ParticleSystem>()[2];
        _rocketEngineLight = GetComponentInChildren<Light>();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void HandleLaunch()
    {
        if (_isLaunched)
        {
            if (_fuel > 0)
            {
                _fuel -= Time.deltaTime;

                _rocketLaunch.Play();
                //_rb.AddForce(transform.forward * _speed);

                _rb.velocity = transform.forward * _speed;

                _rocketEngine.Play();
                _rocketEngineLight.enabled = true;
                _rocketExhaust.Play();
            }
            else
            {
                // Out of fuel
                _rb.useGravity = true;

                if (_rocketExhaust)
                {
                    _rocketEngine.Stop();
                    _rocketEngineLight.enabled = false;
                    _rocketExhaust.Stop();
                    _rocketExhaust.transform.SetParent(null);
                    Destroy(_rocketExhaust, 10f);
                }

                Destroy(gameObject, 2f);
                
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

    }
}
