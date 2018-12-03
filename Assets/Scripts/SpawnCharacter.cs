using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour {
    public float yTarget = 0;
    public float dropSpeed = 5;
    public Vector2 yRange;
    public Vector2 xRange;

	// Use this for initialization
	void Start () {
        transform.position = new Vector2(
            Random.Range(xRange.x, xRange.y),
            Random.Range(yRange.x, yRange.y) + 30
            );

        dropSpeed *= Random.Range(0.85F, 1.3F);

        GetComponent<Character>().enabled = false;
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
            sr.color = new Color(sr.color.r, sr.color.b, sr.color.g, 0.7F);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(-Vector2.up * dropSpeed * Time.deltaTime);

        if (transform.position.y <= yTarget)
        {
            Character ch = GetComponent<Character>();
            if (ch != null) ch.enabled = true;
            SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
                sr.color = new Color(sr.color.r, sr.color.b, sr.color.g, 1.0F);
            this.enabled = false;
        }
	}
}
