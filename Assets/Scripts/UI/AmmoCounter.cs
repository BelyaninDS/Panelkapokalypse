using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{   
    public Text uitext;

    private int ammo;
    private GameObject player;
        
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        ammo = player.GetComponentInChildren<WeaponScript>().weapon.currentAmmo;
        if (ammo >= 0)
            uitext.text = ammo.ToString();
    }
}
