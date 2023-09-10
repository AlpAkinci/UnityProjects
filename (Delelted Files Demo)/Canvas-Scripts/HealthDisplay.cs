using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health = 10;
    public Text healthText;

    private void Update()
    {
        healthText.text = health.ToString();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            health--;
        }
    }
}
