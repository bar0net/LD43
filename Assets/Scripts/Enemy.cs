using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    public Transform greenLifeBar;
    public Transform redLifeBar;
    public GameObject lifeBar;

    public int exp = 1;
    public int gold = 1;

    public override void Hit(int dmg)
    {
        if (max_hp == 0) max_hp = 1;
        hp -= dmg;

        if (hp <= 0)
        {
            lifeBar.SetActive(false);
            SpriteRenderer sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            sr.color = new Color(0.4F, 0.25F, 0.25F, 0.8F);
            FindObjectOfType<Player>().RecieveExperience(exp);
            FindObjectOfType<Player>().RecieveGold(gold);
            Die();
        }
        else
        {
            greenLifeBar.localScale = new Vector3(
                redLifeBar.localScale.x * (float)hp / (float)max_hp,
                greenLifeBar.localScale.y,
                1.0F
            );
        }
    }
}
