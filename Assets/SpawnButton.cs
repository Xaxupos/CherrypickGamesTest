using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SpawnButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isSpawnHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isSpawnHeld = true;
        StartCoroutine(SpawnItems());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSpawnHeld = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnItems()
    {
        while(isSpawnHeld)
        {
            Debug.Log($"Spawning");

            yield return null;
        }
    }
}
