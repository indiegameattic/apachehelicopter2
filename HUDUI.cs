using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _engineHP;
    public TextMeshProUGUI EngineHP
    {
        get { return _engineHP; }
        set { _engineHP = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _throttle;
    public TextMeshProUGUI Throttle
    {
        get { return _throttle; }
        set { _throttle = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _fuel;
    public TextMeshProUGUI Fuel
    {
        get { return _fuel; }
        set { _fuel = value; }
    }
    /*
    [SerializeField]
    private TextMeshProUGUI _engine2Power;
    public TextMeshProUGUI Engine2Power
    {
        get { return _engine2Power; }
        set { _engine2Power = value; }
    }
    */
    [SerializeField]
    private TextMeshProUGUI _munitionsMessage;
    public TextMeshProUGUI MunitionsMessage
    {
        get { return _munitionsMessage; }
        set { _munitionsMessage = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _mechanicalMessage;
    public TextMeshProUGUI MechanicalMessage
    {
        get { return _mechanicalMessage; }
        set { _mechanicalMessage = value; }
    }
}
