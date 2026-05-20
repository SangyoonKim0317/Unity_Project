using UnityEngine;
using System.Collections.Generic;

// 원소 간 충돌 시 머지 및 파괴 규칙을 처리하는 클래스
public class MergeManager : MonoBehaviour
{
    public List<ElementData> elementDataList;

    public bool CheckMerge(Element a, Element b)
{
    // 같은 타입 + 같은 레벨 → 머지
    if (a.elementType == b.elementType && a.level == b.level)
    {
        if (a.level >= 3) return false; // 레벨 3 이상은 머지 안되도록 - 레벨 0부터 시작해서 최고레벨 3으로 설정함.
        Merge(a, b);
        return true; 
    }

    // 불 vs 물 파괴 로직
    if (a.elementType != b.elementType)
    {
        Element water = (a.elementType == ElementType.Water) ? a : b;
        Element fire = (a.elementType == ElementType.Fire) ? a : b;

        if (fire.level >= water.level)
        {
            DestroyByRule(water, fire);
            return true; 
        }
    }

    return false; 
}

/* 
    // 머지 가능 여부 확인 및 처리
    public void CheckMerge(Element a, Element b)
    {
        // TODO: 머지 조건 확인

        // 같은 타입 + 같은 레벨 → 머지
        if (a.elementType == b.elementType && a.level == b.level)
        {
            Merge(a, b);
            return;
        }

        // 불 vs 물 파괴 규칙: 불 >= 물 레벨일 때 둘 다 파괴
        if (a.elementType != b.elementType)
        {
            Element water = (a.elementType == ElementType.Water) ? a : b;
            Element fire = (a.elementType == ElementType.Fire) ? a : b;

            if (fire.level >= water.level)
            {
                DestroyByRule(water, fire);
            }
        }
        
    }
*/
    
    // 같은 레벨 + 같은 타입 머지
    public void Merge(Element a, Element b)
    {
        // TODO: 레벨 증가 원소 생성

        ElementType mergedType = a.elementType;
        int nextLevel = a.level + 1;
        Vector2 mergePosition = (a.transform.position + b.transform.position) / 2f;

        // 기존 두 원소 파괴
        Destroy(a.gameObject);
        Destroy(b.gameObject);

        // 다음 레벨 원소가 존재하면 생성
        ElementData nextData = GetElementData(mergedType, nextLevel);
        if (nextData != null)
        {
            SpawnNextElement(mergedType, nextLevel, mergePosition);
        }
    }

    // 불 >= 물 레벨일 경우 파괴
    public void DestroyByRule(Element water, Element fire)
    {
        // TODO: 파괴 및 점수 처리

        // 파괴 점수 계산 (두 원소의 점수 합산)
        int destroyScore = GetScoreForElement(water.elementType, water.level) + GetScoreForElement(fire.elementType, fire.level);

        // 점수 추가
        ScoreManager scoreManager = GameManager.Instance.scoreManager;
        if (scoreManager != null)
        {
            scoreManager.AddScore(destroyScore);
        }

        // 두 원소 모두 파괴
        Destroy(water.gameObject);
        Destroy(fire.gameObject);
    }

    // 다음 레벨 원소 생성
    public void SpawnNextElement(ElementType type, int nextLevel, Vector2 position)
    {
        // TODO: 프리팹 생성

        ElementData data = GetElementData(type, nextLevel);
        if (data == null || data.prefab == null) return;

        // 프리팹 인스턴스 생성
        GameObject newObj = Instantiate(data.prefab, position, Quaternion.identity);

        // Element 컴포넌트 초기화
        Element newElement = newObj.GetComponent<Element>();
        if (newElement != null)
        {
            newElement.Init(type, nextLevel);
        }

        // 스프라이트 설정
        SpriteRenderer sr = newObj.GetComponent<SpriteRenderer>();
        if (sr != null && data.sprite != null)
        {
            sr.sprite = data.sprite;
        }

        // 스케일 설정
        // newObj.transform.localScale = Vector3.one * data.scale;

        // 질량 설정
        Rigidbody2D rb = newObj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.mass = data.mass;
        }
    }

    // elementDataList에서 타입과 레벨에 맞는 ElementData를 찾는 헬퍼 메서드
    public ElementData GetElementData(ElementType type, int level)
    {
        if (elementDataList == null) return null;

        for (int i = 0; i < elementDataList.Count; i++)
        {
            if (elementDataList[i].elementType == type && elementDataList[i].level == level)
            {
                return elementDataList[i];
            }
        }
        return null;
    }

    // 특정 원소의 점수를 가져오는 헬퍼 메서드
    private int GetScoreForElement(ElementType type, int level)
    {
        ElementData data = GetElementData(type, level);
        if (data != null)
        {
            return data.score;
        }
        return 0;
    }
}
