using UnityEngine;

public class ClickableCursor : MonoBehaviour
{
    private void OnMouseEnter()
    {
        // ---: 전역 싱글톤 인스턴스를 통해 커서 이미지를 교체합니다. ---
        if (GlobalCursor.instance != null)
        {
            GlobalCursor.instance.SetClickCursor();
        }
    }

    private void OnMouseExit()
    {
        // ---: 다시 기본 커서로 돌립니다. ---
        if (GlobalCursor.instance != null)
        {
            GlobalCursor.instance.SetDefaultCursor();
        }
    }
}