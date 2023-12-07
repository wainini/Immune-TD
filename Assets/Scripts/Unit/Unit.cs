using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitDetection detection;

    private void Awake()
    {
        detection.SetUnit(this);
    }

}
