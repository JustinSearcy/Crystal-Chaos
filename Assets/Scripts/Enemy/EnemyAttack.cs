using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackDamage = 1;

    EnemyMovement enemyMovement;
    private void Start()
    {
        enemyMovement = FindObjectOfType<EnemyMovement>();
    }
    public void DealDamage()
    {
        if (enemyMovement.InAttackRange())
        {
            GameObject targetCrystal = enemyMovement.GetTargetCrystal().gameObject;
            targetCrystal.GetComponent<CrystalHealth>().TakeDamage(attackDamage);
        }
    }
}
