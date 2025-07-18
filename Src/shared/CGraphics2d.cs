// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CGraphics2d
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

#nullable disable
namespace com.glu.shared
{
  public sealed class CGraphics2d : CGraphics2dAbstract
  {
    private const uint NumOfElementsInNonTransformStateStack = 5;
    public int ang;
    private SpriteEffects[] SpriteEffectsMap_DrawTime = new SpriteEffects[4]
    {
      SpriteEffects.None,
      SpriteEffects.FlipHorizontally,
      SpriteEffects.FlipVertically,
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically
    };
    private SpriteEffects[] SpriteEffectsMap_LoadTime_NoFlip = new SpriteEffects[7]
    {
      SpriteEffects.FlipHorizontally,
      SpriteEffects.FlipVertically,
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
      SpriteEffects.FlipVertically,
      SpriteEffects.None,
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
      SpriteEffects.FlipHorizontally
    };
    private SpriteEffects[] SpriteEffectsMap_LoadTime_HorizontalFlip = new SpriteEffects[7]
    {
      SpriteEffects.None,
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
      SpriteEffects.FlipVertically,
      SpriteEffects.None,
      SpriteEffects.FlipVertically,
      SpriteEffects.FlipHorizontally,
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically
    };
    private SpriteEffects[] SpriteEffectsMap_LoadTime_VerticalFlip = new SpriteEffects[7]
    {
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
      SpriteEffects.None,
      SpriteEffects.FlipHorizontally,
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
      SpriteEffects.FlipHorizontally,
      SpriteEffects.FlipVertically,
      SpriteEffects.None
    };
    private SpriteEffects[] SpriteEffectsMap_LoadTime_HorizontalAndVerticalFlip = new SpriteEffects[7]
    {
      SpriteEffects.FlipVertically,
      SpriteEffects.FlipHorizontally,
      SpriteEffects.None,
      SpriteEffects.FlipHorizontally,
      SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically,
      SpriteEffects.None,
      SpriteEffects.FlipVertically
    };
    private bool m_hasRenderingBegun;
    private CGraphics2dAbstract.AbstractState m_as_hw;
    private TCStack<CGraphics2dAbstract.TransformCondition> m_transform_condition_hw;
    private TCStack<CGraphics2dAbstract.Matrix> m_transform_shadow_hw;
    private CMatrix2d m_cachedOrientationAndScale_hw;
    private CVector2d m_cachedTranslation_hw;
    public bool m_useColorState;
    private bool m_color_xna_is_dirty;
    private Microsoft.Xna.Framework.Color m_color_xna;
    private Microsoft.Xna.Framework.Color m_color_xna_premult;
    private Microsoft.Xna.Framework.Color m_image_draw_color_xna;
    private CRectangle m_clipRect_hw;
    private Rectangle m_activeClipRect_hw;
    private int m_vertexPositionArray_numOfCompsPerVertex_hw;
    private uint m_vertexPositionArray_type_hw;
    private int m_vertexPositionArray_stride_hw;
    private byte[] m_vertexPositionArray_pos_ptr_hw;
    private SpriteBatch m_spriteBatch;
    private SpriteBatch m_currSpriteBatch;
    private RasterizerState[] m_rasterizerState_hw;
    private CGraphics2d.XNARasterizationStateId m_currRasterizerStateId_hw;
    private CGraphics2d.XNARasterizationStateId m_nextRasterizerStateId_hw;
    private SpriteSortMode m_spriteSortMode_hw;
    private BlendState m_blendState_hw;
    private Vector2 m_trans_xna;
    private Vector2 m_scale_xna;
    private Rectangle m_srcClip_xna;
    private bool m_suspendConfigStateBasedOnSrc;
    private CRenderSurface_XNA_OffScreen m_rectSurface;

    public void foo(ref Color.ARGB_fixed vv) => vv.m_b = (int) ushort.MaxValue;

