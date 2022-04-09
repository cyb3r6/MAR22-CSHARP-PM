using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Handles the muffin reward text animations: speed, direction, fade
/// </summary>
public class FloatingText : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;
    public float fadeDuration = 5f;
    public float age = 0;
    private TMP_Text text;
    private Color initialColor;
    private float speed;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Start()
    {
        text = GetComponent<TMP_Text>();
        initialColor = text.color;
    }
    
    void Update()
    {
        // move the text upwards
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        // fade text over time
        age += Time.deltaTime;

        text.color = Color.Lerp(initialColor, Color.clear, age / fadeDuration);
        if (age > fadeDuration)
        {
            Destroy(this.gameObject);
        }
    }
}
