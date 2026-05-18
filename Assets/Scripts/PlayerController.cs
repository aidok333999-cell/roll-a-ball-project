using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private int count;

    private float movementX;
    private float movementY;

    public float speed = 0;
    public float sprintMultiplier = 2f;

    public TextMeshProUGUI countText;
    public AudioSource pickupSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        count = 0;

        SetCountText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            pickupSound.Play();

            count++;

            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            FindFirstObjectByType<GameManager>().WinGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindFirstObjectByType<GameManager>().LoseGame();

            gameObject.SetActive(false);
        }
    }
}