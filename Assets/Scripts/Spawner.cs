using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] entities;
    float delay = 2;
    float changerate = 0.95F;

    float time;
    float cooldown = 0;

	// Use this for initialization
	void Start () {
        time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (cooldown <= 0)
        {
            GameObject.Instantiate(entities[Random.Range(0, entities.Length)]);
            cooldown = delay;
        }
        else cooldown -= Time.deltaTime;

        if (Time.time - time > 20)
        {
            time = Time.time;
            delay *= changerate;
        }
	}
}
