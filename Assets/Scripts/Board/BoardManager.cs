using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private Transform[] diceSlots;
    [SerializeField] private DiceUnit dicePrefab;
    [SerializeField] private DiceData defaultDiceData;
    [SerializeField] private int autoSpawnCount = 2;

    private void Start()
    {
        int count = Mathf.Min(autoSpawnCount, diceSlots.Length);
        for (int i = 0; i < count; i++)
        {
            PlaceDice(i);
        }
    }

    public void PlaceDice(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= diceSlots.Length || dicePrefab == null)
        {
            return;
        }

        Transform slot = diceSlots[slotIndex];
        if (slot.childCount > 0)
        {
            return;
        }

        DiceUnit dice = Instantiate(dicePrefab, slot.position, Quaternion.identity, slot);
        dice.Initialize(defaultDiceData);
    }
}
