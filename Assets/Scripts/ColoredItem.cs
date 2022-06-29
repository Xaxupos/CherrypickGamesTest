using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ColoredItem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float travelTime = 1f;
    [SerializeField] private Color[] colors;

    public Slot AssignedSlot { get; set; }
    public int ColorIndex { get; set; }
    public Vector3 TargetPos { get; set; }

    private Vector3 startPos;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        ColorIndex = Random.Range(0, colors.Length);
        spriteRenderer.color = colors[ColorIndex];
    }

    private void Start()
    {
        StartCoroutine(Move(startPos, TargetPos, travelTime));
    }

    private IEnumerator Move(Vector3 beginPos, Vector3 endPos, float time)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(beginPos, endPos, t);
            yield return null;
        }
    }
}
