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

    private GameObject previewObject;    // 미리보기 오브젝트

    // 좌우 이동 처리
    public void Move(float direction)
    {
        float targetX;
        float clampedX;

        targetX = transform.position.x + (direction * moveSpeed * Time.deltaTime);
        clampedX = Mathf.Clamp(targetX, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // 프리뷰 오브젝트 위치도 동기화
        if (previewObject != null)
        {
            previewObject.transform.position = spawnPoint.position;
        }
    }

    // 원소 드롭
    public void Drop()
    {
        if (canDrop == false)
        {
            Debug.LogWarning("failed to drop element: cooldown in progress");
            return;
        }

        canDrop = false;

        // 프리뷰 오브젝트가 없으면 아무것도 하지 않음
        if (previewObject == null)
        {
            Debug.LogWarning("No preview object to drop");
            return;
        }

        // 실제 원소를 새로 생성 (기존 Drop 로직)
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
                if (checker != null && element != null)
                {
                    checker.RegisterElement(element);
                }
            }
        }

        // 기존 프리뷰 오브젝트 삭제 및 새 프리뷰 생성(지연)
        SetNextElement();
        if (previewObject != null)
        {
            Destroy(previewObject);
        }
        Invoke(nameof(CreatePreviewObject), 0.5f);

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
        // (UI 표시 필요시 여기에 추가)
    }

    // 유니티 생명주기: 시작 시 프리뷰 오브젝트 생성
    void Start()
    {
        SetNextElement();
        CreatePreviewObject();
    }

    // 프리뷰 오브젝트 생성 및 위치/상태 초기화
    void CreatePreviewObject()
    {
        // 기존 프리뷰 오브젝트가 있으면 삭제
        if (previewObject != null)
        {
            Destroy(previewObject);
        }

        MergeManager mergeManager = FindFirstObjectByType<MergeManager>();
        if (mergeManager != null)
        {
            ElementData data = mergeManager.GetElementData(spawnType, nextLevel);
            if (data != null && data.prefab != null)
            {
                previewObject = Instantiate(data.prefab, spawnPoint.position, Quaternion.identity);
                // 프리뷰 오브젝트는 중력/물리 비활성화
                Rigidbody2D rb = previewObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.simulated = false;
                }
                // Element 정보 초기화
                Element element = previewObject.GetComponent<Element>();
                if (element != null)
                {
                    element.Init(spawnType, nextLevel);
                }
            }
        }
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