using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    public TileRow[] Rows { get; private set; }
    public TileCell[] Cells { get; private set; }
    public int Size => Cells.Length;
    public int Height => Rows.Length;
    public int Width => Size / Height;

    private void Awake()
    {
        Rows = GetComponentsInChildren<TileRow>();
        Cells = GetComponentsInChildren<TileCell>();
    }
    private void Start()
    {
        for (int i = 0; i < Rows.Length; ++i)
        {
            for (int j = 0; j < Rows[i].Cells.Length; ++j)
            {
                Rows[i].Cells[j].Coordinates = new Vector2Int(j, i);
            }
        }
    }
    public TileCell GetCell(int i, int j)
    {
        if (i < 0 || i >= Height || j < 0 || j >= Width)
            return null;
        return Rows[i].Cells[j];
    }
    public TileCell GetCell(Vector2Int pos) => GetCell(pos.y, pos.x);
    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int pos = cell.Coordinates;
        pos.x += direction.x;
        pos.y -= direction.y;
        return GetCell(pos);
    }
}
