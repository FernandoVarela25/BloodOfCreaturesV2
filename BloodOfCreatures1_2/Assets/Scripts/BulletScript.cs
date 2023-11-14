using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    public float TimeToLive = 2.0f;
    private float lifeTimer;
    private Rigidbody2D Rigidbody2D;
    public float cantidadDeDanio = 10f;

    private Vector2 bulletDirection;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(Mathf.Sign(Rigidbody2D.velocity.x) * 0.5f, 0.5f, 1f);

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
        EnemigoWolfN enemigo = collision.GetComponent<EnemigoWolfN>();
        if (enemigo != null)
        {
            enemigo.RecibirDanio(cantidadDeDanio);
            DestroyBullet();
        }
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
