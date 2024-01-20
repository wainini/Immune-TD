using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneCell : Unit
{
    [SerializeField] protected ImmuneCellData data;

    public ImmuneCellData GetCellData() { return data; }
}
