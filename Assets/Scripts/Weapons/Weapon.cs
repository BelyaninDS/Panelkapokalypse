using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon 
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shotDelay;
    public float scatter;
    public bool isAutomatic;

    public Weapon(Transform firePoint, GameObject bulletPrefab, float shotDelay, float scatter, bool isAutomatic)
    {
        this.firePoint.position = firePoint.position;
        this.bulletPrefab = bulletPrefab;
        this.shotDelay = shotDelay;
        this.scatter = scatter;
        this.isAutomatic = isAutomatic;
    }

}
