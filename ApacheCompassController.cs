using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ApacheController))]
public class ApacheCompassController : CompassBase
{
    [SerializeField]
    private ApacheController _apacheController = null;

    protected override void Start()
    {
        base.Start();
        //_apacheController = GetComponent<ApacheController>();
    }

    protected override void Update()
    {
        base.Update();

        base.SetHeading(Mathf.Round(_apacheController.LocalEulerAngles.y));
    }
}
