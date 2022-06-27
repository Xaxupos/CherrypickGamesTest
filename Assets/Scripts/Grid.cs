
using UnityEngine;
using System.IO;

public class Grid : MonoBehaviour
{
    public GridDimensions gridDimensions;

    private string jsonPath;
    private string jsonString;

    private void Start()
    {
        GetDimensionsFromJSON();
    }

    private void GetDimensionsFromJSON()
    {
        jsonPath = Application.dataPath + "/Resources/GridJSON.json";
        jsonString = File.ReadAllText(jsonPath);

        gridDimensions = JsonUtility.FromJson<GridDimensions>(jsonString);
    }
}
