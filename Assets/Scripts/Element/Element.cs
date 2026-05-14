using UnityEngine;

// 게임 내 실제 떨어지는 원소를 담당하는 클래스
// 원소 타입, 레벨, 물리, 충돌 정보를 관리한다.
public class Element : MonoBehaviour
{
    public ElementType elementType; // 원소 타입: Water, Fire 등
    public int level;               // 원소 레벨

    public bool isGameOverTarget;   // 게임오버 감시 대상인지 여부

    public Rigidbody2D rb;          // 2D 물리 처리를 담당하는 Rigidbody
    public CircleCollider2D col;    // 원형 충돌 판정을 담당하는 Collider

    private MergeManager mergeManager; // 충돌 시 머지 처리를 넘길 MergeManager

    // 오브젝트가 생성될 때 한 번 실행
    private void Awake()
    {
        // Inspector에서 rb를 직접 넣지 않아도 자동으로 가져오게 함
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Inspector에서 col을 직접 넣지 않아도 자동으로 가져오게 함
        if (col == null)
        {
            col = GetComponent<CircleCollider2D>();
        }

        // 씬에 있는 MergeManager를 한 번만 찾아서 저장
        // 충돌할 때마다 찾으면 비효율적이므로 Awake에서 미리 찾아둠
        mergeManager = FindObjectOfType<MergeManager>();
    }

    // 원소 초기화 함수
    // Spawner 쪽에서 새 원소를 만들 때 호출하면 됨
    public void Init(ElementType type, int level)
    {
        // 전달받은 타입과 레벨을 현재 원소에 저장
        this.elementType = type;
        this.level = level;

        // 레벨에 따라 크기 증가
        // level 0 → 1.0배
        // level 1 → 1.2배
        // level 2 → 1.4배
        float size = 1f + (level * 0.2f);
        transform.localScale = new Vector3(size, size, 1f);

        // Rigidbody2D가 있으면 레벨에 따라 질량 설정
        if (rb != null)
        {
            // 레벨이 높을수록 더 무겁게 설정
            rb.mass = 1f + (level * 0.5f);

            // 중력 적용 정도
            rb.gravityScale = 1f;
        }

        // CircleCollider2D가 있으면 기본 반지름 설정
        if (col != null)
        {
            col.radius = 0.5f;
        }

        // 처음 생성된 원소는 게임오버 감시 대상이 아님
        isGameOverTarget = false;
    }

    // 다른 오브젝트와 충돌했을 때 Unity가 자동으로 호출하는 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 상대 오브젝트에서 Element 컴포넌트를 가져옴
        Element other = collision.gameObject.GetComponent<Element>();

        // 상대가 Element가 아니면 바닥, 벽 같은 것이므로 무시
        if (other == null)
        {
            return;
        }
        Debug.Log("원소 충돌 발생: " + elementType + " / " + elementType);
    }

        // 같은 두 원소가 충돌하면
        // A에서도 OnCollisionEnter2D가 실행되고
        // B에서도 OnCollisionEnter2D가 실행될 수 있음
        // 그래서 한 쌍의 충돌을 한 번만 처리하기 위한 조건
        if (this.GetInstanceID() > other.GetInstanceID())
        {
            return;
        }

        // MergeManager가 씬에 없으면 머지 처리를 할 수 없으므로 무시
        if (mergeManager == null)
        {
            return;
        }

        // MergeManager에게 두 원소의 충돌 정보를 전달
        // 실제 머지 가능 여부 판단, 파괴 규칙 처리는 MergeManager가 담당
        mergeManager.CheckMerge(this, other);
    }

    // 게임오버 감시 대상 등록
    public void MarkAsGameOverTarget()
    {
        isGameOverTarget = true;
    }

    // 같은 타입 + 같은 레벨인지 확인하는 함수
    public bool IsSameElement(Element other)
    {
        // 비교 대상이 없으면 같은 원소가 아님
        if (other == null)
        {
            return false;
        }

        // 타입도 같고 레벨도 같을 때만 true
        return elementType == other.elementType && level == other.level;
    }
}
