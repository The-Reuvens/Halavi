using System;
using TMPro;
using UnityEngine;

public enum WeightMode
{
    THIN,
    FAT,
    FATSO,
    NULL
}

enum WeightSetType
{
    GAIN, LOSE
}

public class WeightManager : MonoBehaviour
{
    public float WeightThreshold = 300;
    private WeightMode mode = WeightMode.THIN;
    [SerializeField] private TMP_Text weightText;

    private float weight;
    public float Weight
    {
        get { return weight; }
        set
        {
            WeightSetType weightSetType = value > weight ? WeightSetType.GAIN : WeightSetType.LOSE;

            weight = Mathf.Max(0, value);

            weightText.SetText($"{weight}kg");
            weightText.transform.LeanScale(1.25f * Vector3.one, 0.2f);
            weightText.transform.LeanScale(Vector3.one, 0.1f).delay = 0.3f;

            if (value > WeightThreshold)
            {
                if (weightText.color != Color.red) weightText.color = Color.red;
                return;
            }

            var player = GameManager.Instance.Player;
            float weightPercentage = weight / WeightThreshold;

            player.transform.LeanScale((1 + 0.5f * weightPercentage) * Vector3.one, 0.4f).setEaseInOutBounce();

            SetMode(value switch
            {
                float v when mode == WeightMode.FAT && weightSetType == WeightSetType.LOSE && v < WeightThreshold / 3 => WeightMode.THIN,
                float v when ((mode == WeightMode.THIN && weightSetType == WeightSetType.GAIN) || (mode == WeightMode.FATSO && weightSetType == WeightSetType.LOSE)) && v >= WeightThreshold / 3 && v < WeightThreshold / 3 * 2 => WeightMode.FAT,
                float v when mode == WeightMode.FAT && v >= WeightThreshold / 3 * 2 && v < WeightThreshold => WeightMode.FATSO,
                _ => WeightMode.NULL
            });

            if (weightSetType == WeightSetType.GAIN)
            {
                var playerClone = player.PlayerClone;

                playerClone.transform.localPosition = player.transform.localPosition;
                playerClone.LeanScale(2f * Vector3.one, 1f).setEaseOutExpo();
                LeanTween.alpha(playerClone, 0, 1f).setEaseOutExpo();

            }
        }
    }

    public WeightMode GetMode() => mode;

    private void SetMode(WeightMode mode)
    {
        if (mode == WeightMode.NULL) return;

        if ((int)mode < (int)this.mode)
        {
            //TODO: Play losing weight audio clip
        }
        else
        {
            //TODO: Play gaining weight audio clip
        }

        this.mode = mode;

        weightText.color = mode switch
        {
            WeightMode.THIN => Color.green,
            WeightMode.FAT => Color.yellow,
            WeightMode.FATSO => new Color(1, 0.3488759f, 0),
            _ => Color.black
        };

        var player = GameManager.Instance.Player;
        player.transform.LeanRotateAround(Vector3.up, (OMath.RandomInt(0, 1) == 0 ? -1 : 1) * 360f, 0.8f).setEaseOutElastic();
        player.Animator.Play(Enum.GetName(typeof(WeightMode), mode));
    }
}
