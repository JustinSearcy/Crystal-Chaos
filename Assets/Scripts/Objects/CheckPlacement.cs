using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    [SerializeField] float detectionRadius = 1f;
    [SerializeField] float detectionOffset = 0.1f;
    [SerializeField] private SpriteRenderer teleportSprite = null;

    PlayerController playerController;

    Color tmpWhite;
    Color tmpRed;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        tmpWhite = teleportSprite.color;
        tmpRed = teleportSprite.color;
        tmpWhite = new Color(255, 255, 255, 0.3f);
        tmpRed = new Color(255, 0, 0, 0.3f);
    }

    private void Update()
    {
        Vector2 detectionPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - detectionOffset);
        Collider2D colliderCheck = Physics2D.OverlapCircle(detectionPoint, detectionRadius);
        if(colliderCheck != null)
        {
            playerController.teleportAllowed = false;
            teleportSprite.color = tmpRed;
        }
        else
        {
            playerController.teleportAllowed = true;
            teleportSprite.color = tmpWhite;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector2 detectionPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - detectionOffset);
        Gizmos.DrawWireSphere(detectionPoint, detectionRadius);
    }
}
