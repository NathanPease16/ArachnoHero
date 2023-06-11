using UnityEngine;

public class Energy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float maxEnergy;
    [SerializeField] public Vector3 respawnPosition;

    [Header("States")]
    [SerializeField]  float currentEnergy;
    [SerializeField]  bool hasFuse;

    [Header("References")]
    private new Transform transform;

    public float MaxEnergy { get {return maxEnergy; } }
    public float CurrentEnergy { get { return currentEnergy; } }
    public bool HasFuse { get { return hasFuse; } set { hasFuse = value; } }

    public delegate void EnergyAmountUpdate();
    public event EnergyAmountUpdate updateEnergy;

    void Awake()
    {
        currentEnergy = maxEnergy;
    }

    void Start()
    {
        transform = GetComponent<Transform>();
        respawnPosition = transform.position;
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.R)) {
        //    transform.position = respawnPosition;
        //}
    }

    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;

        if (currentEnergy < 0)
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
