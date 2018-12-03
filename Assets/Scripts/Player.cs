using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    [Header("Player Abilities")]
    public float dashSpeed = 15F;
    public float dashTime = 0.15F;
    public float dashCooldown = 1.6F;
    public float dmgRecoveryTime = 0.2F;

    [Header("Progression")]
    public int exp = 0;
    public int gold = 0;

    public int nextLevelExp = 30;

    float dashTimer = 0;
    Vector3 dashDir = Vector3.zero;
    float dashCDtimer = 0;

    [SerializeField]
    float invincibleTimer = 0;

    Manager _m;
	// Use this for initialization
	void Start () {
        base.Start();

        Color c = Random.ColorHSV();
        transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().color = c;

        _m = FindObjectOfType<Manager>();
        GameObject w = GameObject.Instantiate(_m.RandomWeapon(), transform.position, Quaternion.identity, transform.GetChild(0));

        _m.UpdateSpeedText((int)speed);
        _m.UpdateLifeUI(hp, max_hp);
        _m.UpdateGoldText(gold);
        _m.UpdateExpBar(exp, nextLevelExp);
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();

        Vector3 inputDir = (Input.GetAxis("Vertical") * Vector3.up + Input.GetAxis("Horizontal") * Vector3.right);

        // Movement Control
        transform.Translate(speed * inputDir *  Time.deltaTime);

        // Dash Check
        if (Input.GetButton("Fire2") && dashCDtimer <= 0 && inputDir.magnitude != 0)
        {
            dashDir = inputDir.normalized;
            dashTimer = Time.time;
            dashCDtimer = dashTime + dashCooldown;
            SetInvincible(true);
        }

        // Apply Dash
        if (dashTimer > 0)
        {
            transform.Translate(dashSpeed * dashDir * Time.deltaTime);
            if (Time.time - dashTimer > dashTime)
            {
                if(invincibleTimer <= 0) SetInvincible(false);
                dashTimer = 0;
            }
        }

        if (dashCDtimer > 0) dashCDtimer -= Time.deltaTime;

        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0 && dashTimer == 0)
            {
                SetInvincible(false);
            }
        }
	}

    public override void Hit(int dmg)
    {
        SetInvincible(true);
        invincibleTimer = dmgRecoveryTime;

        base.Hit(dmg);

        _m.UpdateLifeUI(hp, max_hp);
    }

    public void RecieveExperience(int value)
    {
        exp += value;

        if (exp >= nextLevelExp)
        {
            exp -= nextLevelExp;
            LevelUp();
        }
        if (_m != null && hp > 0)
            _m.UpdateExpBar(exp, nextLevelExp);
    }

    void LevelUp()
    {
        Debug.Log("Level UP!");
    }

    public void RecieveGold(int value)
    {
        gold += value;
        _m.UpdateGoldText(gold);
    }


    void SetInvincible(bool value)
    {
        invincible = value;

        if (value) SetAlpha(0.75F);
        else SetAlpha(1.0F);
    }

    void SetAlpha(float alpha)
    {
        SpriteRenderer[] srs = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sr in srs)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.tag == "ThePit")
        {
            gameObject.tag = "None";
            Enemy[] es = FindObjectsOfType<Enemy>();
            foreach (Enemy e in es)
            {
                if (e.enabled)
                {
                    e.Die();
                    Destroy(e);
                }
                else Destroy(e.gameObject);
            }

            GetComponentInChildren<Weapon>().enabled = false;
            _m.Sacrifice(exp, gold);
            base.Die();
            
        }
    }

    public override void Die()
    {
        gameObject.tag = "None";
        Enemy[] es = FindObjectsOfType<Enemy>();
        foreach (Enemy e in es)
        {
            if (e.enabled)
            {
                e.Die();
                Destroy(e);
            }
            else Destroy(e.gameObject);
        }
        _m.GameOver();

        GetComponentInChildren<Weapon>().enabled = false;
        base.Die();
    }
}
