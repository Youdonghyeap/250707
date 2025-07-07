using UnityEngine;

public class PipeMover : MonoBehaviour
{
    public float destroyX = -20f;
    // Update is called once per frame
    void Update()
    {
        float moveSpeed = FindAnyObjectByType<PipeSpawner>().pipeMoveSpeed;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
