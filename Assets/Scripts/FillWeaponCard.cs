using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillWeaponCard : MonoBehaviour {
    public Text[] texts;
    public Image img;

    public void SetUp(WeaponRanged w)
    {
        Bullet b = w.bullet.GetComponent<Bullet>();
        texts[0].text = w.name;
        texts[1].text = "Spread: " + w.spread;
        texts[2].text = "Recoil: " + w.recoil;
        texts[3].text = "Damage: " + b.damage;
        texts[4].text = "Bullet speed: " + b.speed;
        texts[5].text = "Bullet Reach: " + b.time;

        img.sprite = w.GetComponentInChildren<SpriteRenderer>().sprite;
    }
}
