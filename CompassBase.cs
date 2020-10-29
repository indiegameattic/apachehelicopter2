using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(GameObject), typeof(TextMeshProUGUI))]
public class CompassBase : MonoBehaviour
{
    [SerializeField]
    private GameObject _compassRuler;
    public GameObject CompassRuler
    {
        get { return _compassRuler; }
        set { _compassRuler = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _compassHeading;
    public TextMeshProUGUI CompassHeading
    {
        get { return _compassHeading; }
        set { _compassHeading = value; }
    }

    [SerializeField]
    private float _calibrate = 0f;
    public float Calibrate
    {
        get { return _calibrate; }
        set { _calibrate = value; }
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void SetHeading(float heading)
    {
        float compassHeading = heading;
        if (heading > 0)
        {
            compassHeading = -heading + Calibrate;
        }
        CompassHeading.text = heading.ToString();
        CompassRuler.transform.localPosition = new Vector3(compassHeading, 0f, 0f);
    }
}
