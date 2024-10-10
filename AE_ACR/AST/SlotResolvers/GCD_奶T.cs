#region

using AE_ACR.GLA.SlotResolvers;
using AE_ACR.utils;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.Extension;
using AEAssist.Helper;

#endregion

namespace AE_ACR.AST.SlotResolvers;

public class GCD_奶T : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (是否停手())
        {
            return Flag_停手;
        }

        if (吉星.MyIsUnlock() == false)
        {
            return Flag_没有解锁;
        }

        var 目标 = PartyHelper.CastableAlliesWithin30 //周围30米
            .Where(r => r.CurrentHp > 0 && r.IsTank() && r.CurrentHpPercent() <= 0.45f).OrderBy(r => r.CurrentHpPercent()) //排序
            .FirstOrDefault();

        if (目标 != null && 目标.IsValid())
        {
            return 0;
        }


        return -1;
    }

    public override void Build(Slot slot)
    {
        //对T的目标设置
        var 目标 = PartyHelper.CastableAlliesWithin30.Where(r => r.CurrentHp > 0 && r.IsTank()).OrderBy(r => r.CurrentHpPercent()).FirstOrDefault();

        if (目标 != null && 目标.IsValid())
        {
            if (福星.OriginalHook().MyIsUnlock())
            {
                slot.Add(new Spell(福星, 目标));
            }
            else
            {
                slot.Add(new Spell(吉星, 目标));
            }
        }

    }
}