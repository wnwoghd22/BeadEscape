using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    BoardData board;
    public BoardData Board => board;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }

    public void StartStage(int i) => _ = StartStageAsync(i);
    private async UniTask StartStageAsync(int i)
    {
        board = await FileManager.GetStageFileAsync<BoardData>(i);
        Debug.Log(board.Col + ", " + board.Row);
        Debug.Log(board.Tiles);
        foreach (TileData tile in board.Tiles)
        {
            Debug.Log(tile.type + ", " + tile.col + ", " + tile.row);
        }
        SceneManager.LoadScene("Board" + board.Row + "x" + board.Col);
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("Main");
    }
}