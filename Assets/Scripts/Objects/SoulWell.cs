using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulWell : MonoBehaviour
{
    [SerializeField] GameObject interactIcon = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            interactIcon.SetActive(true);
            collision.gameObject.GetComponent<PlayerController>().soulWellInteractable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactIcon.SetActive(false);
            collision.gameObject.GetComponent<PlayerController>().soulWellInteractable = false;
        }
    }
}
