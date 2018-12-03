using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    static Color transparent = new Color(1, 1, 1, 0);

    public float time = 0.4F;

    float start = 0;
    SpriteRenderer _sr;
	// Use this for initialization
	void Start () {
        transform.localScale = Vector3.one * Random.Range(0.75F, 1.0F);
        time *= Random.Range(0.9F, 1.2F);

        start = Time.time;
        _sr = GetComponent<SpriteRenderer>();
        if (_sr == null) DestroyImmediate(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        float ratio = (Time.time - start) / time;
        _sr.color = Color.Lerp(Color.white, transparent, ratio);

        if (ratio > 1) Destroy(this.gameObject);
	}
}
