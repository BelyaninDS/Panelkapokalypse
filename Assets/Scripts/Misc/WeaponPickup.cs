using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup: MonoBehaviour
{    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameObject.GetComponent<SingleShotWeaponScript>().isEquipped == false)
        {         
            {
                other.GetComponentInChildren<SingleShotWeaponScript>().DestroyWeapon();
                //Instantiate<GameObject>(other.GetComponentInChildren<SingleShotWeaponScript>().gameObject, transform.position, Quaternion.identity);

                gameObject.GetComponent<SingleShotWeaponScript>().SetEquipped(true);
                other.GetComponentInChildren<WeaponHolder>().weaponPrefab = gameObject;
                other.GetComponentInChildren<WeaponHolder>().UpdateWeapon();
                Destroy(gameObject);
            }
        }
    }
}
