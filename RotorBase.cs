using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorBase : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _rotors;
    public List<Transform> Rotors
    {
        get { return _rotors; }
        set { _rotors = value; }
    }

    private float _maxRotorSpeed = 60f;
    public float MaxRotorSpeed
    {
        get { return _maxRotorSpeed; }
        set { _maxRotorSpeed = value; }
    }

    private float _rotorSpeed = 0f;
    public float RotorSpeed
    {
        get { return _rotorSpeed; }
        set { _rotorSpeed = value; }
    }

    protected virtual void HandleRotor(Transform rotorMast, float rotorSpeed)
    {
        rotorMast.transform.Rotate(Vector3.up, rotorSpeed);
    }
}
