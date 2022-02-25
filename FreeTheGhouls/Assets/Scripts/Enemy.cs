using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    [SerializeField]Transform player;
    [SerializeField]Vector3 playerPreviousPosition;

    [SerializeField]private float enemySpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPreviousPosition = player.position;     
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, playerPreviousPosition, enemySpeed * Time.deltaTime);
        if(transform.position == playerPreviousPosition)
        {
           StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        animator.SetBool("enemyDead", true);

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
