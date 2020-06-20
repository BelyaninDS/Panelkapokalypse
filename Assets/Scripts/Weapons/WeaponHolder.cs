using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    //[HideInInspector]
    public GameObject weaponPrefab;

    public Transform holder;

    void Start()
    {
        if (weaponPrefab != null)
        {
            weaponPrefab = Instantiate(weaponPrefab, holder.position + Vector3.right * transform.localScale.x * 0.1f, holder.rotation);
            weaponPrefab.GetComponent<SingleShotWeaponScript>().enabled = true;
            weaponPrefab.GetComponent<SingleShotWeaponScript>().SetEquipped(true);
            weaponPrefab.transform.SetParent(holder);          
        }
    }

    public void UpdateWeapon()
    {
        if (weaponPrefab != null)
        {
            weaponPrefab = Instantiate(weaponPrefab, holder.position + Vector3.right * transform.localScale.x * 0.1f, holder.rotation);
            weaponPrefab.GetComponent<SingleShotWeaponScript>().enabled = true;
            weaponPrefab.GetComponent<SingleShotWeaponScript>().SetEquipped(true);
            weaponPrefab.transform.SetParent(holder);           
        }
    }


}
