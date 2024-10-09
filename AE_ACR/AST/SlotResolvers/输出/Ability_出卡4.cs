#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class Ability_出卡4 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (Play4.OriginalHook().Id == Play4)
        {
            return -1;
        }

        if (CanWeave())
        {
            if (Play4.OriginalHook().Id == 王冠之领主)
            {
                return 0;
            }

            if (Play4.OriginalHook().Id == 王冠之贵妇)
            {
                var 目标 = PartyHelper.CastableAlliesWithin30 //周围30米
                    .Where(r => r.CurrentHp > 0 && !r.IsTank() && r.CurrentHpPercent() <= 0.85f) //且 不具有几个buff 且不具有list中的buff 3秒
                    .OrderBy(r => r.CurrentHpPercent()) //排序
                    .FirstOrDefault();

                if (目标 != null && 目标.IsValid())
                {
                    return 0;
                }

            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        slot.Add(Play4.OriginalHook());
    }
}