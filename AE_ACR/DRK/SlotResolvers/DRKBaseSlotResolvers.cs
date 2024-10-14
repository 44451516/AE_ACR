#region

using AE_ACR_DRK;
using AE_ACR_DRK_Setting;
using AE_ACR.Base;

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
        腐秽黑暗 = 25755,
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
        深恶痛绝 = 3629,
        暗黑布道 = 16471,
        伤残 = 3624;

    public static bool 是否停手()
    {
        if (是否全局停手())
        {
            return true;
        }
        
        if (是否日常模式())
        {
            if (HasEffect(DeBuffs.加速器炸弹))
            {
                if (GetBuffRemainingTime(DeBuffs.加速器炸弹) > 0 && GetBuffRemainingTime(DeBuffs.加速器炸弹) < 3)
                {
                    return true;
                }
            }  
        }

      
        
        return getQTValue(BaseQTKey.停手);
    }

    public static bool 是否减伤()
    {
        return DKSettings.Instance.日常模式;
    }

    public static bool 是否日常模式()
    {
        return DKSettings.Instance.日常模式;
    }

    public static bool getQTValue(string key)
    {
        return DRKRotationEntry.QT.GetQt(key);
    }


    public static class Buffs
    {
        public const ushort
            //嗜血
            嗜血BloodWeapon = 742,
            Darkside = 751,
            BlackestNight = 1178,
            //血乱
            血乱Delirium1 = 1972,
            血乱Delirium2 = 3836,
            Scorn = 3837,
            腐秽黑暗 = 749,
            SaltedEarth = 749,
            出生入死 = 3255,
            暗影墙 = 747,
            暗影墙v2 = 747,
            献奉 = 2682,
            弃明投暗 = 746,
            深恶痛绝 = 743,
            留空 = 0;
    }
}