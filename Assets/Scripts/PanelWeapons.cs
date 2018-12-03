using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelWeapons : MonoBehaviour {
    public FillWeaponCard[] cards;
	
    public void Fill(GameObject[] weapons)
    {
        int i = 0;
        int len = Mathf.Min(cards.Length, weapons.Length);

        for(; i < len; ++i)
        {
            cards[i].SetUp(weapons[i].GetComponent<WeaponRanged>());
            cards[i].gameObject.SetActive(true);
        }

        for(; i < cards.Length; ++i)
        {
            cards[i].gameObject.SetActive(false);
        }

    }
}
