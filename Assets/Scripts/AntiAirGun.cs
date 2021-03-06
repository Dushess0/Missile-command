using System.Collections.Generic;
using UnityEngine;

public class AntiAirGun : Building
{
    public Transform firePoint;
    public AntiAirProjectile projectile;
    public int maxAmmo = 10;
    public int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            SetAmmo(value);
        }
    }

    private int currentAmmo;
    private List<GameObject> ammoObjects;
    [SerializeField] int ammoStacking = 5;
    [SerializeField] GameObject ammoStartPoint;
    [SerializeField] float ammoHorizontalInterval;
    [SerializeField] float ammoVerticalInterval;

    void Start()
    {
        SetAmmo(maxAmmo);
    }

    public void Fire(Vector2 destination)
    {
        if (currentAmmo <= 0 || Destroyed) return;
        var gameObject = Instantiate(projectile, firePoint.position, Quaternion.identity);
        gameObject.GetComponent<AntiAirProjectile>().Setup(destination);
        CurrentAmmo--;
        GameManager.instance.statistics.AntiAirFired++;
    }

    void SetAmmo(int ammo)
    {
        if (ammoObjects == null) CreateAmmoObjects();

        currentAmmo = ammo % (maxAmmo + 1);
        for (int i = 0; i < ammoObjects.Count; i++)
        {
            if (i < currentAmmo)
                ammoObjects[i].SetActive(true);
            else
                ammoObjects[i].SetActive(false);
        }
    }

    public void RestockAmmo()
    {
        SetAmmo(maxAmmo);
    }

    void CreateAmmoObjects()
    {
        int fullstacks = maxAmmo / ammoStacking;
        int remains = maxAmmo - ammoStacking * fullstacks;
        ammoObjects = new List<GameObject>();
        for (int i = 0; i < fullstacks; i++)
        {
            for (int j = 0; j < ammoStacking; j++)
            {
                var newLocation = ammoStartPoint.transform.position + new Vector3(ammoHorizontalInterval * i, ammoVerticalInterval * j);
                var gameObject = Instantiate(ammoStartPoint, newLocation, Quaternion.identity, transform);
                gameObject.SetActive(true);
                ammoObjects.Add(gameObject);
            }
        }
        for (int i = 0; i < remains; i++)
        {
            var newLocation = ammoStartPoint.transform.position + new Vector3(ammoHorizontalInterval * fullstacks, ammoVerticalInterval * i);
            var gameObject = Instantiate(ammoStartPoint, newLocation, Quaternion.identity, transform);
            gameObject.SetActive(true);
            ammoObjects.Add(gameObject);
        }

    }
}
