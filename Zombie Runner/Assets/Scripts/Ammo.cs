using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmotSlot[] ammoSlots;

    [System.Serializable]
    private class AmmotSlot
    {
        public AmmoType ammoType;
        public int ammoAmount;
    }
    
    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmotSlot(ammoType).ammoAmount;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmotSlot(ammoType).ammoAmount--;
    }

    public void IncreaseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmotSlot(ammoType).ammoAmount += ammoAmount;
    }

    private AmmotSlot GetAmmotSlot(AmmoType ammoType)
    {
        foreach (AmmotSlot slot in ammoSlots)
        {
            if(slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}
