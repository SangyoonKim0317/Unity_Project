using UnityEngine;

// 게임 내 실제 떨어지는 원소(물/불)를 담당하는 클래스
// 물리, 충돌, 레벨 정보를 관리
public class Element : MonoBehaviour
{
    public ElementType elementType; // 원소 타입 (물/불)
    public int level;               // 원소 레벨

    public bool isGameOverTarget;   // 게임오버 감시 대상 여부

    public Rigidbody2D rb;          // 물리 처리용 Rigidbody
    public CircleCollider2D col;    // 충돌 판정용 Collider

    // 원소 초기화 (타입과 레벨 설정)
    public void Init(ElementType type, int level)
    {
        // TODO: 데이터 세팅
    }

    // 충돌 시 호출되는 함수 (머지 판정 트리거)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: MergeManager에 충돌 전달
    }

    // 게임오버 감시 대상 등록
    public void MarkAsGameOverTarget()
    {
        // TODO: 감시 대상 처리
    }

    // 같은 타입 + 같은 레벨인지 확인
    public bool IsSameElement(Element other)
    {
        return false;
    }
}