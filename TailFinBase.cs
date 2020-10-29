using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailFinBase : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _tailfins;
    public List<Transform> TailFins
    {
        get { return _tailfins; }
        set { _tailfins = value; }
    }

    protected virtual void HandleTailFin(InputController inpu)
    {

    }
}
