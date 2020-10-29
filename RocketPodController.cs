using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPodController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _rocketTubes;

    [SerializeField]
    private Transform _hydraRocket;

    void Awake()
    {
        _rocketTubes = new List<GameObject>();

        
    }

    void Start()
    {
        //Debug.Log(transform.childCount);
        GetRocketTubes();
    }

    private void GetRocketTubes()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _rocketTubes.Add(transform.GetChild(i).gameObject);
        }

        if (_hydraRocket)
        {
            LoadHydraRockets(_hydraRocket);
        }
        
    }

    private void LoadHydraRockets(Transform hydraRocket)
    {
        //foreach (var tube in _rocketTubes)
        //{
            //Debug.Log(tube.name);
            //var rocket = Instantiate(hydraRocket);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
