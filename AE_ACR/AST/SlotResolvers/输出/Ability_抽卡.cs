#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_抽卡 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        // if (Play4.OriginalHook().Id == Play4)
        // {
        //     return -1;
        // }

        if (CanWeave())
        {
            if (抽卡.OriginalHookActionReady())
            {
                return 0;
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(抽卡.OriginalHook());
    }
}