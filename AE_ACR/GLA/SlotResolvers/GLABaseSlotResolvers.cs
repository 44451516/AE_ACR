﻿#region

using AE_ACR.Base;

#endregion

namespace AE_ACR.GLA.SlotResolvers;

public abstract class GLABaseSlotResolvers : TankBaseIslotResolver
{
    public const uint
        先锋剑FastBlade = 9,
        暴乱剑RiotBlade = 15,
        战女神之怒RageOfHalone = 21,
        厄运流转CircleOfScorn = 23,
        深奥之灵SpiritsWithin = 29,
        投盾ShieldLob = 24,
        战逃反应FightOrFlight = 20,
        全蚀斩TotalEclipse = 7381,
        预警 = 17,
        钢铁信念 = 28,
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