using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCompassController : CompassBase
{
    [SerializeField]
    private GameObject _source;
    public GameObject Source
    {
        get { return _source; }
        set { _source = value; }
    }

    private GameObject _target;
    public GameObject Target
    {
        get { return _target; }
        set { _target = value; }
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();

        if (_source &&_target)
        {
            base.SetHeading(Utils.GetHeadingToTarget(_source.transform, _target.transform));
        }
    }
}
