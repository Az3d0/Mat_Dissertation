using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D m_rb;

    [Header("Slide")]
    [SerializeField] direction m_slideDirection;
    [SerializeField] float m_slideSpeed = 1;
    Vector2 m_directionVector;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        ConvertToVector(m_slideDirection);

    }
    private void Start()
    {
        m_rb.linearVelocity = m_directionVector * m_slideSpeed * 0.02f;
    }

    private void ConvertToVector(direction dir)
    {
        switch(dir){
            case direction.Left:
                m_directionVector = new Vector2(-1, 0);
                break;
            case direction.Right:
                m_directionVector = new Vector2(1, 0);
                break;
            case direction.Up:
                m_directionVector = new Vector2(0,1);
                break;
            case direction.Down:
                m_directionVector = new Vector2(0, -1);
                break;

        }
    }

    private void OnValidate()
    {
        m_rb = GetComponent<Rigidbody2D>();
        ConvertToVector(m_slideDirection);
        m_rb.linearVelocity = m_directionVector * m_slideSpeed * 0.02f;
    }
    [Serializable] enum direction {
        Left,
        Right,
        Up,
        Down
    }
}
