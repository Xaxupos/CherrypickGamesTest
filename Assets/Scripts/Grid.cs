using UnityEngine;
using System;
using System.IO;

public class Grid : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private GameObject slotPrefab;

    public Action OnGridGenerated;

    private Slot[,] GridArray;
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
        GridArray = new Slot[gridDimensions.width, gridDimensions.height];

        for (int x = 0; x < gridDimensions.width; x++)
        {
            for (int y = 0; y < gridDimensions.height; y++)
            {
                var slotObject = Instantiate(slotPrefab, new Vector3(x,y), Quaternion.identity);
                slotObject.transform.SetParent(transform);

                var slot = slotObject.GetComponent<Slot>();
                slot.PositionInArray = new Vector2(x, y);
                slot.AssignProperColor();

                GridArray[x, y] = slot;
            }
        }

        centerSlot = GridArray[gridDimensions.width / 2, gridDimensions.height / 2];

        centerSlot.IsCenterSlot = true;
        centerSlot.AssignProperColor();

        Camera.main.transform.position = new Vector3((float)gridDimensions.width / 2 - 0.5f, (float)gridDimensions.height / 2 - 0.5f, -10f);

        spawner.SetToCenterSlot(this);
    }

    public Slot GetCenterSlot()
    {
        return centerSlot;
    }
}
