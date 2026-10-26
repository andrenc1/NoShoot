using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerXP : MonoBehaviour
{
    [Header("XP Settings")]
    public int level = 1;
    public int xp;
    public float xpGainMult = 1.0f;    
    public int xpToNextLevel = 10;
    public TextMeshProUGUI timerBar;
    public TextMeshProUGUI levelText;

    [Header("UI Settings")]
    public Image xpBarFillAmount;

    private void Start()
    {
        levelText.text = "Lv. " + level.ToString();
    }

    public void AddXP(int amount)
    {

        amount = Mathf.RoundToInt(amount * xpGainMult);

        xp += amount;
        
        int levelsGained = 0;

        while (xp >= xpToNextLevel)
        {
            xp -= xpToNextLevel;
            level++;
            levelsGained++;

            xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.25f) + 5;
        }

        UpdateXPBar();

        if (levelsGained > 0)
        {
            UpgradeSystem.instace.Open();
        }
    }


    void UpdateXPBar()
    {
        xpBarFillAmount.fillAmount = (float)xp / xpToNextLevel;
        levelText.text = "Lv. " + level.ToString();
    }

    private void Update()
    {
        timerBar.text = GameManager.Instance.GetElapsedTime();
    }
}
