using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeslieMovement : MonoBehaviour
{
    public static Vector2 shootDirection;
    public GameObject BulletPrefab;
    public float Speed;
    public float JumpForce;
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private Animator Animator;
    private bool Grounded;
    private float LastShoot;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0)
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        else if (Horizontal > 0.0f)
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Animator.SetBool("running", Horizontal != 0.0f);

        Debug.DrawRay(transform.position, Vector3.down * 1.0f, Color.red);

        if (Physics2D.Raycast(transform.position, Vector3.down, 1.0f))
            Grounded = true;
        else
            Grounded = false;

        if (Input.GetKeyDown(KeyCode.W) && Grounded)
            Jump();

        // Cambiado de KeyCode.Space a Input.GetMouseButtonDown(0)
        if (!Animator.GetBool("running") && Input.GetMouseButtonDown(0) && Time.time > LastShoot + 0.25f && Grounded)
        {
            Shoot();
            LastShoot = Time.time;
            Animator.SetTrigger("disparando");
            StartCoroutine(ResetShootTrigger());
        }
    }

    private IEnumerator ResetShootTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        Animator.ResetTrigger("disparando");
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        shootDirection = (transform.localScale.x == -1.0f) ? Vector2.left : Vector2.right;
        Debug.Log("Shoot Direction: " + shootDirection);

        float offsetX = (shootDirection.x < 0) ? -0.6f : 0.6f;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + new Vector3(offsetX, 0.2f, 0f), Quaternion.identity);
        bullet.transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);

        Debug.Log("Bullet Created: " + bullet);

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        if (bulletScript != null)
        {
            // Ajusta la velocidad aquí
            bulletScript.SetSpeed(10.0f);  // Ajusta el valor según tus preferencias

            if (bulletScript.GetDirection() != Vector2.zero)
            {
                Debug.Log("Bullet Direction: " + bulletScript.GetDirection());
            }
        }

        Destroy(bullet, 2.0f);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);

    }
}
