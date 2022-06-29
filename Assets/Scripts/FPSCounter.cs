using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text display_Text;

    private int avgFrameRate;

    public void Update()
    {
        float current = 0;
        current = (int)(1f / Time.unscaledDeltaTime);
        avgFrameRate = (int)current;
        display_Text.text = $"FPS: {avgFrameRate}";
    }
}