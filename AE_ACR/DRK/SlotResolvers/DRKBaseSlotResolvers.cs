#region

using AE_ACR_DRK;
using AE_ACR.Base;
using AE_ACR.PLD;
using AE_ACR.utils;

#endregion

namespace AE_ACR.DRK.SlotResolvers;

public abstract class DRKBaseSlotResolvers : TankBaseIslotResolver
{
    public const uint
        //单体连击1
        单体1HardSlash = 3617,
        释放Unleash = 3621,
        //吸收斩
        单体2SyphonStrike = 3623,
        单体3Souleater = 3632,
        腐秽大地SaltedEarth = 3639,
        AbyssalDrain = 3641,
        精雕怒斩CarveAndSpit = 3643,
        //血乱
        血乱Delirium = 7390,
        寂灭Quietus = 7391,
        //Bloodspiller
        血溅Bloodspiller = 7392,
        血溅3 = 36930,
        血溅2 = 36929,
        血溅1 = 36928,
        暗黑锋 = 16467,
        暗黑波动AOE = 16467,
        刚魂StalwartSoul = 16468,
        FloodOfShadow = 16469,
        EdgeOfShadow = 16470,
        //弗雷
        LivingShadow = 16472,
        蔑视厌恶Disesteem = 36932,
        SaltAndDarkness = 25755,
        Oblation = 25754,
        Shadowbringer暗影使者 = 25757,
        // Shadowbringer = 29738,
        Plunge = 3640,
        //BloodWeapon
        嗜血BloodWeapon = 3625,
        疾跑 = 3,
        弃明投暗 = 3634,
        暗影墙 = 3636,
        至黑之夜 = 7393,
        行尸走肉 = 3636,
        献奉 = 25754,
        Unmend = 3624;


    public static class Buffs
    {
        public const ushort
            //嗜血
            嗜血BloodWeapon = 742,
            Darkside = 751,
            BlackestNight = 1178,
            //血乱
            血乱Delirium = 3836,
            Scorn = 3837,
            SaltedEarth = 749,
            出生入死 = 3255,
            暗影墙 = 747,
            暗影墙v2 = 747,
            献奉 = 2682,
            弃明投暗 = 746,
            留空 = 0;
    }

    public static bool 是否停手()
    {
        return getQTValue(BaseQTKey.停手);
    }

    public static bool 是否减伤()
    {
        return getQTValue(BaseQTKey.减伤);
    }

    public static bool getQTValue(string key)
    {
        return DRKRotationEntry.QT.GetQt(key);
    }
}