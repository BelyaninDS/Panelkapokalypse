using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    //Текущее оружие
    public GameObject weaponPrefab;

    [HideInInspector]
    public Transform holder;

    void Start()
    {
        holder = transform;
        if (weaponPrefab != null)
        {
            weaponPrefab = Instantiate(weaponPrefab, holder.position + Vector3.right * transform.localScale.x * 0.1f, holder.rotation);
            weaponPrefab.GetComponent<WeaponScript>().enabled = true;
            weaponPrefab.GetComponent<WeaponScript>().SetEquipped(true);
            weaponPrefab.transform.SetParent(holder);          
        }
    }

    //Обновить оружие
    public void UpdateWeapon()
    {
        if (weaponPrefab != null)
        {
            holder.rotation = Quaternion.identity;
            weaponPrefab = Instantiate(weaponPrefab, holder.position + Vector3.right * 0.1f, holder.rotation);
            weaponPrefab.transform.localScale = new Vector3(1f, transform.localScale.y, transform.localScale.z);    //немного костыль
            weaponPrefab.GetComponent<WeaponScript>().enabled = true;
            weaponPrefab.GetComponent<WeaponScript>().SetEquipped(true);
            weaponPrefab.transform.SetParent(holder);
        }
    }


}
