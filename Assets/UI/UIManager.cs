using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
        [SerializeField] private Text scoreText;
        [SerializeField] private GameObject restartObjects;
        [SerializeField] private Text restartScoreText;

        public void ShowRestart()
        {
            restartObjects.SetActive(true);
        }

        public void IncreaseScoreText(int score)
        {
            scoreText.text = score.ToString();
            restartScoreText.text = score.ToString();
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
}