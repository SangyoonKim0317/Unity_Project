using System.Security.Cryptography;
using UnityEngine;

// 플레이어가 조작하는 원소 생성기
// 좌우 이동 및 드롭 기능 담당
public class Spawner : MonoBehaviour
{
    public ElementType spawnType;   // 생성할 원소 타입 (물/불)
    public Transform spawnPoint;    // 생성 위치

    public float moveSpeed;         // 이동 속도
    public float minX;              // 이동 최소 범위
    public float maxX;              // 이동 최대 범위

    public float dropCooldown = 1;      // 드롭 쿨타임
    public bool canDrop = true;            // 드롭 가능 여부

    public int nextLevel;           // 다음 생성될 원소 레벨

    // 좌우 이동 처리
    public void Move(float direction)
    {
        float targetX;
        float clampedX;

        targetX = transform.position.x + (direction * moveSpeed * Time.deltaTime);
        
        clampedX = Mathf.Clamp(targetX, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    // 원소 드롭
    public void Drop()
    {
        if (canDrop == false) // 드롭 불가능 상태 시 드롭 스킵
        {
            Debug.LogWarning("failed to drop element: cooldown in progress");
            return;
        }

        canDrop = false; // 앞의 if문 통과로 우선 드롭 불가능 상태로 만든 뒤 드롭 수행

        MergeManager mergeManager = FindFirstObjectByType<MergeManager>();

        if (mergeManager != null)
        {
            ElementData data = mergeManager.GetElementData(spawnType, nextLevel);

            if (data != null && data.prefab != null)
            {
                GameObject spawnedObj = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);
                
                Element element = spawnedObj.GetComponent<Element>();
                
                if (element != null)
                {
                    element.Init(spawnType, nextLevel);
                }

                GameOverChecker checker = FindFirstObjectByType<GameOverChecker>();

                if (checker != null)
                {
                    checker.RegisterElement(element);
                }
            }
            else 
            {
                Debug.LogWarning("failed to get ElementData for type: " + spawnType + ", level: " + nextLevel);
            }
        }
        else
        {
            Debug.LogWarning("failed to find MergeManager");
        }

        SetNextElement();

        Invoke(nameof(ResetDrop), dropCooldown);
    }

    // 드롭 가능 여부 참으로 변경
    void ResetDrop()
    {
        canDrop = true;
    }

    // 다음 원소 설정
    public void SetNextElement()
    {
        nextLevel = GetRandomLevel();

        // 다음 원소를 UI에 보여주는 코드. UIManager 최종 구현 완료 후 주석 지우고 사용
        /*
        if (GameManager.Instance != null && GameManager.Instance.uiManager != null)
        {
            GameManager.Instance.uiManager.UpdateNextElement(spawnType, nextLevel);
        }
        */
    }

    // 랜덤 레벨 반환
    public int GetRandomLevel()
    {
        // GameManager 정상 작동 시 GameManager 이용해서 원소 추첨
        if (GameManager.Instance != null)
        {
            int min = GameManager.Instance.waterSpawnMinLevel;
            int max = GameManager.Instance.waterSpawnMaxLevel;

            return Random.Range(min, max + 1);
        }

        // GameManager 작동 안 할 시 기본적으로 0을 반환
        return 0;
    }
}