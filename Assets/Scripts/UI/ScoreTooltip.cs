using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ScoreTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public TextMeshProUGUI tooltipText;

    private void OnEnable()
    {
        tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager gm = GameManager.Instance;

        int killPoints = gm.Kills * gm.pointsPerKill;
        int timePoints = gm.SurvivedSeconds * gm.pointsPerSecond;

        tooltipText.text =
            "<b>How score works</b>\n" +
            $"Each enemy defeated: <color=#FFD54A>{gm.pointsPerKill} {Pts(gm.pointsPerKill)}</color>\n" +
            $"Each second survived: <color=#FFD54A>{gm.pointsPerSecond} {Pts(gm.pointsPerSecond)}</color>\n" +
            "\n" +
            $"Enemies<pos=5em>{gm.Kills} x {gm.pointsPerKill}<pos=10em>= {killPoints}\n" +
$"Time<pos=5em>{gm.SurvivedSeconds}s x {gm.pointsPerSecond}<pos=10em>= {timePoints}\n" +
$"<b>Total<pos=10em>= {gm.Score}</b>";

        tooltip.SetActive(true);
    }

    string Pts(int value) => value == 1 ? "pt" : "pts";

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}