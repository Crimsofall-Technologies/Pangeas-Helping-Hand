using UnityEngine;

public class MovingBoards : MonoBehaviour
{
    public int points = 100;
    public bool removePoints = false; //false to add points

    [Space]
    public float speed = 5f;
    public float minX = -10f;
    public float maxX = 10f;
    public bool startMovingRight = true;

    private int direction;
    public Animator animator { get; private set; }

    void Start()
    {
        direction = startMovingRight ? 1 : -1;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Move the object
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);

        // Clamp position and reverse direction if limits are exceeded
        float x = transform.position.x;

        if (x >= maxX)
        {
            x = maxX;
            direction = -1;

            //reset animation
            animator.SetBool("Hit", false);
        }
        else if (x <= minX)
        {
            x = minX;
            direction = 1;

            //reset animation
            animator.SetBool("Hit", false);
        }

        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
}
