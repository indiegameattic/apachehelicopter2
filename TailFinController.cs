using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailFinController : TailFinBase
{
    private InputController _inputController;

    void Awake()
    {
        _inputController = GetComponent<InputController>();
    }

    void FixedUpdate()
    {
        base.HandleTailFin(_inputController);
    }
}
