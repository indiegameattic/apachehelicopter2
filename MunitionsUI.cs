using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MunitionsUI : MonoBehaviour
{

    [SerializeField]
    private MunitionsController _munitionsController = null;

    [SerializeField]
    private TextMeshProUGUI _gun;
    public TextMeshProUGUI Gun
    {
        get { return _gun; }
        set { _gun = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _hfm;
    public TextMeshProUGUI HFM
    {
        get { return _hfm; }
        set { _hfm = value; }
    }

    [SerializeField]
    private TextMeshProUGUI _hyr;
    public TextMeshProUGUI HYR
    {
        get { return _hyr; }
        set { _hyr = value; }
    }

    void Update()
    {
        HandleToggleMunitions();
    }

    private void HandleToggleMunitions()
    {
        switch (_munitionsController.CurrentMunition)
        {
            case 1:
                _gun.alpha = 0.254f;
                _hfm.alpha = 1f;
                _hyr.alpha = 0.254f;
                break;
            case 2:
                _gun.alpha = 0.254f;
                _hfm.alpha = 0.254f;
                _hyr.alpha = 1;
                break;
            default:
                _gun.alpha = 1f;
                _hfm.alpha = 0.254f;
                _hyr.alpha = 0.254f;
                break;
        }
    }
}
