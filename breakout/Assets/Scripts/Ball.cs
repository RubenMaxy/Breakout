using UnityEngine;
using UnityEngine.WSA;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 initialSpeed;
    [SerializeField] private float velocityMultiplier = 1.1f;
    private Rigidbody2D rb;
    private bool isMoving = false; //Para evitar que se mueva antes de que la lance el jugador

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }

    private void Launch()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            isMoving = true;
            transform.parent = null;
            rb.linearVelocity = initialSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("block")) {
            Destroy(collision.gameObject);
            rb.linearVelocity *= velocityMultiplier; //Aumentamos la velocidad de la bola cada vez que destruye un bloque
        }
        VelocityFix(); //Soluciona un posible rebote horizontal o vertical eterno
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("lose"))
        {
            GameManager.Instance.ReloadScene();
        }
    }

    private void VelocityFix()
    {
        float velocidadDelta = 0.5f; // Velocidad que queremos que aumente la bola
        float velocidadMinima = 0.2f; // Velocidad mínima que queremos que tenga la bola

        if (Mathf.Abs(rb.linearVelocityX) < velocidadMinima) // Si la velocidad de la bola en el eje x es menor que la mínima
        {
            velocidadDelta = Random.value < 0.5f ? velocidadDelta : -velocidadDelta; // Elegimos un valor aleatorio entre -0.5 y 0.5
            rb.linearVelocity = new Vector2(rb.linearVelocityX + velocidadDelta, rb.linearVelocityY); // Aumentamos la velocidad de la bola
        }

        if (Mathf.Abs(rb.linearVelocityY) < velocidadMinima) // Si la velocidad de la bola en el eje y es menor que la mínima
        {
            velocidadDelta = Random.value < 0.5f ? velocidadDelta : -velocidadDelta; // Elegimos un valor aleatorio entre -0.5 y 0.5
            // Otra forma de aumentar la velocidad (esta vez en el eje y)
            rb.linearVelocity += new Vector2(0f, velocidadDelta); // Aumentamos la velocidad de la bola
        }
    }

}
