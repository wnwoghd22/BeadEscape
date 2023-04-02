using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public enum eType
{
    Empty = 0,
    Hole = 1,
    Red = 2,
    Blue = 3,
}

public class Tile : MonoBehaviour
{
    public TileState State { get; private set; }
    public TileCell Cell { get; private set; }
    public eType Type { get; private set; }
    [SerializeField] private Image background;

    public void SetState(TileState state, int type)
    {
        State = state;
        Type = (eType)type;
        background.color = State.backgroundColor;
        background.sprite = State.backgroundImage;
    }
    public void Spawn(TileCell cell)
    {
        if (Cell != null)
            Cell.Tile = null;
        Cell = cell;
        Cell.Tile = this;
        transform.position = Cell.transform.position;
    }
    public void MoveTo(TileCell cell)
    {
        if (Cell != null)
            Cell.Tile = null;
        Cell = cell;
        if (Cell.IsHole)
        {

        }
        else Cell.Tile = this;
        _ = MoveAsync(Cell.transform.position);
        // StartCoroutine(Move(Cell.transform.position));
    }
    private async UniTaskVoid MoveAsync(Vector3 to)
    {
        float elapsed = 0f;
        float duration = .1f;
        Vector3 from = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            await UniTask.Yield();
        }
        transform.position = to;
        if (Cell.IsHole)
        {
            gameObject.SetActive(false);
        }
    }
    private IEnumerator Move(Vector3 to)
    {
        float elapsed = 0f;
        float duration = .1f;
        Vector3 from = transform.position;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = to;
        if (Cell.IsHole)
        {
            gameObject.SetActive(false);
        }
    }
}
