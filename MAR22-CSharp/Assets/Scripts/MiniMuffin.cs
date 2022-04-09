using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMuffin : MonoBehaviour
{
    public float lifeTime = 1.5f;
    public float maxRotationVelocity = 160;
    public float maxhorizontalVelocity = 100;
    public float maxinitialVerticalVelocity = 40;
    public float gravity = 400;

    private float age;
    private RectTransform rectTransform;
    private Vector2 currentVelocity;
    private Vector3 rotationalVelocity;

    public void SetUpVelocities(Vector2 randomPosition)
    {
        // get the transform
        rectTransform = GetComponent<RectTransform>();

        // set the transform to the random position 
        rectTransform.localPosition = randomPosition;

        // create a random rotation
        rectTransform.rotation = Quaternion.Euler(new Vector3 (0, 0, Random.Range(0, 360)));

        // create a random horizontal and vertical velocity
        rotationalVelocity = new Vector3(0, 0, Random.Range(-maxRotationVelocity, maxRotationVelocity));

        currentVelocity = new Vector2(Random.Range(-maxhorizontalVelocity, maxhorizontalVelocity), Random.Range(0, maxinitialVerticalVelocity));
    }

    
    void Update()
    {
        age += Time.deltaTime;

        if(age >= lifeTime)
        {
            Destroy(this.gameObject);
            return;
        }

        UpdateTransform();
    }

    private void UpdateTransform()
    {
        rectTransform.anchoredPosition += currentVelocity * Time.deltaTime;
        rectTransform.Rotate(rotationalVelocity * Time.deltaTime);

        currentVelocity.y -= gravity * Time.deltaTime;
    }
}
