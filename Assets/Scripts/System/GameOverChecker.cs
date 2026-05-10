using UnityEngine;
using System.Collections.Generic;

// 게임오버 조건을 체크하는 시스템
// 특정 영역 위로 원소가 올라오면 타이머 시작
public class GameOverChecker : MonoBehaviour
{
    public float sharedAreaY;   // 공유 영역 기준
    public float deadLineY;     // 데드라인

    public List<Element> targetElements = new List<Element>(); // 감시 대상 원소 목록 : 스포너에서 원소를 만들고 나서 등록필요

    public void Update()
    {
        if (!GameManager.Instance.IsPlaying())
            return;

        CheckGameOver();
    }

    // 감시 대상 등록
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
        for (int i = targetElements.Count - 1; i >= 0; i--)
        {
            if (targetElements[i] == null)
            {
                targetElements.RemoveAt(i);
                continue;
            }

            if (targetElements[i].transform.position.y >= deadLineY)
            {
                Debug.Log("데드라인 넘음");
                GameManager.Instance.GameOver();
                return;
            }
        }
    }

    // 타이머 초기화
    public void ResetTimer()
    {
        // 즉시 게임 오버 방식이라 현재는 사용 X
    }
}