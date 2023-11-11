using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Leslie;
    private float targetX;
    private float targetY;

    private float posX;
    private float posY;

    public float derechaMax;
    public float izquierdaMax;
    public float alturaMax;
    public float alturaMin;

    public float speed;
    public bool encendida = true;

    private void Awake()
    {
        posX = targetX + derechaMax;
        posY = targetY + alturaMax;
        transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), 1);
    }
    void Move_Cam()
    {
        if (encendida)
        {
            if (Leslie)
            {
                targetX = Leslie.transform.position.x;
                targetY = Leslie.transform.position.y;
                if (targetX > derechaMax && targetX < izquierdaMax)
                {
                    posX = targetX;
                }
                if (targetY < alturaMax && targetY > alturaMin)
                {
                    posY = targetY;
                }
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(posX, posY, -1), speed + Time.deltaTime);

        }
    }
    void Update()
    {
        Move_Cam();
    }
}
