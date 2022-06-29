using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomSlider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Grid grid;

    private AnimationCurve zoomAnimationCurve;
    private Slider zoomSlider;

    private void Awake()
    {
        zoomSlider = GetComponent<Slider>();

       
    }

    private void Start()
    {
        zoomAnimationCurve = new AnimationCurve(new Keyframe(1, 5f), new Keyframe(0, grid.GetGridDimensions().width / 2f + 1f));
    }

    private void Update()
    {
        //Camera is cached in new unity version so there is no 
        //Perfomarnce issue when using Camera.main
        if(zoomAnimationCurve != null)
        {
            Camera.main.orthographicSize = zoomAnimationCurve.Evaluate(zoomSlider.value);
            if(Camera.main.orthographicSize < 5f) Camera.main.orthographicSize = 5f;
        }
    }
}
