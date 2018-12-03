using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRanged : Weapon {
    public GameObject bullet;
    public Transform nozzle;
    public float recoil = 0.1F;
    public float cooldown = 0.25F;
    public float characterRecoil = 0.1F;
    public float spread = 0.1F;

    float recoverSpeed = 2.0F;
    float cdTimer = 0;

    [SerializeField]
    Character _ch = null;
	// Use this for initialization
	void Start () {
        Transform parent = transform.parent;

        while (parent != null && _ch == null)
        {
            _ch = parent.GetComponent<Character>();
            parent = parent.parent;
        }
	}
	
	// Update is called once per frame
	protected override void Update ()  {
        base.Update();

        if (Input.GetButton("Fire1") && cdTimer <= 0) Fire();

        if (transform.localPosition.magnitude > recoverSpeed * Time.deltaTime)
        {
            transform.localPosition -= transform.localPosition.normalized * recoverSpeed * Time.deltaTime;
        }
        else transform.localPosition = Vector3.zero;

        if (cdTimer > 0) cdTimer -= Time.deltaTime;
	}

    void Fire()
    {
        GameObject go = (GameObject)Instantiate(bullet,nozzle.position,Quaternion.identity,null);
        Bullet b = go.GetComponent<Bullet>();
        if (b != null) b.SetDirection(transform.right + Random.Range(-spread, spread) * transform.up);

        transform.localPosition = -recoil * this.transform.right;
        if (_ch != null) _ch.AddVelocity(-characterRecoil * this.transform.right);

        cdTimer = cooldown;
        UnityEngine.Camera.main.GetComponent<CameraFollow>().Shake(transform.right);
    }
}
