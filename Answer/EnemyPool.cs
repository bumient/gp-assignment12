using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    public EnemyPool<GameObject> pool;

    public void Awake()
    {
        pool = new EnemyPool<GameObject>(
            () => Instantiate(prefab, transform)
            , CreateEnemy
            , OnGet
            , OnRelease
            , OnObjectDestroyed
            , true
            , 100
            , 10000
            );

        Prewarm();
    }

    private GameObject CreateEnemy()
    {
        return Instantiate(prefab);
    }

    private void OnGet(GameObject enemy)
    {
        enemy.SetActive(true);
    }

    private void OnRelease(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    private void Prewarm()
    {
        GameObject[] temp = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            temp[i] = pool.Get();
        }
        for (int i = 0; i < poolSize; i++)
        {
            pool.Release(temp[i]);
        }
    }

    public GameObject SpawnEnemy(Vector3 position)
    {
        GameObject enemy = pool.Get();
        enemy.transform.position = position;
        return enemy;
    }

    public void ReleaseEnemy(GameObject enemy)
    {
        pool.ReleaseEnemy(enemy)
    }
}


