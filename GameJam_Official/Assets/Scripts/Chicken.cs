using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnCollision : MonoBehaviour
{
    public GameObject floor;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == floor)
        {
            gameObject.SetActive(false);
        }
    }
}
