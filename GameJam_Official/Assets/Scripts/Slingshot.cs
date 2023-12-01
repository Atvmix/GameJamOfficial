using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;

    bool isMouseDown;

    public GameObject pigPrefab;

    public float pigPositionOffset;

    Rigidbody2D pig;
    Collider2D pigCollider;

    public float force; 

    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        Createpig();
    }

     void Createpig() 
     {
        pig = Instantiate(pigPrefab, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
        pigCollider = pig.GetComponent<Collider2D>();
        pigCollider.enabled = false;

        pig.isKinematic = true;


        ResetStrips();
     }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

            SetStrips(currentPosition);

            if (pigCollider)
            {
                pigCollider.enabled = true;
            }

        }
        else
        {
            ResetStrips();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        pig.isKinematic = false;
        Vector3 pigForce = (currentPosition - center.position) * force * -1;
        pig.velocity = pigForce;

        pig = null;
        pigCollider = null;
        Invoke("CreatePig", 2);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (pig)
        {
            Vector3 dir = position - currentPosition;
            pig.transform.position = position + dir.normalized * pigPositionOffset;
            pig.transform.right = -dir.normalized;
        }
      
    }
}
