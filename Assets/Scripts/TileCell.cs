using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCell : MonoBehaviour
{
    public Vector2Int Coordinates { get; set; }
    public Tile Tile { get; set; }
    public bool IsEmpty => Tile == null;
    public bool IsOccupied => Tile != null;
    public bool IsHole => Tile != null && Tile.Type == eType.Hole;
    public bool IsBead => Tile != null && (Tile.Type == eType.Red || Tile.Type == eType.Blue);
}
