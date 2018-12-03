using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public float speed = 9F;
    public float radius = 2F;
    public float shakeIntensity = 0.4F;

    public Transform target;
    bool moving = false;
    Vector2 shake = Vector2.zero;
    float prevShakeMag = 0;
	// Use this for initialization
	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = target.transform.position - this.transform.position;
        dir.z = 0;
        float dst = dir.magnitude;

        if (moving || dst > radius)
        {
            if (dst == 0) moving = false;
            else moving = true;

            if (dst < speed * Time.deltaTime)
                transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
            else
                transform.Translate(dir.normalized * speed * Time.deltaTime);
        }

        if (shake.magnitude > 0 && shake.magnitude < prevShakeMag)
        {
            transform.Translate(-shake.normalized * 0.05F * Time.deltaTime);
            shake -= shake.normalized * 0.05F * Time.deltaTime;
            prevShakeMag = shake.magnitude;
        }
        else if (shake.magnitude > 0 && shake.magnitude >= prevShakeMag)
        {
            transform.Translate(-shake);
            shake = Vector2.zero;
            prevShakeMag = 0;
        }
	}

    public void Shake(Vector3 dir)
    {
        shake = Random.Range(0.9F, 1.2F) * shakeIntensity * dir.normalized;
        Vector2 norm = new Vector2(-dir.y, dir.x).normalized;
        norm *= shakeIntensity * Random.Range(0.1F, 0.3F);
        transform.Translate(shake);
        prevShakeMag = 1.01F * shake.magnitude;
    }
}
