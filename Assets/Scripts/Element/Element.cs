using UnityEngine;

public class Element : MonoBehaviour
{
    public ElementType elementType; // 원소 타입
    public int level;               // 원소 레벨

    public bool isGameOverTarget;   // 게임오버 감시 대상 여부

    public Rigidbody2D rb;          // 물리 이동 담당
    public CircleCollider2D col;    // 원형 충돌 판정 담당

    public MergeManager mergeManager; // 충돌 후 머지 처리를 담당하는 매니저

    private bool isProcessingCollision = false; // 충돌 중복 처리 방지용

    private void Awake()
    {
        Debug.LogWarning("Element Awake 실행됨: " + gameObject.name);
        // Rigidbody2D 자동 연결
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // CircleCollider2D 자동 연결
        if (col == null)
        {
            col = GetComponent<CircleCollider2D>();
        }

        // Scene에 있는 MergeManager 자동 연결
        if (mergeManager == null)
        {
            mergeManager = FindFirstObjectByType<MergeManager>();
        }
    }

    public void Init(ElementType type, int level)
    {
        // 전달받은 타입과 레벨 저장
        this.elementType = type;
        this.level = level;

        // 레벨에 따라 크기 증가
        // level 0 → 1.0배, level 1 → 1.2배, level 2 → 1.4배
        float size = 1f + (level * 0.2f);
        transform.localScale = new Vector3(size, size, 1f);

        // 레벨에 따라 질량 설정
        if (rb != null)
        {
            rb.mass = 1f + (level * 0.5f);
            rb.gravityScale = 1f;
        }

        // 기본 충돌 반지름 설정
        if (col != null)
        {
            col.radius = 0.5f;
        }

        // 처음 생성된 원소는 게임오버 감시 대상이 아님
        isGameOverTarget = false;

        // 새로 생성된 원소는 충돌 처리 상태 초기화
        isProcessingCollision = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning("충돌 함수 실행됨: " + gameObject.name + " / " + collision.gameObject.name);
        
        // 충돌한 상대 오브젝트에서 Element 컴포넌트 가져오기
        Element other = collision.gameObject.GetComponent<Element>();

        // 상대가 원소가 아니면 무시
        if (other == null)
        {
            return;
        }

        // 이미 처리 중인 충돌이면 중복 실행 방지
        if (isProcessingCollision || other.isProcessingCollision)
        {
            return;
        }

        // 양쪽 원소에서 같은 충돌이 두 번 실행되는 것을 방지
        if (GetInstanceID() > other.GetInstanceID())
        {
            return;
        }

        // MergeManager가 있으면 머지/파괴 규칙 검사
        if (mergeManager != null)
        {
            isProcessingCollision = true;
            other.isProcessingCollision = true;

            mergeManager.CheckMerge(this, other);
        }
        else
        {
            Debug.LogWarning("MergeManager를 찾을 수 없습니다.");
        }
    }
}