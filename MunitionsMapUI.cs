using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MunitionsMapUI : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> _hellfireMissiles;
    public List<TextMeshProUGUI> HellfireMissiles
    {
        get { return _hellfireMissiles; }
        set { _hellfireMissiles = value; }
    }
}
