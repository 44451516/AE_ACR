#region

using AEAssist;
using AEAssist.Extension;
using Dalamud.Game.ClientState.Objects.Types;

#endregion

namespace AE_ACR.utils;

public class RaidBuff
{
    public const ushort

        // Heals
        强化药 = 49,
        灼热之光 = 2703,
        连环计 = 1221,
        星空 = 3685,
        占卜 = 1878,
        义结金兰 = 1185,
        战斗连祷 = 786,
        战斗之声 = 141,

        //忍者夺取
        攻其不备 = 3254,
        罐毒之术 = 3849,
        受伤加重 = 638,
        神秘纹 = 2599,
        技巧舞步结束TechnicalFinish = 1822,
        留空 = 0;


    public static bool 爆发期_120()
    {
        //小于20f避免早打，反正爆发期很长
        var 强化药BUFF = 强化药.GetBuffRemainingTime() / 1000f;
        if (强化药BUFF is > 0 and < 20f)
            return true;

        if (Core.Me.HasAura(灼热之光))
            return true;
        
        if (Core.Me.HasAura(战斗之声))
            return true;    
        
        if (Core.Me.HasAura(神秘纹))
            return true;

        if (Core.Me.HasAura(技巧舞步结束TechnicalFinish))
            return true;

        if (Core.Me.HasAura(星空))
            return true;

        if (Core.Me.HasAura(占卜))
            return true;

        if (Core.Me.HasAura(义结金兰))
            return true;

        if (Core.Me.HasAura(战斗连祷))
            return true;

        if (Core.Me.TargetObject is IBattleChara battleChara)
        {
            if (battleChara.HasAura(攻其不备))
                return true;

            if (battleChara.HasAura(连环计))
                return true;

            if (battleChara.HasAura(罐毒之术))
                return true;
        }

        return false;
    }
}