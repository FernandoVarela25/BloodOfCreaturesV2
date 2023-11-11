using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed ;
    public float TimeToLive = 2.0f;
    private float lifeTimer;
    private Rigidbody2D Rigidbody2D;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        Vector2 direction = LeslieMovement.shootDirection;

        if (direction != Vector2.zero)
        {
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }

            // Establecer la velocidad directamente en el Rigidbody2D
            Rigidbody2D.velocity = direction * Speed;

            lifeTimer = TimeToLive;
        }
        else
        {
            // Si la dirección es un vector nulo, destruye la bala inmediatamente
            DestroyBullet();
        }
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

    public void DestroyBullet()
    {
        Debug.Log("Destroying Bullet");
        Destroy(gameObject);
    }
}
