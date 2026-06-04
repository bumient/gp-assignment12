using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize;
    public ProjectilePool<GameObject> pool;

    public void Awake()
    {
        pool = new ProjectilePool<GameObject>(
            () => Instantiate(prefab, transform)
            , CreateProjectile
            , OnGet
            , OnRelease
            , OnObjectDestroyed
            , true
            , 100
            , 10000
            );

        Prewarm();
    }
    private GameObject CreateProjectile()
    {
        return Instantiate(prefab);
    }

    private void OnGet(GameObject projectile)
    {
        projectile.SetActive(true);
    }

    private void OnRelease(GameObject projectile)
    {
        projectile.SetActive(false);
    }

    private void OnDestroyProjectile(GameObject projectile)
    {
        Destroy(projectile);
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

    public GameObject SpawnProjectile(Vector3 position)
    {
        GameObject projectile = pool.Get();
        projectile.transform.position = position;
        return projectile;
    }

    public void ReleaseEnemy(GameObject projectile)
    {
        pool.ReleaseEnemy(projectile)
    }
}
