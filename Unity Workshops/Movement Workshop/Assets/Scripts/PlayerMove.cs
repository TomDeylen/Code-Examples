using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float m_speed = 4f;
    public float m_turnSpeed = 180f;

    float m_forwardInput;
    float m_sideInput;

    float m_rotation;

    Rigidbody m_rigidbody;

    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_forwardInput = Input.GetAxis("Vertical");
        m_sideInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        m_rotation += (m_sideInput * m_turnSpeed * Time.deltaTime);
        if (m_rotation > 360)
            m_rotation -= 360;
        if (m_rotation < 0)
            m_rotation += 360;

        Quaternion turn = Quaternion.Euler(0, m_rotation, 0);
        m_rigidbody.rotation = turn;
        //Move
        Vector3 direction = turn * Vector3.forward * m_speed * m_forwardInput * Time.deltaTime;
        m_rigidbody.MovePosition(m_rigidbody.position + direction);
    }

}
