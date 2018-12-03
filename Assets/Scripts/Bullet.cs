using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Damage
{
    public float time = 0.5F;
    public float speed = 20.0F;
    public Vector2 dir = Vector2.zero;
    public GameObject explosion;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(dir * speed * Time.deltaTime);
        //transform.Translate(transform.right * speed * Time.deltaTime);

        time -= Time.deltaTime;
        if (time <= 0) Destroy(this.gameObject);
	}

    public void SetDirection(Vector2 direction)
    {
        this.dir = direction.normalized;
    }

    public override void Contact()
    {
        base.Contact();

        GameObject.Instantiate(explosion, transform.position, Quaternion.identity, null);
    }
}
