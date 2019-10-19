using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : LivingEntity
{
    private float myRemainingAmount = 1.0f;
    private const float myConsumeSpeed = 8;

    public float Consume(float amount)
    {
        float consumedAmount = Mathf.Max(0, Mathf.Min(myRemainingAmount, amount));
        myRemainingAmount -= amount * myConsumeSpeed;

        transform.localScale = Vector3.one * myRemainingAmount;

        if (myRemainingAmount <= 0)
            Die(CauseOfDeath.Eaten);

        return consumedAmount;
    }

    public float RemainingAmount
    {
        get
        {
            return myRemainingAmount;
        }
    }
}