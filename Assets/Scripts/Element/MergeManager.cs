using UnityEngine;
using System.Collections.Generic;

// 원소 간 충돌 시 머지 및 파괴 규칙을 처리하는 클래스
public class MergeManager : MonoBehaviour
{
    public List<ElementData> elementDataList;

    // 머지 가능 여부 확인 및 처리
    public void CheckMerge(Element a, Element b)
    {
        // TODO: 머지 조건 확인
    }

    // 같은 레벨 + 같은 타입 머지
    public void Merge(Element a, Element b)
    {
        // TODO: 레벨 증가 원소 생성
    }

    // 불 >= 물 레벨일 경우 파괴
    public void DestroyByRule(Element water, Element fire)
    {
        // TODO: 파괴 및 점수 처리
    }

    // 다음 레벨 원소 생성
    public void SpawnNextElement(ElementType type, int nextLevel, Vector2 position)
    {
        // TODO: 프리팹 생성
    }
}