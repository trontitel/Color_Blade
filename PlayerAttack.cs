using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerMovement playerScript;
    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
        pom = this.gameObject;
    }

    Animator animator;
    GameObject pom;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && playerScript.isPlayerAtacking)
        {
            animator = other.gameObject.GetComponent<Animator>();
            if (other.gameObject.name == "enemyNPC") animator.SetBool("Dead", true);
            pom = other.gameObject;

            pom.gameObject.tag = "Null";
            Invoke("KillSlime", 0.3f);
        }
    }

    void KillSlime()
    {
        pom.SetActive(false);
        pom.gameObject.tag = "Enemy";
    }
}