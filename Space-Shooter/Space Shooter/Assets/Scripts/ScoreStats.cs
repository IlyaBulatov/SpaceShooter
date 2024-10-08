using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField]
        private Text m_Text;

        private int m_LastScore;

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            if(Player.Instance != null)
            {
                int currentScore = Player.Instance.Score;

                if(m_LastScore != currentScore)
                {
                    m_LastScore = currentScore;

                    m_Text.text = "Score : " + m_LastScore.ToString();
                }
            }
        }
    }
}