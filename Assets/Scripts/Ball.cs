using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class PerfectBounceBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;
    private float ballRadius;

    [Header("Instellingen")]
    public float horizontalSpeed = 5f;   // constante horizontale snelheid
    public float bounceStrength = 10f;   // hoe sterk hij altijd omhoog stuitert

    private int directionX = 1; // 1 = rechts, -1 = links

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        rb.gravityScale = 1;
        rb.freezeRotation = true;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Bepaal straal (voor randen)
        ballRadius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;

        // Kies random start richting
        directionX = Random.value < 0.5f ? -1 : 1;

        // Start velocity
        rb.linearVelocity = new Vector2(directionX * horizontalSpeed, 0);
    }

    void Update()
    {
        KeepInsideCamera();
    }

    void KeepInsideCamera()
    {
        Vector3 pos = transform.position;

        float leftBound = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).x + ballRadius;
        float rightBound = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - ballRadius;
        float bottomBound = cam.ScreenToWorldPoint(new Vector3(0, 0, 0)).y + ballRadius;
        float topBound = cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y - ballRadius;

        Vector2 vel = rb.linearVelocity;

        // 🔹 Horizontale bounce
        if (pos.x <= leftBound && directionX < 0)
            directionX = 1;
        if (pos.x >= rightBound && directionX > 0)
            directionX = -1;

        // 🔹 Verticale bounce (altijd even sterk omhoog)
        if (pos.y <= bottomBound && vel.y < 0)
            vel.y = bounceStrength;
        if (pos.y >= topBound && vel.y > 0)
            vel.y = -vel.y;

        // Stel velocity opnieuw in → gravity werkt op Y, maar X blijft constant
        vel.x = directionX * horizontalSpeed;
        rb.linearVelocity = vel;
    }
}
