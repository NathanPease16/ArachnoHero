using UnityEngine;

public class Energy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxEnergy;

    [Header("States")]
    [SerializeField]  float currentEnergy;

    public float MaxEnergy { get {return maxEnergy; } }
    public float CurrentEnergy { get { return currentEnergy; } }

    public delegate void EnergyAmountUpdate();
    public event EnergyAmountUpdate updateEnergy;

    void Awake()
    {
        currentEnergy = maxEnergy;
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;

        if (amount < 0)
            currentEnergy = 0;

        updateEnergy?.Invoke();
    }

    public void Charge(float amount)
    {
        currentEnergy += amount;

        if (currentEnergy > maxEnergy)
            currentEnergy = maxEnergy;

        updateEnergy?.Invoke();
    }

    public bool HasEnoughEnergy(float amount)
    {
        return amount <= currentEnergy;
    }
}