    public override bool Initialize()
    {
      this.m_hasRenderingBegun = false;
      this.m_as_hw = new CGraphics2dAbstract.AbstractState();
      this.m_as_hw.Initialize(5U);
      for (int index = 0; index < 5; ++index)
      {
        this.m_as_hw.m_color.m_ele[index].m_colorfx = Color.ARGB_fixed_t.White;
        this.m_as_hw.m_color.m_ele[index].m_color8 = Color.A8R8G8B8_t.Make(this.m_as_hw.m_color.m_ele[index].m_colorfx.GetAlpha(), this.m_as_hw.m_color.m_ele[index].m_colorfx.GetRed(), this.m_as_hw.m_color.m_ele[index].m_colorfx.GetGreen(), this.m_as_hw.m_color.m_ele[index].m_colorfx.GetBlue());
      }
      this.m_useColorState = false;
      this.m_color_xna_is_dirty = true;
      this.m_image_draw_color_xna = Microsoft.Xna.Framework.Color.White;
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      this.SetClip(0U, 0U, width, height);
      Color.ARGB_fixed red = Color.ARGB_fixed_t.Red;
      this.foo(ref red);
      this.m_transform_condition_hw = new TCStack<CGraphics2dAbstract.TransformCondition>();
      this.m_transform_condition_hw.Initialize(32U);
      this.m_transform_shadow_hw = new TCStack<CGraphics2dAbstract.Matrix>();
      this.m_transform_shadow_hw.Initialize(32U);
      for (int index = 0; (long) index < (long) this.m_transform_shadow_hw.Depth; ++index)
        this.m_transform_shadow_hw.m_ele[index].loadIdentity();
      this.m_cachedOrientationAndScale_hw = new CMatrix2d();
      this.m_cachedTranslation_hw = new CVector2d();
      this.m_rasterizerState_hw = new RasterizerState[2];
      for (int index = 0; index < 2; ++index)
      {
        this.m_rasterizerState_hw[index] = new RasterizerState();
        switch (index)
        {
          case 0:
            this.m_rasterizerState_hw[index].ScissorTestEnable = false;
            this.m_rasterizerState_hw[index].CullMode = CullMode.None;
            break;
          case 1:
            this.m_rasterizerState_hw[index].ScissorTestEnable = true;
            this.m_rasterizerState_hw[index].CullMode = CullMode.None;
            break;
        }
      }
      this.m_currRasterizerStateId_hw = CGraphics2d.XNARasterizationStateId.NoSissor;
      this.m_nextRasterizerStateId_hw = CGraphics2d.XNARasterizationStateId.NoSissor;
      this.m_spriteBatch = new SpriteBatch(((CGraphics) ICGraphics.GetInstance()).GetGraphicsDeviceXNA());
      this.m_currSpriteBatch = (SpriteBatch) null;
      this.m_spriteSortMode_hw = SpriteSortMode.Immediate;
      this.m_blendState_hw = BlendState.AlphaBlend;
      this.m_trans_xna = new Vector2(0.0f, 0.0f);
      this.m_scale_xna = new Vector2(1f, 1f);
      this.m_srcClip_xna = new Rectangle(0, 0, 1, 1);
      this.m_suspendConfigStateBasedOnSrc = false;
      this.m_rectSurface = new CRenderSurface_XNA_OffScreen();
      Color.Format format = Color.Format.B5G6R5;
      ICRenderSurface.Param[] objArray = new ICRenderSurface.Param[5];
      objArray[0].m_id = ICRenderSurface.ParamId.ColorBufferFormat;
      objArray[0].m_val = (object) format;
      objArray[1].m_id = ICRenderSurface.ParamId.Width;
      objArray[1].m_val = (object) 1U;
      objArray[2].m_id = ICRenderSurface.ParamId.Height;
      objArray[2].m_val = (object) 1U;
      objArray[3].m_id = ICRenderSurface.ParamId.Unknown;
      this.m_rectSurface.Initialize(objArray);
      return true;
    }

    public override void SetActiveAbstractionLayer(ICGraphics.Abstraction abstraction)
    {
    }

    public override ICGraphics.Abstraction GetActiveAbstractionLayer()
    {
      return ICGraphics.Abstraction.Hardware;
    }

    public override void RenderBegin()
    {
      uint width;
      uint height;
      ICGraphics.GetInstance().GetTargetSurface().GetWidthAndHeight(out width, out height);
      this.m_clipRect_hw.Set(0, 0, (int) width, (int) height);
      this.m_activeClipRect_hw.X = 0;
      this.m_activeClipRect_hw.Y = 0;
      this.m_activeClipRect_hw.Width = (int) width;
      this.m_activeClipRect_hw.Height = (int) height;
      this.LoadIdentity();
      this.m_currSpriteBatch = (SpriteBatch) null;
      this.m_hasRenderingBegun = true;
    }

    public override void RenderEnd()
    {
      if (this.m_currSpriteBatch != null)
      {
        this.m_currSpriteBatch.End();
        this.m_currSpriteBatch = (SpriteBatch) null;
      }
      this.m_hasRenderingBegun = false;
    }

