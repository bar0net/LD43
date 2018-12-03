using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public bool destroyOnContact = true;
    public bool selfPush = false;
    public float pushForce = 2.0F;
    public int selfDamage = 0;

    public int damage
    {
        get { return damage_; }
        private set { damage_ = value; }
    }

    [SerializeField]
    private int damage_ = 1;

    virtual public void Contact()
    {
        if (destroyOnContact) Destroy(this.gameObject);
    }
}
