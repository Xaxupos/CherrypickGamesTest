using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void SetToCenterSlot(Grid grid)
    {
        Debug.Log(grid.GetCenterSlot().transform.position);
        transform.position = grid.GetCenterSlot().transform.position;
    }
}
