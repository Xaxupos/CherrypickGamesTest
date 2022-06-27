using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject coloredItemPrefab;

    private Slot spawnerSlot;

    private bool SpawnedAtSlot(Slot slot)
    {
        if (slot != null && slot.IsEmpty && !slot.IsDenoted)
        {
            slot.IsEmpty = false;
            Instantiate(coloredItemPrefab, slot.transform.position, Quaternion.identity);
            return true;
        }
        return false;
    }

    public IEnumerator SpawnItems()
    {  
        GridDimensions dimensions = grid.GetGridDimensions();
        Slot[,] slots = grid.GetSlotsArray();

        //Direction in which we move right now
        int dx = 1;
        int dy = 0;
        int segment_length = 1;

        int x = 0;
        int y = 0;
        int segments_passed = 0;

        while (true)
        {
            for (int i = 0; i < (dimensions.width/2 * dimensions.height/2); ++i)
            {
                x += dx;
                y += dy;
                ++segments_passed;

                var spawnerSlotPositionXPlus = spawnerSlot.PositionInArray.x + x;
                var spawnerSlotPositionYPlus = spawnerSlot.PositionInArray.y + y;

                if (spawnerSlotPositionXPlus >= slots.GetLength(0) || spawnerSlotPositionYPlus >= slots.GetLength(1)) yield break;

                var checkingSlot = slots[spawnerSlotPositionXPlus, spawnerSlotPositionYPlus];

                if(checkingSlot != null && checkingSlot.IsEmpty && !checkingSlot.IsDenoted)
                {
                    checkingSlot.IsEmpty = false;
                    Instantiate(coloredItemPrefab, checkingSlot.transform.position, Quaternion.identity);
                    yield return null;
                }

                if(segments_passed == segment_length)
                {
                    segments_passed = 0;

                    int temp = dx;
                    dx = dy;
                    dy = -temp;

                    //Increase segment length if necessary
                    if(dy == 0)
                    {
                        ++segment_length;
                    }
                }
                yield return null;
            }
        }
    }

    /// <summary>
    /// Sets the Spawner to the grid's center slot
    /// </summary>
    /// <param name="grid">Grid</param>
    public void SetPositionToCenterSlot(Grid grid)
    {
        transform.position = grid.GetCenterSlot().transform.position;
        spawnerSlot = grid.GetCenterSlot();
    }
}
