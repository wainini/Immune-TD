using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlotManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject plotPrefab;
    [SerializeField] private Vector3 tileOffset;

    private void Start()
    {
        foreach(var pos in tilemap.cellBounds.allPositionsWithin)
        {
            tilemap.GetTile(pos);
            Vector3 place = tilemap.CellToWorld(pos);
            if (tilemap.HasTile(pos))
            {
                Vector3 tileCenter = place + tileOffset;
                Instantiate(plotPrefab, tileCenter, Quaternion.identity);
            }
        }

        tilemap.gameObject.SetActive(false);
    }
}
