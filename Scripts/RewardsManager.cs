using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsManager : MonoBehaviour
{
    public static RewardsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float CalculateRewardAmount(float secondsSurvived)
    {
        float rewardAmount = Mathf.Lerp(0f, 500, Mathf.Pow(secondsSurvived, 2) / Mathf.Pow(600, 2));
        if(secondsSurvived > 600 ) 
            rewardAmount += (secondsSurvived-600)*5f;

        // Debug.Log($"Seconds Survived: {secondsSurvived}, Reward Amount: {rewardAmount}");
        return rewardAmount;
    }
    [ContextMenu("Test Calculate Reward Amount")]
    public void TestCalculateRewardAmount()
    {
       Debug.Log($"Reward Amount at 1 minute survived: {CalculateRewardAmount(60)}");
       Debug.Log($"Reward Amount at 5 minute survived: {CalculateRewardAmount(300)}");
       Debug.Log($"Reward Amount at 10 minutes survived: {CalculateRewardAmount(600)}");
       Debug.Log($"Reward Amount at 15 minutes survived: {CalculateRewardAmount(900)}");
       Debug.Log($"Reward Amount at 20 minutes survived: {CalculateRewardAmount(1200)}");
       Debug.Log($"Reward Amount at 25 minutes survived: {CalculateRewardAmount(1500)}");
    }

}
