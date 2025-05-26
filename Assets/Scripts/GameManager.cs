using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayableDirector MasterTimeLine;
    [SerializeField] TMP_Text ScoreText;
    public static int score;

    void Start()
    {
        ScoreText.text = "Avoid Collision";
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        ScoreText.text ="SCORE: " + score.ToString();
    }
}
