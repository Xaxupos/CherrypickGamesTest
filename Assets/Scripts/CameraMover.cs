using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMover : MonoBehaviour
{
    public bool IsMovementBlocked { get; set; } = false;

    private Vector3 origin;

    void LateUpdate()
    {
        if(IsMovementBlocked || EventSystem.current.IsPointerOverGameObject()) return;

        if(Input.GetMouseButtonDown(0))
        {
            origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 difference = origin - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
        }
    }
}
