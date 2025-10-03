using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Vector3 velocity = new Vector3(1, 1, 0);
    [SerializeField] Vector3 acceleration = new Vector3(0, -1, 0);
    Vector2 minScreen, maxScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minScreen = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maxScreen = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;

        Vector3 pos = transform.position;
        if (pos.y > maxScreen.y)
        {
            velocity.y = -Mathf.Abs(velocity.y);
        }
        if (pos.x > maxScreen.x)
        {
            velocity.x = -Mathf.Abs(velocity.x);
        }
        if (pos.y < minScreen.y)
        {
            velocity.y = Mathf.Abs(velocity.y);
        }
        if (pos.x < minScreen.x)
        {
            velocity.x = Mathf.Abs(velocity.x);
        }
    }
}