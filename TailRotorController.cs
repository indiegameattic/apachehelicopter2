using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailRotorController : RotorBase
{

    void Update()
    {
        base.HandleRotor(transform, base.RotorSpeed);
    }
}
