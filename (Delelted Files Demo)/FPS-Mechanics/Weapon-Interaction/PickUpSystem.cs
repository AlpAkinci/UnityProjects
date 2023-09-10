using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    public Gun gunScript;
    public GameObject gunObject;
    public Sway gunScript2;
    public Rigidbody rb;
    public MeshCollider collMesh;
    public Transform player, gunContainer,gunRotation, fpsCam;
    public string pickUpAudioName;
    public string dropAudioName;
    public AudioManager audioManager;

    public float pickUpRange, pickUpTime;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        int LayerSeeGun = LayerMask.NameToLayer("Weapon");
        int LayerDefault = LayerMask.NameToLayer("Default");

        if (!equipped)
        {
            gunScript.enabled = false;
            gunScript2.enabled = false;
            rb.isKinematic = false;
            collMesh.isTrigger = false;
            gunObject.layer= LayerDefault;

        }
        if (equipped)
        {
            slotFull = true;
            rb.isKinematic = true;
            collMesh.isTrigger = true;
            gunObject.layer = LayerSeeGun;
        }
    }
    private void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            //Check if player is in range and "E" is pressed
            Vector3 distanceToPlayer = player.position - transform.position;
            if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
            {
                PickUp();
            }


            //Drop if equipped and "Q" is pressed
            if (equipped && Input.GetKeyDown(KeyCode.Q)) Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to the equippedPosition
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = gunRotation.rotation;


        //Make Rigidbody Kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        collMesh.isTrigger = true;

        //Enable script
        gunScript.enabled = true;
        gunScript2.enabled = true;

        audioManager.Play(pickUpAudioName);

    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody and BoxCollider normal
        rb.isKinematic = false;
        collMesh.isTrigger = false;

        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //Add force
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random,random,random) * 10);

        //Disable script
        gunScript.enabled = false;
        gunScript2.enabled = false;

        audioManager.Play(dropAudioName);
    }
}
