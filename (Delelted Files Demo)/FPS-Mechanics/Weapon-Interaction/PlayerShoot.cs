using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;

    private void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            if (Input.GetMouseButton(0))
                shootInput?.Invoke();

            if (Input.GetKeyDown("r"))
                reloadInput?.Invoke();
        }
    }

}
