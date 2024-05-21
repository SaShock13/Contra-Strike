using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysChasing : MonoBehaviour
{
    GameObject player;
    Vector3 direction = Vector2.one;
    [SerializeField] float enemyChaseSpeed = 2;
    [HideInInspector] public bool chaseMode = false;
    [SerializeField] GameObject sprite;
    Animator animator;
    [SerializeField] Transform leftConstrainPoint;
    [SerializeField] Transform rightConstrainPoint;
    bool isConstrained = false;



    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        Debug.Log("EnemyChasing Загружен");
    }

    private void FixedUpdate()
    {
        if (chaseMode&!isConstrained)
        {
            direction = player.transform.position - transform.position;
            
            Vector3 enemyScale = new Vector3(1,1,1);
            if (direction.x < 0 )
            {
                if (leftConstrainPoint!=null)
                {
                    if (transform.position.x <= leftConstrainPoint.position.x)
                    {
                        Debug.Log("Левый ограничитель");
                        Costraining();
                    }
                    else StopConstrain(); 
                }
                enemyScale.x = -1;
                sprite.transform.localScale = enemyScale;
            }
            else 
            {
                if (rightConstrainPoint!=null)
                {
                    if (transform.position.x >= rightConstrainPoint?.position.x)
                    {
                        Debug.Log("Правый ограничитель");
                        Costraining();
                    }
                    else StopConstrain(); 
                }
                enemyScale.x = 1;
                sprite.transform.localScale = enemyScale;
            }
            
            direction.y = 0;
            transform.position += direction.normalized * Time.fixedDeltaTime * enemyChaseSpeed;
        }
        
    }

    public void StartChasing(GameObject playerToChase)
    {
        animator.SetBool("Walk", true);
        chaseMode = true;
        player = playerToChase;
    }

    public void StopChasing()
    {
        chaseMode = false;
        animator.SetBool("Walk",false);
    }

    void Costraining()
    {
        direction = Vector2.zero;
        animator.SetBool("Walk", false);
    }
    void StopConstrain()
    {
        animator.SetBool("Walk", true);
    }
}
