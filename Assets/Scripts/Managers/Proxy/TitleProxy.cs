using UnityEngine;

public class TitleProxy : Proxy
{
    public void StartStage(int i)
    {
        GameManager.Instance.StartStage(i);
    }
}
