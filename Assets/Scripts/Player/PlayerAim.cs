using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private Transform aimTransform;
    private Transform playerSprite;

    Vector3 aimLocalScale;
    Vector3 playerLocalScale;

    void Start()
    {
        aimTransform = transform.Find("Aim");
        playerSprite = transform.Find("PlayerSprite");
        aimLocalScale = Vector3.one;
        playerLocalScale = Vector3.one;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        

        if (angle > 90 || angle < -90)
        {
            aimLocalScale.y = -1f;
            playerLocalScale.x = -1f;
        }
        else
        {
            aimLocalScale.y = 1f;
            playerLocalScale.x = 1f;
        }
        aimTransform.localScale = aimLocalScale;
        playerSprite.localScale = playerLocalScale;
    }
}
