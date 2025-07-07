using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("파이프 생성 설정")]
    public GameObject pipePairPrefab;
    public float spawnInterval = 2f;
    public float xSpawnPosition = 10f;

    [Header("파이프 갭 설정")]
    public float minGapHeight = 2f;
    public float maxGapHeight = 4f;

    [Header("속도 증가 설정")]
    public float pipeMoveSpeed = 2f;
    public float speedUpInterval = 5f;
    public float speedUpAmount = 1.5f;
    public float maxSpeed = 8f;

    [Header("생성 간격 설정")]
    public float minSpawnInterval = 0.8f;

    private float lastSpeedUpTime = 0f;

    public Bird bird;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating(nameof(SpawnPipes), 0f, spawnInterval);
        if (bird == null)
            bird = FindFirstObjectByType<Bird>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bird != null && bird.isLive)
        {
            if (Time.time - lastSpeedUpTime > speedUpInterval && pipeMoveSpeed < maxSpeed)
            {
                pipeMoveSpeed = Mathf.Min(pipeMoveSpeed + speedUpAmount, maxSpeed);
                lastSpeedUpTime = Time.time;
                spawnInterval = Mathf.Max(spawnInterval * 0.9f, minSpawnInterval);
                CancelInvoke(nameof(SpawnPipes));
                InvokeRepeating(nameof(SpawnPipes), spawnInterval, spawnInterval);
            }
        }

        if (bird != null && !bird.isLive)
        {
            pipeMoveSpeed = 0f;
            CancelInvoke(nameof(SpawnPipes));
        }
    }

    void SpawnPipes()
    {
        float gap = Random.Range(minGapHeight, maxGapHeight);
        float halfGap = gap / 2f;

        float camHalfHeight = Camera.main.orthographicSize;

        Transform pipeSprite = pipePairPrefab.transform.Find("PipeTop");
        float pipeHalfHeight = 0f;
        if (pipeSprite != null && pipeSprite.GetComponent<SpriteRenderer>() != null)
            pipeHalfHeight = pipeSprite.GetComponent<SpriteRenderer>().bounds.size.y / 2f;

        float maxCenterY = camHalfHeight - halfGap - pipeHalfHeight;
        float minCenterY = -camHalfHeight + halfGap + pipeHalfHeight;
        float centerY = Random.Range(minCenterY, maxCenterY);
        Debug.Log($"파이프 중앙 Y 위치 : {centerY}");

        GameObject pipePair = Instantiate(pipePairPrefab, new Vector3(xSpawnPosition, 0f, 0f), Quaternion.identity);

        Transform topPipe = pipePair.transform.Find("PipeTop");
        Transform bottomPipe = pipePair.transform.Find("PipeBottom");
        if (topPipe != null && bottomPipe != null)
        {
            topPipe.localPosition = new Vector3(0, centerY + halfGap, 0);
            bottomPipe.localPosition = new Vector3(0, centerY - halfGap, 0);
        }

    }
}
