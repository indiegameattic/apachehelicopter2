using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellfireMissileController : MissileBase
{

    [Header("Missile Camera")]
    private Camera _missileCamera = null;

    [SerializeField]
    private MissileCameraUI _missileCameraUI;
    public MissileCameraUI MissileCameraUI
    {
        get { return _missileCameraUI; }
        set { _missileCameraUI = value; }
    }

    [SerializeField]
    private float _missileCameraSpeed = 125f;
    /*
    [SerializeField]
    private RenderTexture _missileCameraRT;
    public RenderTexture MissileCameraRT
    {
        get { return _missileCameraRT; }
        set { _missileCameraRT = value; }
    }
    */
    [SerializeField]
    private RawImage _missileCameraRI;
    public RawImage MissileCameraRI
    {
        get { return _missileCameraRI; }
        set { _missileCameraRI = value; }
    }

    [Header("Particle Systems")]
    [SerializeField]
    private ParticleSystem _explodeSmoke1;

    [SerializeField]
    private ParticleSystem _explodeSmoke2;

    private HellfireMissileController _controller;

    protected override void Start()
    {
        base.Start();
        base.HasMissileCamera = true;
        if (base.HasMissileCamera)
        {
            _missileCamera = GetComponentInChildren<Camera>();
        }
        _controller = GetComponent<HellfireMissileController>();
    }

    protected override void Update()
    {

        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleLaunch();
    }

    public GameObject Launch(GameObject missile, GameObject mount, Transform source, Transform target, MissileCameraUI missileCameraUI)
    {
        var missileGO = Instantiate(missile);
        missileGO.transform.position = mount.transform.position;
        missileGO.transform.rotation = mount.transform.rotation;

        var missileController = missileGO.GetComponent<HellfireMissileController>();
        missileController.Source = source;
        missileController.Target = target;
        missileController.HasMissileCamera = true;

        RawImage missileCameraRI = Instantiate(_missileCameraRI);
        missileCameraRI.transform.SetParent(missileCameraUI.MissileCameraRIs, false);
        missileCameraRI.enabled = true;
        missileCameraRI.gameObject.SetActive(true);

        CameraInfoUI cameraInfoUI = missileCameraRI.GetComponentInChildren<CameraInfoUI>();
        cameraInfoUI.MissileCompassController.gameObject.SetActive(true);
        cameraInfoUI.MissileCompassController.Source = source.gameObject;
        cameraInfoUI.MissileCompassController.Target = target.gameObject;
        //cameraInfoUI.TargetLock.gameObject.SetActive(true);
        //ameraInfoUI.TargetName.gameObject.SetActive(true);
        //cameraInfoUI.RangeToTarget.gameObject.SetActive(true);

        missileController.MissileCameraRI = missileCameraRI;
        missileController.MissileCameraUI = missileCameraUI;
        //missileController.MissileCameraUI.MissileCompassController.gameObject.SetActive(true);
        //missileController.MissileCameraUI.MissileCompassController.Target = target.gameObject;
        //missileController.MissileCameraUI.TargetLock.gameObject.SetActive(true);
        //missileController.MissileCameraUI.TargetName.gameObject.SetActive(true);
        //missileController.MissileCameraUI.RangeToTarget.gameObject.SetActive(true);

        missileController.IsLaunched = true;

        return missileGO;
    }

    protected override void HandleLaunch()
    {
        if (base.IsLaunched)
        {
            gameObject.transform.SetParent(null);

            if (!gameObject.GetComponent<Rigidbody>())
            {
                gameObject.AddComponent<Rigidbody>();
                base.Rb = gameObject.GetComponent<Rigidbody>();
                base.Rb.useGravity = false;
            }

            if (base.Fuel > 0)
            {
                float distanceAtGround = Utils.GetDistanceToTargetAtGround(gameObject.transform, base.Target);

                if (base.Target)
                {


                    if (base.HasMissileCamera)
                    {
                        if (_missileCameraRI && _controller.Target)
                        {
                            CameraInfoUI cameraInfoUI = _missileCameraRI.GetComponentInChildren<CameraInfoUI>();
                            cameraInfoUI.TargetName.text = _controller.Target.name;
                            cameraInfoUI.Altitude.text = Utils.GetDistanceToGround(gameObject.transform).ToString();
                            cameraInfoUI.Distance.text = Utils.GetDistanceToTarget(gameObject.transform, _controller.Target).ToString();
                            cameraInfoUI.Fuel.text = Mathf.Round((base.Fuel / base.MaxFuel) * 100).ToString() + "%";
                            //cameraInfoUI.RangeToTarget.text = Utils.GetDistanceToTarget(gameObject.transform, _controller.Target).ToString() + " ft / " + Utils.GetDistanceToGround(gameObject.transform) + " ft";
                        }
                        else
                        {
                            Debug.Log("NO _missileCameraRI");
                        }

                        if (_missileCamera)
                        {
                            Quaternion targetRotation = Quaternion.LookRotation(base.Target.position - _missileCamera.transform.position);

                            Quaternion cameraRotation = Quaternion.RotateTowards(_missileCamera.transform.rotation, targetRotation, _missileCameraSpeed * Time.deltaTime);

                            _missileCamera.transform.rotation = cameraRotation;
                        }
                    }

                    // Is Laser Guided
                    if (base.IsLaserGuided)
                    {

                        if (base.PeakHeight > 0f && base.PeakHeightDistance > 0f &&
                            distanceAtGround > base.PeakHeightDistance)
                        {
                            Vector3 aboveTarget = base.Target.position + new Vector3(0f, base.PeakHeight, 0f);

                            base.MissileRotation = Quaternion.LookRotation(aboveTarget - transform.position);
                        }
                        else
                        {
                            base.MissileRotation = Quaternion.LookRotation(base.Target.position - gameObject.transform.position);
                        }

                        base.Rb.MoveRotation(Quaternion.RotateTowards(gameObject.transform.rotation, base.MissileRotation, base.TurnSpeed));
                    }
                }
                else
                {
                    base.Rb.useGravity = true;
                }
            }
        }
        base.HandleLaunch();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (LayerMask.LayerToName(other.gameObject.layer) == "Ground")
        {
            Debug.Log(LayerMask.LayerToName(other.gameObject.layer));

            var controller = gameObject.GetComponent<HellfireMissileController>();
            controller.MissileCameraRI.enabled = false;
            controller.MissileCameraRI.gameObject.SetActive(false);

            var missileCameraUI = controller.MissileCameraUI;

            if (base.SmokeTower)
            {
                var smokeTower = Instantiate(base.SmokeTower);
                smokeTower.transform.position = transform.position;
                smokeTower.Play();
            }

            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Target")
        {
            Debug.Log(other.gameObject.name);
            Utils.Explode(other.transform, Random.Range(5f, 25f), Random.Range(20f, 200f), Random.Range(20f, 120f), "TargetCamera");

            if (base.RocketExhaust)
            {
                base.RocketExhaust.Stop();
                base.RocketExhaust.transform.SetParent(null);
            }

            if (_explodeSmoke1)
            {
                var explodeSmoke1 = Instantiate(_explodeSmoke1);
                explodeSmoke1.transform.position = other.transform.position;
                explodeSmoke1.Play();

                Destroy(explodeSmoke1, 14f);
            }

            if (_explodeSmoke2)
            {
                var explodeSmoke2 = Instantiate(_explodeSmoke2);
                explodeSmoke2.transform.position = other.transform.position;
                explodeSmoke2.Play();

                Destroy(explodeSmoke2, 20f);
            }
            

            Destroy(other.gameObject, 8f);
            
            

            //Destroy(gameObject);
        }
    }
}
