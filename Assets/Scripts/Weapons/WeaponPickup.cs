using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private bool isNearPlayer;
    private Collider2D holder; 

    void Update()
    {
        if(Input.GetButtonDown("Interact") && isNearPlayer)
        {
            holder.GetComponentInChildren<WeaponScript>().SetEquipped(false);
            Instantiate(holder.GetComponentInChildren<WeaponScript>().gameObject, transform.position, Quaternion.identity);
            holder.GetComponentInChildren<WeaponScript>().DestroyWeapon();

            //Создаем новое оружие в холдере, даем статус "экипировано"
            holder.GetComponentInChildren<WeaponHolder>().weaponPrefab = gameObject;
            gameObject.GetComponent<WeaponScript>().SetEquipped(true);
            holder.GetComponentInChildren<WeaponHolder>().UpdateWeapon();

            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && gameObject.GetComponent<WeaponScript>().isEquipped == false)
        {
            isNearPlayer = true;
            holder = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
            isNearPlayer = false;
    }
}