    public override void SetColor(int a, int r, int g, int b)
    {
      uint index = this.m_as_hw.m_color.m_ptr - 1U;
      this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.SetFixed(a, r, g, b);
      this.m_as_hw.m_color.m_ele[(int) index].m_color8 
                = Color.A8R8G8B8_t.Make(this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetAlpha(), 
                this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetRed(), this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetGreen(), this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetBlue());
      this.m_color_xna_is_dirty = true;
    }

    public override void SetColor(Color.ARGB_fixed color)
    {
      uint index = this.m_as_hw.m_color.m_ptr - 1U;
      this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.Set(color);
      this.m_as_hw.m_color.m_ele[(int) index].m_color8 = Color.A8R8G8B8_t.Make(this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetAlpha(),
          this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetRed(), this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetGreen(), this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.GetBlue());
      this.m_color_xna_is_dirty = true;
    }

    public override void SetColor(uint color)
    {
      uint index = this.m_as_hw.m_color.m_ptr - 1U;
      this.m_as_hw.m_color.m_ele[(int) index].m_color8 = color;
      this.m_as_hw.m_color.m_ele[(int) index].m_colorfx.Make(Color.Format.A8R8G8B8, (object) color);
      this.m_color_xna_is_dirty = true;
    }

    public override void Enable(ICGraphics2d.State state)
    {
      switch (state)
      {
        case ICGraphics2d.State.AlphaTest:
          this.m_as_hw.m_alphaTest.m_ele[(int) (this.m_as_hw.m_alphaTest.m_ptr - 1U)] = true;
          break;
        case ICGraphics2d.State.Blend:
          this.m_as_hw.m_blend.m_ele[(int) (this.m_as_hw.m_blend.m_ptr - 1U)] = true;
          break;
        case ICGraphics2d.State.ColorKeyTest:
          this.m_as_hw.m_colorKeyTest.m_ele[(int) (this.m_as_hw.m_colorKeyTest.m_ptr - 1U)] = true;
          break;
        case ICGraphics2d.State.ConfigStateBasedOnSrcFormat:
          this.m_as_hw.m_configStateBasedOnSrcFormat.m_ele[(int) (this.m_as_hw.m_configStateBasedOnSrcFormat.m_ptr - 1U)] = true;
          break;
      }
    }

    public override void Disable(ICGraphics2d.State state)
    {
      switch (state)
      {
        case ICGraphics2d.State.AlphaTest:
          this.m_as_hw.m_alphaTest.m_ele[(int) (this.m_as_hw.m_alphaTest.m_ptr - 1U)] = false;
          break;
        case ICGraphics2d.State.Blend:
          this.m_as_hw.m_blend.m_ele[(int) (this.m_as_hw.m_blend.m_ptr - 1U)] = false;
          break;
        case ICGraphics2d.State.ColorKeyTest:
          this.m_as_hw.m_colorKeyTest.m_ele[(int) (this.m_as_hw.m_colorKeyTest.m_ptr - 1U)] = false;
          break;
        case ICGraphics2d.State.ConfigStateBasedOnSrcFormat:
          this.m_as_hw.m_configStateBasedOnSrcFormat.m_ele[(int) (this.m_as_hw.m_configStateBasedOnSrcFormat.m_ptr - 1U)] = false;
          break;
      }
    }

    public override void PushState(ICGraphics2d.State state)
    {
      this.m_as_hw.Push(state);
      if (state != ICGraphics2d.State.Color)
        return;
      this.m_color_xna_is_dirty = true;
    }

    public override void PopState(ICGraphics2d.State state)
    {
      this.m_as_hw.Pop(state);
      switch (state)
      {
        case ICGraphics2d.State.AlphaTest:
          int num1 = this.m_as_hw.m_alphaTest.m_ele[(int) (this.m_as_hw.m_alphaTest.m_ptr - 1U)] ? 1 : 0;
          break;
        case ICGraphics2d.State.Blend:
          int num2 = this.m_as_hw.m_blend.m_ele[(int) (this.m_as_hw.m_blend.m_ptr - 1U)] ? 1 : 0;
          break;
        case ICGraphics2d.State.Color:
          this.m_color_xna_is_dirty = true;
          break;
      }
    }

