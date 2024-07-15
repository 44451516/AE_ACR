#region

using AE_ACR.Base;
using AE_ACR.utils;

#endregion

namespace AE_ACR.GLA.SlotResolvers;

public abstract class ASTBaseSlotResolvers : 治疗BaseIslotResolver
{
    public const uint
        落陷凶星 = 25871,
        AOE = 25872,
        群奶 = 3600,
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
        Play3 = 37021,
        Play4 = 37022,
        抽卡 = 37017,
        抽卡2 = 37018,
        
        留空 = 0;

    public static class Buffs
    {
        public const ushort
            FightOrFlight = 76,
            预警 = 74,
            钢铁信念 = 79,
            留空 = 0;
    }
}