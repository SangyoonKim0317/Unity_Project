using UnityEngine;
using UnityEngine.UI;
using TMPro;

// UI 표시를 담당하는 클래스
// 점수, 다음 원소, 게임오버 화면 관리
public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;   // 게임오버 텍스트

    [Header("Text Settings")]
    public TextMeshProUGUI scoreText;       
    public TextMeshProUGUI finalScoreText;
    
    public Image player1NextImage; // 1P 다음 원소
    public Image player2NextImage; // 2P 다음 원소

    // 점수 갱신
    public void UpdateScore(int currentScore)
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE : " + currentScore.ToString();
        }
    }

    // 다음 원소 표시
    public void UpdateNextElement(ElementType type, int level)
    {
        // TODO: 이미지 변경
    }

    // 게임오버 UI 표시
    public void ShowGameOverPanel(int finalScore)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "GAME OVER\nFinal Score : " + finalScore;
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }

    // 게임오버 창 Restart 버튼에 연결할 함수
    public void OnClickRestart()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame(); 
        }
    }
}
