using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour
{

    public float AttackPoint;
    public bool HitArea;

    private int count;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemies") && collision.isTrigger)
        {
            var enemy = collision.GetComponent<EnemyActive>();
            if (HitArea)
            {
                enemy.HealtPoint -= AttackPoint;

            } else
            {
                if (count == 0)
                {
                    enemy.HealtPoint -= AttackPoint;
                    var player = transform.parent.parent.GetComponent<PlayerActive>();
                    player.StaminaPoint += 10;

                }
                count++;
            }
        }

        else if (collision.CompareTag("Player") && collision.isTrigger)
        {
            var player = collision.GetComponent<PlayerActive>();
            player.HealtPoint -= AttackPoint;
        }
    }

    private void OnDisable()
    {
        count = 0;
    }

}
