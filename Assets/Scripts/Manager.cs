using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour {
    [Header("UI")]
    public Image[] lifebars;
    public Text spdText;
    public Text goldText;
    public RectTransform expBar;
    public PanelWeapons panelWeapons;
    public GameObject panelImproveWeapons;
    public FillWeaponCard activeCard;

    [Header("player")]
    public GameObject player;

    static Color _green = new Color(0.32F, 0.75F, 0.27F, 1.0F);
    static Color _red = new Color(0.98F, 0.42F, 0.42F, 1.0F);
    static Color _gray = new Color(0.8F, 0.8F, 0.8F, 1.0F);

    GameObject[] activeWeapons;

    public GameObject[] T1Weapons;
    public GameObject[] T2Weapons;

    GameObject active;

    private void Awake()
    {
        activeWeapons = new GameObject[T1Weapons.Length];
        for (int i = 0; i < T1Weapons.Length; i++)
        {
            activeWeapons[i] = T1Weapons[i];
        }
    }

    public void UpdateLifeUI(int hp, int max_hp)
    {
        int i = 0;
        int limit = Mathf.Min(hp, lifebars.Length);
        for (; i < limit; ++i) lifebars[i].color = _green;

        limit = Mathf.Min(max_hp, lifebars.Length);
        for (; i < limit; ++i) lifebars[i].color = _red;

        for (; i < lifebars.Length; ++i) lifebars[i].color = _gray;
    }

    public void UpdateSpeedText(int value)
    {
        spdText.text = value.ToString();
    }

    public void UpdateGoldText(int value)
    {
        goldText.text = "$" + value.ToString();
    }

    public void UpdateExpBar(int value, int total)
    {
        if (value < total)
            expBar.sizeDelta = new Vector2(640.0F * (float)(value + 1) / (float)(total + 1), 12.0F);
        else
            expBar.sizeDelta = new Vector2(640.0F, 12.0F);
    }

    public void RaisePanelWeapons(bool upgrade)
    {
        // GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject go in gos) Destroy(go);

        // Pause Spawners
        FindObjectOfType<Spawner>().enabled = false;

        panelImproveWeapons.SetActive(true);
        panelWeapons.Fill(activeWeapons);

        active = T2Weapons[Random.Range(0, T2Weapons.Length)];
        activeCard.SetUp(active.GetComponent<WeaponRanged>());

    }

    public void SelectNewWeapon(int i)
    {
        activeWeapons[i] = active;
        panelImproveWeapons.SetActive(false);

        GameObject go = GameObject.Instantiate(player);
        UnityEngine.Camera.main.GetComponent<CameraFollow>().target = go.transform;

        FindObjectOfType<Spawner>().enabled = true;
    }

    public void Sacrifice(int exp, int gold)
    {
        RaisePanelWeapons((float)(gold + exp) / 200.0F > Random.value);
    }

    public void GameOver()
    {
        GameObject go = GameObject.Instantiate(player);
        UnityEngine.Camera.main.GetComponent<CameraFollow>().target = go.transform;
    }

    public GameObject RandomWeapon()
    {
        return activeWeapons[Random.Range(0, activeWeapons.Length)];
    }
}
