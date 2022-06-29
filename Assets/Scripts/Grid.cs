using UnityEngine;
using System.IO;
using System;

public class Grid : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private GameObject slotPrefab;

    private Slot[,] GridSlotArray;
    private Slot centerSlot;
    private GridDimensions gridDimensions;
    private string jsonPath;
    private string jsonString;

    private void Start()
    {
        GetDimensionsFromJSON();
        GenerateGrid();
    }

    private void GetDimensionsFromJSON()
    {
        jsonPath = Application.dataPath + "/Resources/GridJSON.json";
        jsonString = File.ReadAllText(jsonPath);

        gridDimensions = JsonUtility.FromJson<GridDimensions>(jsonString);
    }

    private void GenerateGrid()
    {
        GridSlotArray = new Slot[gridDimensions.width, gridDimensions.height];

        for (int x = 0; x < gridDimensions.width; x++)
        {
            for (int y = 0; y < gridDimensions.height; y++)
            {
                CreateSlot(x, y);
            }
        }

        AssignNeighbors();
        SetCenterSlot();

        Camera.main.transform.position = new Vector3((float)gridDimensions.width / 2 - 0.5f, (float)gridDimensions.height / 2 - 0.5f, -10f);
        spawner.SetPositionToSlot(centerSlot);
    }

    private void AssignNeighbors()
    {
        for (int x = 0; x < gridDimensions.width; x++)
        {
            for (int y = 0; y < gridDimensions.height; y++)
            {
                if(x+1 < GridSlotArray.GetLength(0))
                    GridSlotArray[x, y].Neighbors.Add(GridSlotArray[x + 1, y]);
                if(x-1 >= 0)
                    GridSlotArray[x, y].Neighbors.Add(GridSlotArray[x - 1, y]);
                if(y+1 < GridSlotArray.GetLength(1))
                    GridSlotArray[x, y].Neighbors.Add(GridSlotArray[x, y+1]);
                if(y-1 >= 0)
                    GridSlotArray[x, y].Neighbors.Add(GridSlotArray[x, y-1]);

            }
        }
    }

    private void SetCenterSlot()
    {
        centerSlot = GridSlotArray[gridDimensions.width / 2, gridDimensions.height / 2];

        centerSlot.IsCenterSlot = true;
        centerSlot.AssignProperColor();
    }

    private void CreateSlot(int x, int y)
    {
        var slotObject = Instantiate(slotPrefab, new Vector3(x, y), Quaternion.identity);
        slotObject.transform.SetParent(transform);

        var slot = slotObject.GetComponent<Slot>();
        slot.PositionInArray = new Vector2Int(x, y);
        slot.AssignProperColor();

        GridSlotArray[x, y] = slot;
    }

    /// <returns>Returns center slot</returns>
    public Slot GetCenterSlot()
    {
        return centerSlot;
    }

    /// <returns>Returns array of slots</returns>
    public Slot[,] GetSlotsArray()
    {
        return GridSlotArray;
    }

    /// <returns>Returns array of slots</returns>
    public GridDimensions GetGridDimensions()
    {
        return gridDimensions;
    }
}
