using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    public int coins = 0;
    public TMP_Text coinText;
    public Transform TargetPos;
    public float duration = 1.0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            coins++;
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayCoinSound();

            }
            Destroy(other.gameObject);

            UpdateCoinText();
        }
    }

    public void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "Coins : " + coins.ToString();
        }
    }
}
