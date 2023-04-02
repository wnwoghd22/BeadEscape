using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileData
{
    public int type;
    public int col;
    public int row;
}

[System.Serializable]
public class BoardData
{
    [SerializeField] private int col;
    public int Col { get => col; set => col = value; }
    [SerializeField] private int  row;
    public int Row { get => row; set => row = value; }
    [SerializeField] private float tileSize;
    public float TileSize { get => tileSize; set => tileSize = value; }
    [SerializeField] private TileData[] tiles;
    public TileData[] Tiles => tiles;
}