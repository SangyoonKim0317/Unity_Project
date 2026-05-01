using UnityEngine;
using System.Collections.Generic;

// 게임오버 조건을 체크하는 시스템
// 특정 영역 위로 원소가 올라오면 타이머 시작
public class GameOverChecker : MonoBehaviour
{
    public float sharedAreaY;   // 공유 영역 기준
    public float deadLineY;     // 데드라인

    public float gameOverDelay; // 유지 시간 (예: 2초)
    public float timer;         // 현재 타이머

    public List<Element> targetElements; // 감시 대상 원소 목록

    // 감시 대상 등록
    public void RegisterElement(Element element)
    {
        // TODO: 리스트 추가
    }

    // 게임오버 체크
    public void CheckGameOver()
    {
        // TODO: 조건 검사
    }

    // 타이머 초기화
    public void ResetTimer()
    {
        // TODO: 초기화
    }
}