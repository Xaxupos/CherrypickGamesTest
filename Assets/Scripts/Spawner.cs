using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject coloredItemPrefab;

    private Slot spawnerSlot;

    private int directionX = 1;
    private int directionY = 0;
    private int segment_length = 1;
    private int currentX = 0;
    private int currentY = 0;
    private int segments_passed = 0;


    private void Awake()
    {
        ResetSpawnerValues();
    }

    public IEnumerator SpawnItems()
    {  
        GridDimensions dimensions = grid.GetGridDimensions();
        Slot[,] slots = grid.GetSlotsArray();

        while (true)
        {
            for (int i = 0; i < (dimensions.width/2 * dimensions.height/2); ++i)
            {
                currentX += directionX;
                currentY += directionY;
                ++segments_passed;

                var spawnerSlotPositionXPlus = spawnerSlot.PositionInArray.x + currentX;
                var spawnerSlotPositionYPlus = spawnerSlot.PositionInArray.y + currentY;

                if (spawnerSlotPositionXPlus >= slots.GetLength(0) || spawnerSlotPositionYPlus >= slots.GetLength(1)) yield break;

                var checkingSlot = slots[spawnerSlotPositionXPlus, spawnerSlotPositionYPlus];

                if(checkingSlot != null && checkingSlot.IsEmpty && !checkingSlot.IsDenoted)
                {
                    checkingSlot.IsEmpty = false;
                    var coloredItemObject = Instantiate(coloredItemPrefab, transform.position, Quaternion.identity);
                    var coloredItem = coloredItemObject.GetComponent<ColoredItem>();
                    coloredItem.TargetPos = checkingSlot.transform.position;
                    yield return null;
                }

                if(segments_passed == segment_length)
                {
                    segments_passed = 0;

                    int temp = directionX;
                    directionX = directionY;
                    directionY = -temp;

                    if(directionX == 0)
                    {
                        ++segment_length;
                    }
                }
                yield return null;
            }
        }
    }

    public Slot GetSpawnerSlot() => spawnerSlot;

    public void SetPositionToSlot(Slot slot)
    {
        transform.position = slot.transform.position;
        spawnerSlot = slot;
    }

    public void ResetSpawnerValues()
    {
        directionX = 0;
        directionY = 1;
        segment_length = 1;
        currentX = 0;
        currentY = 0;
        segments_passed = 0;
    }
}
