using UnityEngine;
using System.Collections.Generic;

// 게임오버 조건을 체크하는 시스템
// 특정 영역 위로 원소가 올라오면 타이머 시작
public class GameOverChecker : MonoBehaviour
{
    public float sharedAreaY;   // 공유 영역 기준
    public float deadLineY;     // 데드라인 기준 Y 좌표
    
    [Header("Game Over Settings")]
    public float gameOverDelay = 2f; 
    private float currentTimer = 0f; // 현재 초과된 시간 측정용

    public List<Element> targetElements = new List<Element>(); 

    public void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.IsPlaying())
            return;

        CheckGameOver();
    }

    public void RegisterElement(Element element)
    {
        if (element == null)
            return;

        if (!targetElements.Contains(element))
        {
            targetElements.Add(element);
        }
    }

    // 게임오버 체크
    public void CheckGameOver()
    {
        bool isOverLimit = false;

        // 1. 등록된 모든 원소들을 검사해서 하나라도 선을 넘었는지 확인
        for (int i = targetElements.Count - 1; i >= 0; i--)
        {
            // 중간에 원소가 머지됐다면 리스트에서 제거
            if (targetElements[i] == null)
            {
                targetElements.RemoveAt(i);
                continue;
            }

            // 원소의 Y 좌표가 데드라인을 넘었다면
            if (targetElements[i].transform.position.y >= deadLineY)
            {
                isOverLimit = true;
                break;
            }
        }

        // 2. 선을 넘엇을 때 타이머 작동
        if (isOverLimit)
        {
            currentTimer += Time.deltaTime;
            
            // 3. 2초 넘기면 게임오버
            if (currentTimer >= gameOverDelay)
            {
                Debug.LogWarning("Game Over");
                GameManager.Instance.GameOver();
            }
        }
        else
        {
            // 4. 선 아래로 떨어졌다면 타이머 초기화
            ResetTimer();
        }
    }

    // 타이머 초기화
    public void ResetTimer()
    {
        currentTimer = 0f;
    }
}