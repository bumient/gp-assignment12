using UnityEngine;

namespace Answer.Pooling
{
    // Dummy implementation to exercise both pools.
    public class DummyScript : MonoBehaviour
    {
        private EnemyPool enemyPool;
        private ProjectilePool projectilePool;

        private void Awake()
        {
            enemyPool = FindAnyObjectByType<EnemyPool>();
            projectilePool = FindAnyObjectByType<ProjectilePool>();
        }

        public GameObject SpawnEnemy()
        {
            return enemyPool != null ? enemyPool.pool.Get() : null;
        }

        public GameObject SpawnProjectile()
        {
            return projectilePool != null ? projectilePool.pool.Get() : null;
        }
    }
}