using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterContr : MonoBehaviour
{
    //Скорость ходьбы
    public float speed = 2.0f;
    //Переменная движения
    private Vector3 move = Vector3.zero;
    //Гравитация
    public float gravity = 10.0f;
    //Содержание CharacterContr
    private CharacterController controller;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            move = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            move = transform.TransformDirection(move);
            move *= speed;
        }

        move.y -= gravity * Time.deltaTime;
        controller.Move(move * Time.deltaTime);
    }
}
