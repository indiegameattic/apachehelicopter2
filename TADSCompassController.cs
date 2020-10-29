using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TADSCompassController : CompassBase
{
    [SerializeField]
    private GameObject _source = null;

    //[SerializeField]
    private GameObject _target;
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    protected override void Start()
    {
        base.Start();

        //_source = GetComponent<GameObject>();

        //StartCoroutine(SetTargetCameraHeading());
    }

    protected override void Update()
    {
        base.Update();

        if (_target != null)
        {
            base.SetHeading(Utils.GetHeadingToTarget(_source.transform, _target.transform));
        }
    }

    private IEnumerator SetTargetCameraHeading()
    {
        while (_target != null)
        {
            //base.SetHeading(Utils.GetHeadingToTarget(_source.transform, _target.transform));

            
        }
        yield return null;
    }
}
