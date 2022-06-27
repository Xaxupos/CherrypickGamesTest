using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("References")]
    [SerializeField] private Spawner spawner;

    private Coroutine spawningCoroutine;

    public void OnPointerDown(PointerEventData eventData)
    {
        spawningCoroutine = StartCoroutine(spawner.SpawnItems());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopCoroutine(spawningCoroutine);
    }
}
