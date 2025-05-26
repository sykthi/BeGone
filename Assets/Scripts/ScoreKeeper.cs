using UnityEngine;
using TMPro;
public class ScoreKeeper : MonoBehaviour
{
    int score;
    TMP_Text ScoreText;

    void Start()
    {
        ScoreText = GetComponent<TMP_Text>();
        ScoreText.text = "Avoid Collision";
    }
    public void IncreaseScore(int amount)
    {
        score += amount;
        ScoreText.text = score.ToString();
    }
}
