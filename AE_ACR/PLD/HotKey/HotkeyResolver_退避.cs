﻿using System.Numerics;
using AE_ACR.PLD.SlotResolvers;
using AEAssist;
using AEAssist.CombatRoutine;
using AEAssist.CombatRoutine.Module;
using AEAssist.CombatRoutine.View.JobView;
using AEAssist.Helper;
using AEAssist.MemoryApi;
using Dalamud.Interface.Textures.TextureWraps;
using ImGuiNET;

namespace AE_ACR.PLD.HotKey;

public class HotkeyResolver_退避:IHotkeyResolver
{
    public void Draw(Vector2 size)
    {

        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);
        if (Core.Resolve<MemApiIcon>().GetActionTexture(PLDBaseSlotResolvers.退避, out IDalamudTextureWrap textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }


    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(PLDBaseSlotResolvers.退避.GetSpell(), size, isActive);
    }

    public int Check()
    {
        if (PLDBaseSlotResolvers.退避.GetSpell().IsReadyWithCanCast())
        {
            return 0;
        }
        
        return -1;
    }

    public void Run()
    {
        if (PLDBaseSlotResolvers.退避.GetSpell().IsReadyWithCanCast())
        {
            if (AI.Instance.BattleData.NextSlot == null)
                AI.Instance.BattleData.NextSlot = new Slot();
            
            AI.Instance.BattleData.NextSlot.Add(new Spell(PLDBaseSlotResolvers.退避.GetSpell().Id, SpellTargetType.Pm2));
        }
    }
}