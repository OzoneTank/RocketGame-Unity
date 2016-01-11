using UnityEngine;
using System.Collections;

public class SmokeEffect : MonoBehaviour {

    public float maxTime = 3.0f;
    private float timeLeft;
    SpriteRenderer spriteRenderer;

	void Start () {
        timeLeft = maxTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        Color color = spriteRenderer.color;
        color.a = timeLeft / maxTime;
        spriteRenderer.color = color;
        if (timeLeft <= 0) {
            Destroy(gameObject);
        }
	}
}
