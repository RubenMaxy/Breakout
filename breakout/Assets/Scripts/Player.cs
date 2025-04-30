using UnityEngine;

public class Player : MonoBehaviour
{ 
    [SerializeField] private float speed = 5;
    [SerializeField] private float bound = 7.2f;
    private Vector2 startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = Transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x +moveInput * speed * Time.deltaTime, -bound, bound);
        transform.position = playerPosition;
    }

    public void ResetPlayer()
    {

    }
}