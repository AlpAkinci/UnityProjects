using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBox : MonoBehaviour
{
    private RaycastHit boxHit;
    public LayerMask whatIsTeleport;
    private bool teleportFloor;
    public Vector3 TeleportPosition;
    public float playerHeight;
    MovementController movementController;

    private void Start()
    {
        movementController = gameObject.GetComponent<MovementController>();
    }
    private void Update()
    {
        teleportFloor = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsTeleport);
        if (teleportFloor == true)
            Teleport();
    }

    private void Teleport()
    {
        gameObject.transform.position = TeleportPosition;

        Debug.Log("Teleport");
    }

}
