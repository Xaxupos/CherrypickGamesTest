using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Slot : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color whitenedColor;
    [SerializeField] private Color greyedColor;
    [SerializeField] private Color denotedColor;

    public ColoredItem AssignedColor;
    public List<Slot> Neighbors = new List<Slot>();
    public Vector2Int PositionInArray { get; set; }
    public bool IsDenoted { get; set; } = false;
    public bool IsEmpty { get; set; } = true;
    public bool IsCenterSlot { get; set; } = false;

    private SpriteRenderer slotRenderer;

    private void Awake()
    {
        slotRenderer = GetComponent<SpriteRenderer>();           
    }

    public void AssignProperColor()
    {
        if (Random.value > 0.75f && !IsCenterSlot)
        {
            IsDenoted = true;
            slotRenderer.color = denotedColor;
            return;
        }

        if((PositionInArray.x % 2 == 0 &&PositionInArray. y % 2 != 0) || (PositionInArray.x % 2 != 0 && PositionInArray.y % 2 == 0))
        {
            slotRenderer.color = greyedColor;
        }
        else
        {
            slotRenderer.color = whitenedColor;
        }
    }
}
