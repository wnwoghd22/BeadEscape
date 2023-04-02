using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;

public class TileBoard : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private TileState[] tileStates;
    [SerializeField] private TileGrid grid;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject gameOverPanel;
    private List<Tile> tiles;
    Tile red, blue;
    private bool waiting;
    private bool gameOver;

    private void Awake()
    {
        gameOver = false;
        waiting = false;
        tiles = new List<Tile>(16);
    }
    private void Start()
    {
        CreateTile(3, 3, 1);
        CreateTile(1, 2, 2);
        CreateTile(2, 1, 3);
        CreateTile(1, 1, 4);
    }
    private void Update()
    {
        if (gameOver)
        {
            if (Input.anyKey)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (!waiting)
            HandleInput();
    }

    private void CreateTile(int i, int j, int k)
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[k], k);
        tile.Spawn(grid.GetCell(i, j));
        tiles.Add(tile);
        if (k == (int)eType.Red) red = tile;
        if (k == (int)eType.Blue) blue = tile;
    }
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTiles(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTiles(Vector2Int.left, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveTiles(Vector2Int.down, 0, 1, grid.Height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTiles(Vector2Int.right, grid.Width - 2, -1, 0, 1);
        }
    }
    private void MoveTiles(Vector2Int direction, int startX, int deltaX, int startY, int deltaY)
    {
        bool moved = false;
        for (int x = startX; 0 <= x && x < grid.Width; x += deltaX)
        {
            for (int y = startY; 0 <= y && y < grid.Height; y += deltaY)
            {
                TileCell cell = grid.GetCell(y, x);
                if (cell.IsBead)
                    moved |= MoveTile(cell.Tile, direction);
            }
        }
        if (moved) _ = WaitForChangesAsync();
            // StartCoroutine(WaitForChanges());
    }
    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.Cell, direction);
        while (adjacent != null)
        {
            if (adjacent.IsHole)
            {
                newCell = adjacent;
                break;
            }
            if (adjacent.IsOccupied) break;
            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }
        if (newCell != null)
        {
            tile.MoveTo(newCell);

            return true;
        }
        return false;
    }
    private async UniTaskVoid WaitForChangesAsync()
    {
        waiting = true;
        await UniTask.Delay(100);
        waiting = false;

        // TODO: check game over
        if (!blue.gameObject.activeSelf)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
        }
        else if (!red.gameObject.activeSelf)
        {
            gameOver = true;
            clearPanel.SetActive(true);
        }

    }
    private IEnumerator WaitForChanges()
    {
        waiting = true;
        yield return new WaitForSeconds(0.1f);
        waiting = false;

        // TODO: check game over
        if (!blue.gameObject.activeSelf)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
        }
        else if (!red.gameObject.activeSelf)
        {
            gameOver = true;
            clearPanel.SetActive(true);
        }
    }
}
