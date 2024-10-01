using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if(m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles?.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField]
        private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField]
        private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;

        #endregion

        #region Score

        [SerializeField]
        private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion
    }
}