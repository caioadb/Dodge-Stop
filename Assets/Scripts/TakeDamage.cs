using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    [SerializeField]
    HPValue hp=null;

    public bool _isInvulnerable;

    // Start is called before the first frame update
    void Start()
    {
        hp._RuntimeValue = 3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hp._RuntimeValue--;
            //play damage taken sound
            //play damage taken animation

            Destroy(collision.gameObject);
            if (hp._RuntimeValue != 0)
            {
                StartCoroutine(Invulnerability());
            }
        }
    }

    IEnumerator Invulnerability()
    {
        this.gameObject.GetComponent<CircleCollider2D>().enabled = false;   // deactivate player collider
        yield return new WaitForSeconds(1f);
        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;    // reactivate player 
    }
}
