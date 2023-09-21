using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public Transform slingshotLeft;
    public Transform slingshotRight;
    public Transform projectileSpawnPoint;
    public float launchPower = 10f;

    private Rigidbody2D projectileRigidbody;
    private bool isDragging = false;

    void Start()
    {
        projectileRigidbody = GetComponentInChildren<Rigidbody2D>();
        projectileRigidbody.isKinematic = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (IsMouseOverSlingshot(mousePosition))
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            projectileRigidbody.transform.position = mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 launchDirection = slingshotLeft.position - projectileSpawnPoint.position;
                projectileRigidbody.isKinematic = false;
                projectileRigidbody.AddForce(launchDirection.normalized * launchPower, ForceMode2D.Impulse);
                isDragging = false;
            }
        }
    }

    private bool IsMouseOverSlingshot(Vector3 mousePosition)
    {
        Collider2D leftCollider = slingshotLeft.GetComponent<Collider2D>();
        Collider2D rightCollider = slingshotRight.GetComponent<Collider2D>();

        return leftCollider.OverlapPoint(mousePosition) || rightCollider.OverlapPoint(mousePosition);
    }
}