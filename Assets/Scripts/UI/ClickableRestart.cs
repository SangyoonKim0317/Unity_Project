using UnityEngine;

public class ClickableRestart : MonoBehaviour   // 게임오버 구현 시 삭제하고 게임오버 패널에 연결해야함 
{
    private void OnMouseDown()
    {
        if (GameManager.Instance != null)
        {
            Debug.Log("Restarting Game");
            GameManager.Instance.RestartGame();
        }
    }
}