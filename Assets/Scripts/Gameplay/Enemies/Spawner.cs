using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 5f;

    [Space(20)]
    [SerializeField] private UnitFactoryData enemyFactory;
    [SerializeField] private Bounds bounds;

    private float time = 0;

    void Start()
    {
        time = spawnRate;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnRate)
        {
            time -= spawnRate;
            SpawnUnit();
        }
    }

    private void SpawnUnit()
    {
        Unit unit = enemyFactory.CreateUnit(transform);
        unit.transform.position = new Vector3(
            transform.position.x + Random.Range(bounds.min.x, bounds.max.x),
            transform.position.y,
            transform.position.z + Random.Range(bounds.min.z, bounds.max.z));

        // To go deeper, for exemple on a NavMesh, get the nearest valid position
    }



    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.8f);
        Gizmos.DrawWireCube(transform.position, bounds.size);
    }
}
