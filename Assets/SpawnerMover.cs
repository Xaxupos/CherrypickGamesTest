using UnityEngine;

public class SpawnerMover : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CameraMover cameraMover;

    [Header("Drop Settings")]
    [SerializeField] private float nearbySlotsDetectionRadius = 2f;
    [SerializeField] private LayerMask slotLayerMask;

    private Spawner spawner;
    private Vector3 mouseDragStartPosition;
    private Vector3 spriteDragStartPosition;
    private Collider2D[] detectedSlotsColliders;
    private Slot previousSlot;

    private void Awake()
    {
        spawner = GetComponent<Spawner>();
    }

    private void OnMouseDown()
    {
        cameraMover.IsMovementBlocked = true;
        previousSlot = spawner.GetSpawnerSlot();
        mouseDragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
    }

    private void OnMouseDrag()
    {
        transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
    }

    private void OnMouseUp()
    {
        cameraMover.IsMovementBlocked = false;
        detectedSlotsColliders = Physics2D.OverlapCircleAll(transform.position, nearbySlotsDetectionRadius, slotLayerMask);

        if(detectedSlotsColliders.Length == 0)
        {
            float newRadius = nearbySlotsDetectionRadius;
            while(detectedSlotsColliders.Length == 0)
            {
                newRadius *= 2f;
                detectedSlotsColliders = Physics2D.OverlapCircleAll(transform.position, newRadius, slotLayerMask);
            }
        }

        float closestSlotDistance = -1f;
        Slot closestSlot = null;

        foreach(var slotCollider in detectedSlotsColliders)
        {
            var slot = slotCollider.GetComponent<Slot>();

            float distanceToSlot = Vector2.Distance(transform.position, slot.transform.position);

            if(closestSlot == null || distanceToSlot < closestSlotDistance)
            {
                if(slot.IsDenoted) continue;
                if(!slot.IsEmpty) continue;

                closestSlot = slot;
                closestSlotDistance = distanceToSlot;
            }
        }
        if (closestSlot == null) closestSlot = previousSlot;
        else { previousSlot = closestSlot; }

        spawner.SetPositionToSlot(closestSlot);
        spawner.ResetSpawnerValues();
    }
}
