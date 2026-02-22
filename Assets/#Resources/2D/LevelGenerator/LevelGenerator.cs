using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public enum Grid
    {
        WIND,
        EMPTY
    }
    private Grid[,] gridHandler;
    [SerializeField] private Tilemap Tmap;
    [SerializeField] private Tile m_windTileAsset;
    [SerializeField] private Tile m_defaultTileAsset;
    [SerializeField] private int m_mapWidth;
    [SerializeField] private int m_mapHeight;

    private void Start()
    {
        IntialiseGrid();
        Tmap.SetTile(new Vector3Int(0,0,0), m_windTileAsset);
        Tmap.SetTile(new Vector3Int(1, 0, 0), m_defaultTileAsset);

    }

    private void IntialiseGrid()
    {
        gridHandler = new Grid[m_mapWidth, m_mapHeight];

        for (int x = 0; x < gridHandler.GetLength(0); x++)
        {
            for (int y = 0; y < gridHandler.GetLength(1); y++)
            {
                gridHandler[x,y] = Grid.EMPTY;
            }
        }
    }

    private void ClearMap(bool complete)
    {
        Tmap.ClearAllTiles();
    }
}
