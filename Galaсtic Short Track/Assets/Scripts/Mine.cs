using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject exploid;
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "pl1")
        {
            _ = Instantiate(exploid, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "pl2")
        {
            _ = Instantiate(exploid, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
