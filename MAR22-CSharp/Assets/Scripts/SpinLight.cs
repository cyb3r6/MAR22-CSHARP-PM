using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Managing the animation of one spin light
/// </summary>
public class SpinLight : MonoBehaviour
{
    public Color spinColor;
    public float scaleAnimationSpeed;
    public float scaleAmount;

    private Vector3 currentScale;
    private RectTransform spinLightTransform;
    private float spinSpeed;

    void Start()
    {
        // get rect transform
        spinLightTransform = (RectTransform)this.transform;

        // get the current scale
        currentScale = spinLightTransform.localScale;

        // set a random speed for the light rotation
        spinSpeed = Random.Range(0, 180);

        // set a random rotation on start
        spinLightTransform.Rotate(0, 0, Random.Range(0, 360));
    }

    
    void Update()
    {
        AnimateSpinLightRotation();
        AnimateSpinLightScale();
    }
    public void AnimateSpinLightRotation()
    {
        spinLightTransform.Rotate(0, 0, spinSpeed * Time.deltaTime);
        spinLightTransform.GetComponent<Image>().color = spinColor;
    }

    public void AnimateSpinLightScale()
    {
        Vector3 newScale = spinLightTransform.localScale;
        float xy = currentScale.z + Mathf.Cos(scaleAnimationSpeed * Time.time) * scaleAmount;
        newScale = new Vector3(xy, xy, 1);
        spinLightTransform.localScale = newScale;
    }
}
