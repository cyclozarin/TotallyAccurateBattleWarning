using HarmonyLib;
using TotallyAccurateBattleWarning.Behaviours;

namespace TotallyAccurateBattleWarning.Patches;

[HarmonyPatch(typeof(ItemInstanceData))]
public class ItemInstanceDataPatch
{
    // "borrowed" from TKTC
    [HarmonyPatch(nameof(ItemInstanceData.GetEntryType))]
    [HarmonyPrefix]
    private static bool IDToEntryData(byte identifier, ref ItemDataEntry __result)
    {
        if (identifier != 69) return true;
        
        __result = new AmmoEntry();
        return false;
    }

    [HarmonyPatch(nameof(ItemInstanceData.GetEntryIdentifier))]
    [HarmonyPrefix]
    private static bool EntryDataToID(Type type, ref byte __result)
    {
        if (type != typeof(AmmoEntry)) return true;

        __result = 69;
        return false;
    }
}