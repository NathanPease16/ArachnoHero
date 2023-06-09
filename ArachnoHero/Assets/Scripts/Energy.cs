using UnityEngine;

public class Energy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxEnergy;

    [Header("States")]
    private float currentEnergy;
    public float CurrentEnergy {get { return currentEnergy; } }

    void Awake()
    {
        currentEnergy = maxEnergy;
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;

        if (amount < 0)
            currentEnergy = 0;
    }

    public void Charge(float amount)
    {
        currentEnergy += amount;

        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;
    }

    public bool HasEnoughEnergy(float amount)
    {
        return amount <= currentEnergy;
    }
}
