using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotWeaponScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shotDelay;
    public float scatter;
    public bool isAutomatic;

    [HideInInspector]
    public bool isEquipped;

    private float shotTime;
    private float angle;


    // Start is called before the first frame update
    void Start()
    {
        firePoint.SetParent(gameObject.transform);
        shotTime = Time.time;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Расчет угла поворота оружия относительно горизонтальной плоскости
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - firePoint.position.y, mousePos.x - firePoint.transform.position.x) * Mathf.Rad2Deg;

        if (Time.time > shotTime + shotDelay && isEquipped)
        {
            if (isAutomatic)
            {
                if (Input.GetButton("Fire1"))
                {
                    Shoot();
                    shotTime = Time.time;
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                    shotTime = Time.time;
                }
            }
        }
    }

    void Shoot()
    {
        angle += Random.Range(-scatter / 2, scatter / 2);
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0f, 0f, angle));

        angle *= Mathf.Deg2Rad;
        bulletInstance.GetComponent<Rigidbody2D>().velocity = 10f * new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
    }

    public void SetEquipped(bool value)
    {
        if (value)
            isEquipped = true;
        else
            isEquipped = false;
    }

    public void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}
