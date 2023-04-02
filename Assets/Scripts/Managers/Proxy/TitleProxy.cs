using UnityEngine;

public class TitleProxy : Proxy
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartStage(int i)
    {
        GameManager.Instance.StartStage(i);
    }
}
