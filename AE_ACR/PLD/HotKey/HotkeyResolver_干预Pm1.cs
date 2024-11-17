﻿using System.Numerics;
using AE_ACR.DRK.SlotResolvers;
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

public class HotkeyResolver_干预Pm2:IHotkeyResolver
{
    public void Draw(Vector2 size)
    {

        var iconSize = size * 0.8f;
        //技能图标
        ImGui.SetCursorPos(size * 0.1f);
        if (Core.Resolve<MemApiIcon>().GetActionTexture(PLDBaseSlotResolvers.干预, out IDalamudTextureWrap textureWrap))
        {
            ImGui.Image(textureWrap.ImGuiHandle, iconSize);
        }
    }


    public void DrawExternal(Vector2 size, bool isActive)
    {
        SpellHelper.DrawSpellInfo(PLDBaseSlotResolvers.干预.GetSpell(), size, isActive);
    }

    public int Check()
    {
        return 0;
    }

    public void Run()
    {
        if (AI.Instance.BattleData.NextSlot == null)
            AI.Instance.BattleData.NextSlot = new Slot();
        
        if (PLDBaseSlotResolvers.干预.GetSpell().IsReadyWithCanCast())
            AI.Instance.BattleData.NextSlot.Add(new Spell(PLDBaseSlotResolvers.干预.GetSpell().Id, SpellTargetType.Pm2));
    }
}