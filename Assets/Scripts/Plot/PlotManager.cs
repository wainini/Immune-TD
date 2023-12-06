using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlotManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private SelectablePlot plotPrefab;
    [SerializeField] private Vector3 tileOffset;

    [SerializeField] private Transform plotsParent;

    public SelectablePlot CurrentSelectedPlot { get; private set; }

    private void Start()
    {
        InitializePlots();
    }

    private void InitializePlots()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.GetTile(pos);
            Vector3 place = tilemap.CellToWorld(pos);
            if (tilemap.HasTile(pos))
            {
                Vector3 tileCenter = place + tileOffset;
                SelectablePlot p = Instantiate(plotPrefab, tileCenter, Quaternion.identity, plotsParent);
                p.InitializePlot(this);
            }
        }

        tilemap.gameObject.SetActive(false);
    }

    public void SetCurrentSelectedPlot(SelectablePlot currentSelectedPlot)
    {
        CurrentSelectedPlot = currentSelectedPlot;
    }
}
