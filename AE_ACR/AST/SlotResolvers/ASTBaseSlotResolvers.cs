#region

using AE_ACR.Base;
using AE_ACR.utils;
using AEAssist;

#endregion

namespace AE_ACR.GLA.SlotResolvers;

public abstract class ASTBaseSlotResolvers : 治疗BaseIslotResolver
{
    public const uint
        // 落陷凶星1 = 3596,
        // dot1 = 3599,
        // AOE1 = 3615,
        落陷凶星 = 3596,
        dot = 3599,
        AOE = 3615,
        阳星 = 3600,
        阳星相位 = 3601,
        先天禀赋 = 3614,
        天星冲日 = 16553,
        天星交错 = 16556,
        天宫图 = 16557,
        大宇宙 = 25874,
        擢升 = 25873,
        命运之轮 = 3613,
        占卜 = 16552,
        光速 = 3606,
        福星 = 3610,
        地星 = 7439,
        Play1 = 37019,
        Play2 = 37020,
        生辰 = 3606,
        Play3 = 37021,
        Play4 = 37022,
        抽卡 = 37017,
        抽卡2 = 37018,
        王冠之领主 = 7444,
        王冠之贵妇 = 7445,
        近战卡 = 37023,
        远程卡 = 37026,
        中间学派 = 16559,
        留空 = 0;

    public static class Buffs
    {
        public const ushort
            FightOrFlight = 76,
            预警 = 74,
            钢铁信念 = 79,
            医技buff = 836,
            医技buff2 = 3894,
            留空 = 0;
    }


    public static bool 是否停手()
    {

        if (HasEffect(DeBuffs.加速器炸弹))
        {
            if (GetBuffRemainingTime(DeBuffs.加速器炸弹) > 0 && GetBuffRemainingTime(DeBuffs.加速器炸弹) < 3)
            {
                return true;
            }
        }


        return false;

    }
}