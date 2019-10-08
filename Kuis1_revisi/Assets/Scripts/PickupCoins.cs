using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupCoins : MonoBehaviour
{
    public Text CoinText;
    public int totalCoin = 0;

    void onStart()
    {
        UpdateTotalCOin();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            totalCoin++;
            UpdateTotalCOin();
            Destroy(collision.gameObject);
        }
    }

    private void UpdateTotalCOin()
    {
        CoinText.text = totalCoin.ToString();
    }
}
