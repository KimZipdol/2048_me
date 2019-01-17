using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject gridTilePrefab = null;
    public GameObject tilePrefab = null;
    public Transform gridTilePanel = null;
    public Transform tilePanel = null;
    public int count = 4;

    Vector2 cellSize = Vector2.zero;
    Vector2 halfCellSize = Vector2.zero;
    RectTransform gridTilePanelRect = null;
    List<Tile> tiles = new List<Tile>();

    private void Awake()
    {
        gridTilePanelRect = gridTilePanel.gameObject.GetComponent<RectTransform>();
        cellSize = new Vector2(gridTilePanelRect.rect.width / count, gridTilePanelRect.rect.height / count);
        halfCellSize = cellSize * 0.5f;

        GenerateGrid();

        for (int i = 0; i < 4; ++i)
            RandomGenerateTile();
    }

    void GenerateGrid()
    {
        for(int x = 0; x < count; x++)
        {
            for(int y = 0; y < count; y++)
            {
                GenerateGridTile(gridTilePrefab, gridTilePanel, x, y, cellSize, string.Format("Grid Tile ({0}, {1})", x, y));
            }
        }
    }

    GameObject GenerateGridTile(GameObject prefab, Transform parent, int x, int y, Vector2 size, string name)
    {
        var tile = Instantiate(prefab, parent);
        tile.name = name;

        var tileRT = tile.GetComponent<RectTransform>();
        tileRT.localPosition = PointToVector3(x, y);
        tileRT.sizeDelta = cellSize;

        return tile;
    }

    Tile GenerateTile(GameObject prefab, Transform parent, int x, int y, Vector2 size)
    {
        var tileGO = GenerateGridTile(prefab, parent, x, y, size, name);

        var tile = tileGO.GetComponent<Tile>();
        tile.x = x;
        tile.y = y;
        tiles.Add(tile);
        tile.RefreshName();

        return tile;
    }

    void RandomGenerateTile()
    {
        var limit = 100000;
        for(int i=0;i<limit;++i)
        {
            var x = Random.Range(0, count);
            var y = Random.Range(0, count);

            var tile = GetTile(x, y);
            if (tile != null)
                continue;

            GenerateTile(tilePrefab, tilePanel, x, y, cellSize);
            break;
        }
    }

    Tile GetTile(int x, int y)
    {
        return tiles.Find(t => t.x == x && t.y == y);
    }

    Vector3 PointToVector3(int x, int y)
    {
        return new Vector3(+(x * cellSize.x + halfCellSize.x), -(y * cellSize.y + halfCellSize.y));
    }

}
