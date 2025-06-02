using UnityEngine;
using TMPro;
using UnityEngine.Playables;
public class GameManager : MonoBehaviour
{
    [SerializeField] PlayableDirector MasterTimeLine;
    [SerializeField] TMP_Text ScoreText;
    public static bool isTimelineEnded = false;
    public static int score;

    void Start()
    {
        ScoreText.text = "Avoid Collision";
        score = 0;
    }

    void Update()
    {
        if (MasterTimeLine.state == PlayState.Playing)
        {
            if (MasterTimeLine.time >= MasterTimeLine.duration)
            {
                isTimelineEnded = true;
                Time.timeScale = 0f;
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        ScoreText.text ="SCORE: " + score.ToString();
    }
}
