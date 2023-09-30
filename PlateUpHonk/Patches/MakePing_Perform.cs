using HarmonyLib;
using Kitchen;

namespace PlateUpHonk.Patches
{
    [HarmonyPatch(typeof(MakePing), "Perform")]
    internal class MakePing_Perform
    {
        static void Postfix(ref InteractionData data) => HonkPlugin.PlayHonk(data.Attempt.Location);
    }
}
