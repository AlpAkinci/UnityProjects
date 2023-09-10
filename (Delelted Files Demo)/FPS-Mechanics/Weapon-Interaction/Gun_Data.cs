using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun" , menuName = "Weapon/Gun")]

public class Gun_Data : ScriptableObject
{
    [Header ("Info")]
    public new string name;

    [Header("Shooting")]
    public float shootForce, impactForce,maxDistance,damage,caseForce;
    public LineRenderer bulletTrail;
    public float spread;
    public GameObject impactEffect,bullet;
    public ParticleSystem muzzleflash;
    public GameObject GunModel;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    [Tooltip("In RPM")] public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;

}
