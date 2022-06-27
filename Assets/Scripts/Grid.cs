using UnityEngine;
using System.IO;

public class Grid : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

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
        for (int x = 0; x < gridDimensions.width; x++)
        {
            for (int y = 0; y < gridDimensions.height; y++)
            {
                float xPosition = x - (gridDimensions.width / 2f);
                float yPosition = y - (gridDimensions.height / 2f);

                var slot = Instantiate(slotPrefab, new Vector3(xPosition,yPosition, 0f), Quaternion.identity);
                slot.transform.SetParent(transform);

                slot.GetComponent<Slot>().PositionInArray = new Vector2(x, y);
            }
        }
    }
}
