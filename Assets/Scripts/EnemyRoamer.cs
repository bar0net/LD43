using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamer : Enemy {
    public Vector2 xRange = new Vector2(-11, 11);
    public Vector2 yRange = new Vector2(-8, 8);
    public float roamTime = 1;
    public float shootTime = 0.5F;
    public float waitTime = 0.25F;
    public GameObject bullet;

    Vector2 target;

    enum RoamState { MOVE, SHOOT, WAIT}
    RoamState state = RoamState.MOVE;
    float timer;

	// Use this for initialization
	void Start () {
        target = new Vector2(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y));
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        timer -= Time.deltaTime;

        switch (state)
        {
            case (RoamState.MOVE):
                Vector2 diff = target - (Vector2)transform.position;
                if (diff.magnitude < 0.2F) target = new Vector2(Random.Range(xRange.x, xRange.y), Random.Range(yRange.x, yRange.y));
                else transform.Translate(diff.normalized * speed * Time.deltaTime);
                if (timer < 0)
                {
                    timer = shootTime;
                    state = RoamState.SHOOT;
                }
                break;

            case (RoamState.SHOOT):
                if (timer < 0)
                {
                    Vector2 dir = Random.insideUnitCircle;

                    for (int i = 0; i < 4; i++)
                    {
                        GameObject go = GameObject.Instantiate(bullet, transform.position, Quaternion.identity, null);
                        Bullet b = go.GetComponent<Bullet>();
                        b.SetDirection(dir);

                        dir = new Vector2(-dir.y, dir.x);
                    }

                    timer = waitTime;
                    state = RoamState.WAIT;
                }
                break;

            case (RoamState.WAIT):
                if (timer < 0)
                {
                    timer = roamTime;
                    state = RoamState.MOVE;
                }
                break;
        }
    }
}
