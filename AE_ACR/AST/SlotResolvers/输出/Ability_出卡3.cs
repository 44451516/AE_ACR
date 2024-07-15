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

public class Ability_出卡3 : ASTBaseSlotResolvers
{
    public override int Check()
    {
        if (Play3.OriginalHook().Id == Play3)
        {
            return -1;
        }

        if (CanWeave())
        {
            if (Play3.OriginalHookActionReady())
            {
                return 0;
            }
        }
        return -1;
    }


    public override void Build(Slot slot)
    {
        //对T的目标设置
        var 技能目标对T = PartyHelper.CastableAlliesWithin30.Where(r => r.CurrentHp > 0 && r.IsTank()).OrderBy(r => r.CurrentHpPercent()).FirstOrDefault();
        slot.Add(new Spell(Play3.OriginalHook().Id, 技能目标对T));
        
    }
}