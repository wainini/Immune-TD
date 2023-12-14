using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlotManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapHigh, tilemapGround;
    [SerializeField] private SelectablePlot plotHighPrefab, plotGroundPrefab;
    [SerializeField] private Vector3 tileOffset;

    [SerializeField] private Transform plotsParent;

    public SelectablePlot CurrentSelectedPlot { get; private set; }

    private void Start()
    {
        InitializePlots();
    }

    private void InitializePlots()
    {
        foreach (var pos in tilemapHigh.cellBounds.allPositionsWithin)
        {
            tilemapHigh.GetTile(pos);
            Vector3 place = tilemapHigh.CellToWorld(pos);
            if (tilemapHigh.HasTile(pos))
            {
                Vector3 tileCenter = place + tileOffset;
                SelectablePlot p = Instantiate(plotHighPrefab, tileCenter, Quaternion.identity, plotsParent);
                p.InitializePlot(this);
            }
        }
        tilemapHigh.gameObject.SetActive(false);

        foreach (var pos in tilemapGround.cellBounds.allPositionsWithin)
        {
            tilemapGround.GetTile(pos);
            Vector3 place = tilemapGround.CellToWorld(pos);
            if (tilemapGround.HasTile(pos))
            {
                Vector3 tileCenter = place + tileOffset;
                SelectablePlot p = Instantiate(plotGroundPrefab, tileCenter, Quaternion.identity, plotsParent);
                p.InitializePlot(this);
            }
        }
        tilemapGround.gameObject.SetActive(false);
    }

    public void SetCurrentSelectedPlot(SelectablePlot currentSelectedPlot)
    {
        CurrentSelectedPlot = currentSelectedPlot;
    }
}