    public override void SetBlendArg(ICGraphics2d.BlendArg blendArg)
    {
      this.m_as_hw.m_blendArg.m_ele[(int) (this.m_as_hw.m_blendArg.m_ptr - 1U)] = blendArg;
    }

    public override void LoadIdentity()
    {
      this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)] = CGraphics2dAbstract.TransformCondition.Identity;
      this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)] = CGraphics2dAbstract.Matrix.Identity;
    }

    public override void LoadTransform(CMatrix2d m, CVector2d t)
    {
      this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)] = CGraphics2dAbstract.TransformCondition.Rotate | CGraphics2dAbstract.TransformCondition.Scale | CGraphics2dAbstract.TransformCondition.Translate;
      this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].load(m.m_col0.m_i, m.m_col0.m_j, m.m_col1.m_i, m.m_col1.m_j, t.m_i, t.m_j);
    }

    public override void MultiplyTransform(CMatrix2d m, CVector2d t)
    {
      this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)] = CGraphics2dAbstract.TransformCondition.Rotate | CGraphics2dAbstract.TransformCondition.Scale | CGraphics2dAbstract.TransformCondition.Translate;
      this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].multiply(m.m_col0.m_i, m.m_col0.m_j, m.m_col1.m_i, m.m_col1.m_j, t.m_i, t.m_j);
    }

    public override void Translate(int x, int y)
    {
      this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)] |= CGraphics2dAbstract.TransformCondition.Translate;
      if ((this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)] & (CGraphics2dAbstract.TransformCondition.Rotate | CGraphics2dAbstract.TransformCondition.Scale)) != CGraphics2dAbstract.TransformCondition.Identity)
      {
        this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].precat_T(x, y);
      }
      else
      {
        this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].t0 += x;
        this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].t1 += y;
      }
    }

    public override void Rotate(int angle)
    {
      this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)] |= CGraphics2dAbstract.TransformCondition.Rotate;
      this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].rotate(angle);
    }

    public override void Scale(int x, int y)
    {
      this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)] |= CGraphics2dAbstract.TransformCondition.Scale;
      this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].precat_S(x, y);
    }

    public override void PushTransform()
    {
      this.m_transform_condition_hw.Push();
      this.m_transform_shadow_hw.Push();
    }

    public override void PopTransform()
    {
      this.m_transform_condition_hw.Pop();
      this.m_transform_shadow_hw.Pop();
    }

    public override void GetTransform(out CMatrix2d m, out CVector2d t)
    {
      this.m_cachedOrientationAndScale_hw.m_col0.m_i = this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].m00;
      this.m_cachedOrientationAndScale_hw.m_col1.m_i = this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].m01;
      this.m_cachedOrientationAndScale_hw.m_col0.m_j = this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].m10;
      this.m_cachedOrientationAndScale_hw.m_col1.m_j = this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].m11;
      this.m_cachedTranslation_hw.m_i = this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].t0;
      this.m_cachedTranslation_hw.m_j = this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)].t1;
      m = this.m_cachedOrientationAndScale_hw;
      t = this.m_cachedTranslation_hw;
    }

    public override void SetVertexPositionCoords(
      int numOfCompsPerVertex,
      uint type,
      int stride,
      byte[] pos)
    {
      this.m_vertexPositionArray_numOfCompsPerVertex_hw = numOfCompsPerVertex;
      this.m_vertexPositionArray_type_hw = type;
      this.m_vertexPositionArray_stride_hw = stride;
      this.m_vertexPositionArray_pos_ptr_hw = pos;
    }

    public override void SetClip(uint x, uint y, uint dx, uint dy)
    {
      this.m_clipRect_hw.Set((int) x, (int) y, (int) dx, (int) dy);
      if (!this.m_hasRenderingBegun)
        return;
      this.UpdateActiveClipRect_HW();
    }

    public override void SetClip(CRectangle rect)
    {
      this.m_clipRect_hw.Set(rect);
      if (!this.m_hasRenderingBegun)
        return;
      this.UpdateActiveClipRect_HW();
    }

    public override CRectangle GetClip() => this.m_clipRect_hw;

    public override void Draw(ICRenderSurface surface)
    {
      this.Draw(surface, ICGraphics2d.Flip.NoFlip, new CRectangle?());
    }

    public override void Draw(ICRenderSurface surface, ICGraphics2d.Flip flip)
    {
      this.Draw(surface, flip, new CRectangle?());
    }

    public override void Draw(ICRenderSurface surface, ICGraphics2d.Flip flip, CRectangle? srcClip)
    {
      CRenderSurface_XNA_OffScreen surfaceXnaOffScreen = (CRenderSurface_XNA_OffScreen) surface;
      BlendState blendStateHw = this.m_blendState_hw;
      if (!this.m_suspendConfigStateBasedOnSrc && this.m_as_hw.m_configStateBasedOnSrcFormat.m_ele[(int) (this.m_as_hw.m_configStateBasedOnSrcFormat.m_ptr - 1U)])
      {
        this.m_as_hw.m_blend.m_ele[(int) (this.m_as_hw.m_blend.m_ptr - 1U)] = true;
        this.m_as_hw.m_blendArg.m_ele[(int) (this.m_as_hw.m_blendArg.m_ptr - 1U)] = ICGraphics2d.BlendArg.SrcAlphaInvSrcAlphaAdd;
      }
      CGraphics2d.ColorXNACondition colorXnaCondition1;
      if (this.m_as_hw.m_alphaTest.m_ele[(int) (this.m_as_hw.m_alphaTest.m_ptr - 1U)] || this.m_as_hw.m_blend.m_ele[(int) (this.m_as_hw.m_blend.m_ptr - 1U)])
      {
        if (this.m_as_hw.m_blend.m_ele[(int) (this.m_as_hw.m_blend.m_ptr - 1U)])
        {
          switch (this.m_as_hw.m_blendArg.m_ele[(int) (this.m_as_hw.m_blendArg.m_ptr - 1U)])
          {
            case ICGraphics2d.BlendArg.ConstAlphaInvConstAlphaAdd:
              colorXnaCondition1 = this.m_useColorState ? CGraphics2d.ColorXNACondition.ColorStatePreMult : CGraphics2d.ColorXNACondition.WhitePreMultConstAlpha;
              this.m_blendState_hw = BlendState.AlphaBlend;
              break;
            case ICGraphics2d.BlendArg.ConstAlphaOneAdd:
              colorXnaCondition1 = this.m_useColorState ? CGraphics2d.ColorXNACondition.ColorState : CGraphics2d.ColorXNACondition.WhiteConstAlpha;
              this.m_blendState_hw = BlendState.Additive;
              break;
            case ICGraphics2d.BlendArg.SrcAlphaInvSrcAlphaAdd:
              colorXnaCondition1 = this.m_useColorState ? CGraphics2d.ColorXNACondition.ColorStatePreMult : CGraphics2d.ColorXNACondition.White;
              this.m_blendState_hw = BlendState.AlphaBlend;
              break;
            case ICGraphics2d.BlendArg.SrcAlphaOneAdd:
              colorXnaCondition1 = this.m_useColorState ? CGraphics2d.ColorXNACondition.ColorState : CGraphics2d.ColorXNACondition.White;
              this.m_blendState_hw = BlendState.Additive;
              break;
            default:
              colorXnaCondition1 = this.m_useColorState ? CGraphics2d.ColorXNACondition.ColorState : CGraphics2d.ColorXNACondition.White;
              this.m_blendState_hw = BlendState.Additive;
              break;
          }
        }
        else
        {
          colorXnaCondition1 = this.m_useColorState ? CGraphics2d.ColorXNACondition.ColorStatePreMult : CGraphics2d.ColorXNACondition.White;
          this.m_blendState_hw = BlendState.AlphaBlend;
        }
      }
      else
      {
        CGraphics2d.ColorXNACondition colorXnaCondition2;
        colorXnaCondition1 = this.m_useColorState ? CGraphics2d.ColorXNACondition.ColorState : (colorXnaCondition2 = CGraphics2d.ColorXNACondition.White);
        this.m_blendState_hw = BlendState.Opaque;
      }
      switch (colorXnaCondition1)
      {
        case CGraphics2d.ColorXNACondition.White:
          this.m_image_draw_color_xna = Microsoft.Xna.Framework.Color.White;
          break;
        case CGraphics2d.ColorXNACondition.WhitePreMultConstAlpha:
          int a1 = this.m_as_hw.m_color.m_ele[(int) (this.m_as_hw.m_color.m_ptr - 1U)].m_colorfx.m_a;
          this.m_image_draw_color_xna.R = (byte) ((a1 >> 8) * (int) ushort.MaxValue >> 16);
          this.m_image_draw_color_xna.G = (byte) ((a1 >> 8) * (int) ushort.MaxValue >> 16);
          this.m_image_draw_color_xna.B = (byte) ((a1 >> 8) * (int) ushort.MaxValue >> 16);
          this.m_image_draw_color_xna.A = (byte) ((a1 >> 8) * (int) ushort.MaxValue >> 16);
          break;
        case CGraphics2d.ColorXNACondition.ColorState:
        case CGraphics2d.ColorXNACondition.ColorStatePreMult:
          if (this.m_color_xna_is_dirty)
          {
            int a2 = this.m_as_hw.m_color.m_ele[(int) (this.m_as_hw.m_color.m_ptr - 1U)].m_colorfx.m_a;
            int r = this.m_as_hw.m_color.m_ele[(int) (this.m_as_hw.m_color.m_ptr - 1U)].m_colorfx.m_r;
            int g = this.m_as_hw.m_color.m_ele[(int) (this.m_as_hw.m_color.m_ptr - 1U)].m_colorfx.m_g;
            int b = this.m_as_hw.m_color.m_ele[(int) (this.m_as_hw.m_color.m_ptr - 1U)].m_colorfx.m_b;
            this.m_color_xna.R = (byte) (r >> 8);
            this.m_color_xna.G = (byte) (g >> 8);
            this.m_color_xna.B = (byte) (b >> 8);
            this.m_color_xna.A = (byte) (a2 >> 8);
            this.m_color_xna_premult.R = (byte) ((a2 >> 8) * r >> 16);
            this.m_color_xna_premult.G = (byte) ((a2 >> 8) * g >> 16);
            this.m_color_xna_premult.B = (byte) ((a2 >> 8) * b >> 16);
            this.m_color_xna_premult.A = (byte) ((a2 >> 8) * a2 >> 16);
            this.m_color_xna_is_dirty = false;
          }
          this.m_image_draw_color_xna = colorXnaCondition1 != CGraphics2d.ColorXNACondition.ColorStatePreMult ? this.m_color_xna : this.m_color_xna_premult;
          break;
        default:
          int a3 = this.m_as_hw.m_color.m_ele[(int) (this.m_as_hw.m_color.m_ptr - 1U)].m_colorfx.m_a;
          this.m_image_draw_color_xna.R = Microsoft.Xna.Framework.Color.White.R;
          this.m_image_draw_color_xna.G = Microsoft.Xna.Framework.Color.White.G;
          this.m_image_draw_color_xna.B = Microsoft.Xna.Framework.Color.White.B;
          this.m_image_draw_color_xna.A = (byte) (a3 >> 8);
          break;
      }
      if (this.m_currSpriteBatch != null)
      {
        if (blendStateHw != this.m_blendState_hw && this.m_currSpriteBatch != null)
        {
          this.m_currSpriteBatch.End();
          this.m_currSpriteBatch = (SpriteBatch) null;
        }
        else if (this.m_currRasterizerStateId_hw != this.m_nextRasterizerStateId_hw)
        {
          this.m_currSpriteBatch.End();
          this.m_currSpriteBatch = (SpriteBatch) null;
        }
      }
      if (this.m_currSpriteBatch == null)
      {
        this.m_currSpriteBatch = this.m_spriteBatch;
        this.m_spriteSortMode_hw = SpriteSortMode.Immediate;
        this.m_currSpriteBatch.GraphicsDevice.ScissorRectangle = this.m_activeClipRect_hw;
        this.m_currRasterizerStateId_hw = this.m_nextRasterizerStateId_hw;
        this.m_currSpriteBatch.Begin(this.m_spriteSortMode_hw, this.m_blendState_hw, SamplerState.PointClamp, DepthStencilState.None, this.m_rasterizerState_hw[(int) this.m_currRasterizerStateId_hw]);
      }
      CGraphics2dAbstract.Matrix matrix 
                = this.m_transform_shadow_hw.m_ele[(int) (this.m_transform_shadow_hw.m_ptr - 1U)];
      CGraphics2dAbstract.TransformCondition transformCondition
                = this.m_transform_condition_hw.m_ele[(int) (this.m_transform_condition_hw.m_ptr - 1U)];
      if ((transformCondition & (CGraphics2dAbstract.TransformCondition.Rotate | CGraphics2dAbstract.TransformCondition.Scale)) != CGraphics2dAbstract.TransformCondition.Identity || surfaceXnaOffScreen.m_transpose)
      {
        this.m_trans_xna.X = 1.52587891E-05f * (float) matrix.t0;
        this.m_trans_xna.Y = 1.52587891E-05f * (float) matrix.t1;
        if ((transformCondition & CGraphics2dAbstract.TransformCondition.Scale) != CGraphics2dAbstract.TransformCondition.Identity)
        {
          this.m_scale_xna.X = (float) Math.Sqrt(2.3283064365386963E-10 * (double) matrix.m00 * (double) matrix.m00 + 2.3283064365386963E-10 * (double) matrix.m10 * (double) matrix.m10);
          this.m_scale_xna.Y = (float) Math.Sqrt(2.3283064365386963E-10 * (double) matrix.m01 * (double) matrix.m01 + 2.3283064365386963E-10 * (double) matrix.m11 * (double) matrix.m11);
        }
        else
        {
          this.m_scale_xna.X = 1f;
          this.m_scale_xna.Y = 1f;
        }
        float rotation;
        if ((transformCondition & CGraphics2dAbstract.TransformCondition.Rotate) != CGraphics2dAbstract.TransformCondition.Identity)
        {
          if ((double) this.m_scale_xna.X != 0.0 && (double) this.m_scale_xna.Y != 0.0)
          {
            float num1 = 1.52587891E-05f * (float) matrix.m00 / this.m_scale_xna.X;
            float num2 = 1.52587891E-05f * (float) matrix.m01 / this.m_scale_xna.Y;
            Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
            rotation = (float) Math.Acos((double) num1 / Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2));
            if ((double) num2 > 0.0)
              rotation = -rotation;
          }
          else
            rotation = 0.0f;
        }
        else
          rotation = 0.0f;
        SpriteEffects effects;
        if (surfaceXnaOffScreen.m_usingLoadtimeTransform)
        {
          if (surfaceXnaOffScreen.m_transpose)
            rotation += 1.57079637f;
          switch (flip)
          {
            case ICGraphics2d.Flip.NoFlip:
              effects = this.SpriteEffectsMap_LoadTime_NoFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            case ICGraphics2d.Flip.HorizontalFlip:
              effects = this.SpriteEffectsMap_LoadTime_HorizontalFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            case ICGraphics2d.Flip.VerticalFlip:
              effects = this.SpriteEffectsMap_LoadTime_VerticalFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            case ICGraphics2d.Flip.HorizontalAndVerticalFlip:
              effects = this.SpriteEffectsMap_LoadTime_HorizontalAndVerticalFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            default:
              effects = SpriteEffects.None;
              break;
          }
        }
        else
          effects = this.SpriteEffectsMap_DrawTime[(int) flip];
        if (srcClip.HasValue)
        {
          this.m_srcClip_xna.X = srcClip.Value.m_x;
          this.m_srcClip_xna.Y = srcClip.Value.m_y;
          this.m_srcClip_xna.Width = srcClip.Value.m_dx;
          this.m_srcClip_xna.Height = srcClip.Value.m_dy;
          this.m_currSpriteBatch.Draw(surfaceXnaOffScreen.m_texture, this.m_trans_xna, new Rectangle?(this.m_srcClip_xna), this.m_image_draw_color_xna, rotation, surfaceXnaOffScreen.m_origin, this.m_scale_xna, effects, 0.0f);
        }
        else
          this.m_currSpriteBatch.Draw(surfaceXnaOffScreen.m_texture, this.m_trans_xna, new Rectangle?(), this.m_image_draw_color_xna, rotation, surfaceXnaOffScreen.m_origin, this.m_scale_xna, effects, 0.0f);
      }
      else
      {
        this.m_trans_xna.X = 1.52587891E-05f * (float) matrix.t0;
        this.m_trans_xna.Y = 1.52587891E-05f * (float) matrix.t1;
        SpriteEffects effects;
        if (surfaceXnaOffScreen.m_usingLoadtimeTransform)
        {
          switch (flip)
          {
            case ICGraphics2d.Flip.NoFlip:
              effects = this.SpriteEffectsMap_LoadTime_NoFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            case ICGraphics2d.Flip.HorizontalFlip:
              effects = this.SpriteEffectsMap_LoadTime_HorizontalFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            case ICGraphics2d.Flip.VerticalFlip:
              effects = this.SpriteEffectsMap_LoadTime_VerticalFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            case ICGraphics2d.Flip.HorizontalAndVerticalFlip:
              effects = this.SpriteEffectsMap_LoadTime_HorizontalAndVerticalFlip[(int) surfaceXnaOffScreen.m_srcImgTransformType];
              break;
            default:
              effects = SpriteEffects.None;
              break;
          }
        }
        else
          effects = this.SpriteEffectsMap_DrawTime[(int) flip];
        if (srcClip.HasValue)
        {
          this.m_srcClip_xna.X = srcClip.Value.m_x;
          this.m_srcClip_xna.Y = srcClip.Value.m_y;
          this.m_srcClip_xna.Width = srcClip.Value.m_dx;
          this.m_srcClip_xna.Height = srcClip.Value.m_dy;
          this.m_currSpriteBatch.Draw(surfaceXnaOffScreen.m_texture, this.m_trans_xna, new Rectangle?(this.m_srcClip_xna), this.m_image_draw_color_xna, 0.0f, surfaceXnaOffScreen.m_origin, Vector2.One, effects, 0.0f);
        }
        else
          this.m_currSpriteBatch.Draw(surfaceXnaOffScreen.m_texture, this.m_trans_xna, new Rectangle?(), this.m_image_draw_color_xna, 0.0f, surfaceXnaOffScreen.m_origin, Vector2.One, effects, 0.0f);
      }
    }

    public override void Draw(ICGraphics2d.Primitive prim, int startIdx, int count)
    {
      if (this.m_as_hw.m_alphaTest.m_ele[(int) (this.m_as_hw.m_alphaTest.m_ptr - 1U)] && this.m_as_hw.m_color.m_ele[(int) (this.m_as_hw.m_color.m_ptr - 1U)].m_colorfx.m_a == 0)
        return;
      this.DrawHardwareRectangles(startIdx, count);
    }

    private void DrawHardwareRectangles(int startIdx, int count)
    {
      bool useColorState = this.m_useColorState;
      this.m_useColorState = true;
      switch (this.m_vertexPositionArray_type_hw)
      {
        case 4167462:
          for (int index = startIdx; index < startIdx + count; index += 2)
          {
            int x = ((int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * index] | (int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * index + 1] << 8) << 16;
            int y = ((int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * index + 2] | (int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * index + 3] << 8) << 16;
            int num1 = ((int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * (index + 1)] | (int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * (index + 1) + 1] << 8) << 16;
            int num2 = ((int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * (index + 1) + 2] | (int) this.m_vertexPositionArray_pos_ptr_hw[this.m_vertexPositionArray_stride_hw * (index + 1) + 3] << 8) << 16;
            this.PushTransform();
            this.Translate(x, y);
            this.Scale(num1 - x, num2 - y);
            this.Draw((ICRenderSurface) this.m_rectSurface, ICGraphics2d.Flip.NoFlip, new CRectangle?());
            this.PopTransform();
          }
          break;
      }
      this.m_useColorState = useColorState;
    }

    private void UpdateActiveClipRect_HW()
    {
      CRenderSurface targetSurface = (CRenderSurface) ICGraphics.GetInstance().GetTargetSurface();
      if (targetSurface == null)
        return;
      uint width = 0;
      uint height = 0;
      targetSurface.GetWidthAndHeight(out width, out height);
      if (this.m_clipRect_hw.m_x == 0 && this.m_clipRect_hw.m_y == 0 && this.m_clipRect_hw.m_dx == (int) width && this.m_clipRect_hw.m_dy == (int) height)
      {
        this.m_nextRasterizerStateId_hw = CGraphics2d.XNARasterizationStateId.NoSissor;
      }
      else
      {
        CRectangle crectangle;
        crectangle.m_x = 0;
        crectangle.m_y = 0;
        crectangle.m_dx = (int) width;
        crectangle.m_dy = (int) height;
        crectangle.Clip(this.m_clipRect_hw);
        this.m_activeClipRect_hw.X = crectangle.m_x;
        this.m_activeClipRect_hw.Y = crectangle.m_y;
        this.m_activeClipRect_hw.Width = crectangle.m_dx;
        this.m_activeClipRect_hw.Height = crectangle.m_dy;
        this.m_nextRasterizerStateId_hw = CGraphics2d.XNARasterizationStateId.Sissor;
        if (this.m_currSpriteBatch == null)
          return;
        this.m_currSpriteBatch.GraphicsDevice.ScissorRectangle = this.m_activeClipRect_hw;
      }
    }

    private enum XNARasterizationStateId
    {
      NoSissor,
      Sissor,
    }

    private enum ColorXNACondition
    {
      White,
      WhitePreMultConstAlpha,
      WhiteConstAlpha,
      ColorState,
      ColorStatePreMult,
    }
  }
}
