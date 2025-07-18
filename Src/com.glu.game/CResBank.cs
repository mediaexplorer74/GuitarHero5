// Decompiled with JetBrains decompiler
// Type: com.glu.game.CResBank
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;
using System;

#nullable disable
namespace com.glu.game
{
  public class CResBank
  {
    public static uint kResMaxTimePerUpdateMS = 100;
    protected string m_keysetId;
    protected CKeysetResource m_pKeyset;
    protected uint m_idx;
    protected uint m_count;
    protected CResBank.eResBankState m_state;

    public CResBank()
    {
      this.m_keysetId = (string) null;
      this.m_pKeyset = (CKeysetResource) null;
      this.m_idx = 0U;
      this.m_count = 0U;
      this.m_state = CResBank.eResBankState.BANK_LOAD_IDLE;
    }

    public void Create(string keysetId)
    {
      this.Release();
      this.m_state = CResBank.eResBankState.BANK_LOAD_FAILURE;
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      int resource2 = (int) resourceManager.CreateResource(keysetId, out resource1);
      if (resource1 == null)
        return;
      this.m_keysetId = keysetId;
      this.m_pKeyset = (CKeysetResource) resource1.GetData();
      this.m_count = this.m_pKeyset.GetCount();
      this.m_state = CResBank.eResBankState.BANK_LOAD_IN_PROGRESS;
    }

    public void Release()
    {
      if (this.m_keysetId != null)
      {
        CResourceManager resourceManager = CApp.GetResourceManager();
        for (uint idx = 0; idx < this.m_idx; ++idx)
          resourceManager.ReleaseResource(this.m_pKeyset.GetKey(idx));
        resourceManager.ReleaseResource(this.m_keysetId);
        this.m_keysetId = "";
      }
      this.m_keysetId = (string) null;
      this.m_pKeyset = (CKeysetResource) null;
      this.m_idx = 0U;
      this.m_count = 0U;
      this.m_state = CResBank.eResBankState.BANK_LOAD_IDLE;
    }

    public CResBank.eResBankState GetState() => this.m_state;

    private uint GetIdx() => this.m_idx;

    private uint GetCount() => this.m_count;

    public uint GetProgressPercent()
    {
      uint progressPercent = 100;
      if (this.m_state == CResBank.eResBankState.BANK_LOAD_IN_PROGRESS)
        progressPercent = this.m_idx * 100U / this.m_count;
      return progressPercent;
    }

    public void HandleUpdate(int timeElapsedMS)
    {
      CResourceManager resourceManager = CApp.GetResourceManager();
      CResource resource1 = (CResource) null;
      DateTime now = DateTime.Now;
      while (this.m_state == CResBank.eResBankState.BANK_LOAD_IN_PROGRESS && (DateTime.Now - now).TotalMilliseconds < (double) CResBank.kResMaxTimePerUpdateMS)
      {
        int resource2 = (int) resourceManager.CreateResource(this.m_pKeyset.GetKey(this.m_idx++), out resource1);
        if (resource1 != null)
        {
          if (this.m_idx >= this.m_count)
            this.m_state = CResBank.eResBankState.BANK_LOAD_SUCCESS;
        }
        else
          this.m_state = CResBank.eResBankState.BANK_LOAD_FAILURE;
      }
    }

    public enum eResBankState
    {
      BANK_LOAD_IDLE,
      BANK_LOAD_IN_PROGRESS,
      BANK_LOAD_FAILURE,
      BANK_LOAD_SUCCESS,
    }
  }
}
