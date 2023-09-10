using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private Gun_Data gunData;
    [SerializeField] private Transform muzzle,cassing;
    [SerializeField] public TextMeshProUGUI ammunitionDisplay;
    [SerializeField] public Camera fpscamera;
    [SerializeField] public AudioManager audioManager;
    [SerializeField] public string shootAudioName;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    public void StartReload() 
    { 
        if (!gunData.reloading) 
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;

    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    RaycastHit hitInfo;
    public void Shoot()
    {
        //Spread
        float x = Random.Range(-gunData.spread, gunData.spread);
        float y = Random.Range(-gunData.spread, gunData.spread);

        Vector3 direction = fpscamera.transform.forward + new Vector3(x, y, 0);

        if (gunData.currentAmmo > 0)
        {
            if(CanShoot()) 
            {
                if(Physics.Raycast(fpscamera.transform.position, direction, out hitInfo, gunData.maxDistance))
                {
                    UnityEngine.Debug.Log(hitInfo.transform.name);
                    UnityEngine.Debug.Log(hitInfo.transform.tag);

                    Target target = hitInfo.transform.GetComponent<Target>();
                    if (target != null)
                    {
                        target.TakeDamage(gunData.damage);
                    }

                    if (hitInfo.rigidbody != null)
                    {
                        hitInfo.rigidbody.AddForce(-hitInfo.normal * gunData.impactForce);
                    }

                    gunData.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnGunShot();

                }
            }
        }
    }

    private void SpawnBulletTrail(Vector3 hitPoint)
    {
        GameObject bulletTrailEffect = Instantiate(gunData.bulletTrail.gameObject, muzzle.position, Quaternion.identity);
        Destroy(bulletTrailEffect, 2f);

        LineRenderer lineRender = bulletTrailEffect.GetComponent<LineRenderer>();

        lineRender.SetPosition(0, muzzle.position);
        lineRender.SetPosition(1, hitPoint);


    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzle.position, muzzle.forward);

        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(gunData.currentAmmo + " / " + gunData.magSize);
    }


    private void OnGunShot()
    {
        SpawnBulletTrail(hitInfo.point);
        GameObject impactGO = Instantiate(gunData.impactEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(impactGO, 3f);
        gunData.muzzleflash.Play();
        audioManager.Play(shootAudioName);
    }

}