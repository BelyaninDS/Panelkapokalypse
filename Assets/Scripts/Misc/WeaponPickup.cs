using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup: MonoBehaviour
{    void OnTriggerStay2D(Collider2D other)
     {
        if (other.tag == "Player" && gameObject.GetComponent<WeaponScript>().isEquipped == false && Input.GetButtonDown("Interact"))
        {
                //Создаем старое оружие на месте нового, убираем статус "экипировано"
                other.GetComponentInChildren<WeaponScript>().SetEquipped(false);
                Instantiate(other.GetComponentInChildren<WeaponScript>().gameObject, transform.position, Quaternion.identity);
                other.GetComponentInChildren<WeaponScript>().DestroyWeapon();

                //Создаем новое оружие в холдере, даем статус "экипировано"
                other.GetComponentInChildren<WeaponHolder>().weaponPrefab = gameObject;
                gameObject.GetComponent<WeaponScript>().SetEquipped(true);
                other.GetComponentInChildren<WeaponHolder>().UpdateWeapon();

                Destroy(gameObject);
        }
    }
}
