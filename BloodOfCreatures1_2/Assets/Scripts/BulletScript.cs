using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    public float TimeToLive = 2.0f;
    private float lifeTimer;
    private Rigidbody2D Rigidbody2D;
    public float cantidadDeDanio = 20f;

    private Vector2 bulletDirection;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(Mathf.Sign(Rigidbody2D.velocity.x) * 0.3f, 0.3f, 1f);

        if (Rigidbody2D.velocity != Vector2.zero)
        {
            lifeTimer = TimeToLive;
        }
        else
        {
            // Si la velocidad es cero, destruir la bala inmediatamente
            DestroyBullet();
        }
    }

    public void Shoot(Vector2 direction, float speed)
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(Mathf.Sign(direction.x) * 0.5f, 0.5f, 1f);

        Rigidbody2D.velocity = direction * speed;
        lifeTimer = TimeToLive;
    }

    private void Update()
    {
        if (lifeTimer > 0)
        {
            lifeTimer -= Time.deltaTime;
        }

        if (lifeTimer <= 0)
        {
            DestroyBullet();
        }
    }

    private void OnBecameInvisible()
    {
        DestroyBullet();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto con el que colisiona tiene el tag "NoColision"
        if (collision.CompareTag("Leslie"))
        {
            Debug.Log("Colisión con objeto de tag Leslie. Ignorando...");
            return;  // Ignora la colisión
        }

        // Verifica si el objeto con el que colisiona tiene el tag "Borde"
        if (collision.CompareTag("Borde"))
        {
            Debug.Log("Colisión con objeto de tag Borde. Ignorando...");
            return;  // Ignora la colisión
        }
        if (collision.CompareTag("Posiones"))
        {
            Debug.Log("Colisión con objeto de tag Posiones. Ignorando...");
            return;  // Ignora la colisión
        }

        Debug.Log("Colisión con: " + collision.gameObject.name);

        Enemigo2D enemigo2D = collision.GetComponent<Enemigo2D>();
        EnemigoWolfN enemigoWolfN = collision.GetComponent<EnemigoWolfN>();

        if (enemigo2D != null)
        {
            Debug.Log("Colisión con Enemigo2D");
            enemigo2D.RecibirDanio(cantidadDeDanio);
        }

        if (enemigoWolfN != null)
        {
            Debug.Log("Colisión con EnemigoWolfN");
            enemigoWolfN.RecibirDanio(cantidadDeDanio);
        }

        // Agrega un mensaje de depuración para verificar la destrucción de la bala
        Debug.Log("Destruyendo la bala");
        DestroyBullet();
    }

    public Vector2 GetDirection()
    {
        if (Rigidbody2D != null && Rigidbody2D.velocity != Vector2.zero)
        {
            return Rigidbody2D.velocity.normalized;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;

        if (Rigidbody2D != null && Rigidbody2D.velocity != Vector2.zero)
        {
            Rigidbody2D.velocity = Rigidbody2D.velocity.normalized * Speed;
        }
    }

    public void InitializeBullet(Vector2 direction)
    {
        bulletDirection = direction;
    }

    public void DestroyBullet()
    {
        Debug.Log("Destroying Bullet");
        Destroy(gameObject);
    }
}
