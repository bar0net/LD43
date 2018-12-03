using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [Header("Character Attributes")]
    public int hp = 3;
    public float speed = 5F;

    [Header("Impact Recovery")]
    protected Vector3 velocity = Vector3.zero;
    public float drag = 1F;
    public float maxVelocity = 2.5F;
    
    [SerializeField]
    protected int max_hp = 0;
    [SerializeField]
    protected bool invincible = false;

    virtual protected void Start()
    {
        max_hp = hp;
    }

	// Update is called once per frame
	virtual protected void Update () {
        transform.Translate(velocity * Time.deltaTime);

        float dragValue = (drag + velocity.magnitude * 0.4F);
        if (velocity.magnitude < dragValue * Time.deltaTime)
            velocity = Vector3.zero;
        else
            velocity -= velocity.normalized * dragValue * Time.deltaTime;
	}

    public void AddVelocity(Vector3 v)
    {
        velocity += v;

        if (velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
    }

    public virtual void Hit(int dmg)
    {
        hp -= dmg;

        if (hp <= 0) Die();
    }

    virtual public void Die()
    {
        hp = 0;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in sprites) sr.color = new Color(0.2F * sr.color.r, 0.2F * sr.color.g, 0.2F * sr.color.b, 0.8F);

        Collider2D[] colls = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D c in colls) c.enabled = false;

        Animator[] anims = GetComponentsInChildren<Animator>();
        foreach (Animator a in anims) a.enabled = false;

        Rigidbody2D[] rbs = GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in rbs) Destroy(rb);

        enabled = false;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible && collision.tag == "Damage")
        {
            Damage d = collision.GetComponent<Damage>();
            if (d != null)
            {
                Hit(d.damage);
                d.Contact();
                AddVelocity((transform.position - collision.gameObject.transform.position).normalized * d.pushForce);

                Transform t = collision.gameObject.transform;
                Character ch = null;

                while (t != null && ch == null)
                {
                    ch = t.GetComponent<Character>();
                    t = t.parent;
                }

                if (ch != null)
                {
                    if (d.selfPush)
                        ch.AddVelocity(-(transform.position - collision.gameObject.transform.position).normalized * d.pushForce);
                    ch.Hit(d.selfDamage);
                }
            }
        }
    }
}
