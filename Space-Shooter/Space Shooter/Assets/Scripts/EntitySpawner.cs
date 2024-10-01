using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField]
        private Entity[] m_EntityPrefbs;

        [SerializeField]
        private CircleArea m_Area;

        [SerializeField]
        private SpawnMode m_SpawnMode;

        [SerializeField]
        private int m_NumSpawns;

        [SerializeField]
        private float m_RespawnTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawwnEntities();
            }

            m_Timer = m_RespawnTime;
        }

        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                SpawwnEntities();

                m_Timer = m_RespawnTime;
            }
        }

        private void SpawwnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                int index = Random.Range(0, m_EntityPrefbs.Length);

                GameObject e = Instantiate(m_EntityPrefbs[index].gameObject);

                e.transform.position = m_Area.GetRandomInsideZone();
            }
        }
    }
}