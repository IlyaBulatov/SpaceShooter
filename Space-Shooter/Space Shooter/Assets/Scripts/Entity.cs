using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Base class for all interactive objects on scene.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField]
        private string m_Nickname;
        public string Nickname => m_Nickname;

    }
}