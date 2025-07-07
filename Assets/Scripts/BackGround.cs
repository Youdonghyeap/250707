using System.Numerics;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Bird bird;

    public float width = 40f;
    public float speed = 3.0f;

    void Start()
    {
        if (bird == null)
            bird = FindFirstObjectByType<Bird>();
    }

    void Update()
    {
        if (bird != null && bird.isLive)
        {
            transform.position += UnityEngine.Vector3.left * speed * Time.deltaTime;

            if (transform.position.x < -width)
            {
                UnityEngine.Vector3 newPosition = transform.position;
                newPosition.x += width * 2f; // 두 배 이동
                transform.position = newPosition;
            }
        }
    }
}
