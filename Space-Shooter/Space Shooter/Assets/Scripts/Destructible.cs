using UnityEngine;

namespace SpaceShooter
{
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Object will ignore damage.
        /// </summary>
        [SerializeField]
        private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Start count of HP.
        /// </summary>
        [SerializeField]
        private int m_HitPoints;

        /// <summary>
        /// Current HP count.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        #endregion

        #region Unity Events

        /// <summary>
        /// Assign current count to HP.
        /// </summary>
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }

        #endregion

        #region Public API

        /// <summary>
        /// Applying damage to object
        /// </summary>
        /// <param name="damage"> Damage done to a object. </param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
                OnDeath();
        }

        #endregion

        #region Methods & Events

        /// <summary>
        /// Virtual destroy object event, when HP reaches zero.
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }

        #endregion
    }
}