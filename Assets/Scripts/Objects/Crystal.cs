using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    bool isSelected = false;

    private void OnMouseDown()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (!isSelected && !playerController.menuOpen)
        {
            isSelected = true;
            playerController.SelectCrystal(this.gameObject);
        }
    }

    public void DeselectCrystal()
    {
        isSelected = false;
    }
}
