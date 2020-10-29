using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : MonoBehaviour
{
    [SerializeField]
    private GameObject _munitionPrefab;
    public GameObject MunitionPrefab
    {
        get { return _munitionPrefab; }
        set { _munitionPrefab = value; }
    }

    [SerializeField]
    private string _munitionType;
    public string MunitionType
    {
        get { return _munitionType; }
        set { _munitionType = value; }
    }

    [SerializeField]
    private string _indicatorUI;
    public string IndicatorUI
    {
        get { return _indicatorUI; }
        set { _indicatorUI = value; }
    }


}
