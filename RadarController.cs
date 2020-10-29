using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : Target, ITargetable
{
    
    [SerializeField]
    private Transform _turret = null;

    [SerializeField]
    private float _radarSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _radarSpeed = 5f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        // Spin Radar Turret
        _turret.Rotate(Vector3.up, _radarSpeed);
    }
}
