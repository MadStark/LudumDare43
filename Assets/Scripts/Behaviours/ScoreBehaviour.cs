using System.Linq;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    public enum TargetValue
    {
        UserScore,
        OpponentScore,
        Sacrifice
    }

    [SerializeField]
    private TMPro.TMP_Text powerText;

    public TargetValue target;

    private void Update()
    {
        string text = null;
        switch (target)
        {
            case TargetValue.UserScore:
                text = GameplayController.Instance.user.points.ToString();
                break;
            case TargetValue.OpponentScore:
                text = GameplayController.Instance.opponent.points.ToString();
                break;
            case TargetValue.Sacrifice:
                text = GameplayController.Instance.sacrifice.ToString();
                break;
        }
        powerText.text = text;
    }
}
