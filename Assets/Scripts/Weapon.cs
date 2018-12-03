using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    string name;
	
	// Update is called once per frame
	virtual protected void Update () {
        Vector3 dir = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x));

        float angle = transform.localEulerAngles.z;
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;

        if (angle < 90 && angle > -90) transform.localScale = Vector3.one;
        else transform.localScale = new Vector3(1, -1, 1);
	}
}
