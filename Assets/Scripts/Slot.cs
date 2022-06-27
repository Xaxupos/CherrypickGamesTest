using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Slot : MonoBehaviour
{
    public Vector2 PositionInArray;

    private SpriteRenderer slotRenderer;
    private bool isDenoted = false;

    [SerializeField] private Color whitenedColor;
    [SerializeField] private Color greyedColor;
    [SerializeField] private Color denotedColor;

    private void Awake()
    {
        slotRenderer = GetComponent<SpriteRenderer>();
    }

    public void AssignProperColor()
    {
        if(Random.value > 0.75f)
        {
            isDenoted = true;
            slotRenderer.color = denotedColor;
            return;
        } 

        if(PositionInArray.x % 2 == 0)
        {
            if(PositionInArray.y % 2 == 0)
            {
                slotRenderer.color = greyedColor;
            }
            else
            {
                slotRenderer.color = whitenedColor;
            }
        }
        else
        {
            if (PositionInArray.y % 2 == 0)
            {
                slotRenderer.color = whitenedColor;
            }
            else
            {
                slotRenderer.color = greyedColor;
            }
        }
    }
}
