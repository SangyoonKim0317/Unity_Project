using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameOverChecker : MonoBehaviour
{
    public float deadLineY;       
    public float gameOverDelay = 2f; 
    private float currentTimer = 0f; 

    [Header("--- 2D 오브젝트 연결 ---")]
    public GameObject gameOverWindow;   // 게임오버 부모 스프라이트 오브젝트
    public TextMeshPro finalScoreText;  
    public ScoreManager scoreManager;   

    private bool isGameOverTriggered = false; 
    public List<Element> targetElements = new List<Element>(); 

    public void Update()
    {
        if (isGameOverTriggered) return;
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying()) return;

        CheckGameOver();
    }

    public void RegisterElement(Element element)
    {
        if (element != null && !targetElements.Contains(element)) targetElements.Add(element);
    }

    private void CheckGameOver()
    {
        bool isOverLimit = false;
        for (int i = targetElements.Count - 1; i >= 0; i--)
        {
            if (targetElements[i] == null) { targetElements.RemoveAt(i); continue; }
            if (targetElements[i].transform.position.y >= deadLineY) { isOverLimit = true; break; }
        }

        if (isOverLimit)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= gameOverDelay)
            {
                isGameOverTriggered = true; 
                if (GameManager.Instance != null) GameManager.Instance.GameOver(); 
                ShowGameOverWindow(); 
            }
        }
        else { currentTimer = 0f; }
    }

    private void ShowGameOverWindow()
    {
        if (gameOverWindow != null)
        {
            gameOverWindow.SetActive(true);
            
            if (finalScoreText != null && scoreManager != null)
            {
                finalScoreText.text = "Final score\n" + scoreManager.GetScore().ToString();
            }
        }
    }
}