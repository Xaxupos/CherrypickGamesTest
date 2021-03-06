using System.Collections.Generic;
using UnityEngine;

public class ClearManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Grid grid;
    [SerializeField] private Spawner spawner;
    
    private List<ColoredItem> ItemsToDelete = new List<ColoredItem>();

    public void ClearButton()
    {
        foreach (var slot in grid.GetSlotsArray())
        {
            if (slot.AssignedColor == null || slot.IsEmpty) continue;

            foreach (var neighbor in slot.Neighbors)
            {
                if (neighbor.AssignedColor == null || slot.IsEmpty) continue;

                if (slot.AssignedColor.ColorIndex == neighbor.AssignedColor.ColorIndex)
                {
                    if (!ItemsToDelete.Contains(neighbor.AssignedColor))
                        ItemsToDelete.Add(neighbor.AssignedColor);
                }
            }
        }

        DeleteNeighbors();

        ItemsToDelete.Clear();
        spawner.ResetSpawnerValues();
    }

    private void DeleteNeighbors()
    {
        foreach (var item in ItemsToDelete)
        {
            item.AssignedSlot.IsEmpty = true;
            Destroy(item.gameObject);
        }
    }
}
