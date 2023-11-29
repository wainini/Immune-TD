using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlotManager : MonoBehaviour
{
    public static PlotManager Instance;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private SelectablePlot plotPrefab;
    [SerializeField] private Vector3 tileOffset;

    [SerializeField] private Transform plotsParent;

    public SelectablePlot CurrentSelectedPlot { get; private set; }

    private void Awake()
    {
        #region Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
        #endregion
    }

    private void Start()
    {
        InitializePlot();
    }

    private void InitializePlot()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.GetTile(pos);
            Vector3 place = tilemap.CellToWorld(pos);
            if (tilemap.HasTile(pos))
            {
                Vector3 tileCenter = place + tileOffset;
                Instantiate(plotPrefab, tileCenter, Quaternion.identity, plotsParent);
            }
        }

        tilemap.gameObject.SetActive(false);
    }

    public void SetCurrentSelectedPlot(SelectablePlot currentSelectedPlot)
    {
        CurrentSelectedPlot = currentSelectedPlot;
    }
}
