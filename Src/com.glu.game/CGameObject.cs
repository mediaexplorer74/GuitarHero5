// Decompiled with JetBrains decompiler
// Type: com.glu.game.CGameObject
// Assembly: Guitar Hero 5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 86E366E3-F44F-4C53-89BA-5BEFDCC09A14
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\Guitar Hero 5.dll

using com.glu.shared;

#nullable disable
namespace com.glu.game
{
  internal class CGameObject : CClass
  {
    public int GAMEOBJECT_MAX_VERTEX = 10;
    protected int m_id;
    protected bool m_active;
    protected bool m_visible;
    protected CVector2d m_position;
    protected int m_orientation;
    protected CVector2d m_scale;
    protected bool m_hasLimits;
    protected int m_limitX;
    protected int m_limitY;
    protected int m_radius;
    protected object m_pData;
    public int COLLISION_DEBUG;

    public int GetID() => this.m_id;

    public bool IsActive() => this.m_active;

    public bool IsVisible() => this.m_visible;

    public int GetRadius() => this.m_radius;

    public bool HasLimits() => this.m_hasLimits;

    public int GetLimitX() => this.m_limitX;

    public int GetLimitY() => this.m_limitY;

    public object GetUserData() => this.m_pData;

    public CVector2d GetPosition() => this.m_position;

    public int GetOrientation() => this.m_orientation;

    public CVector2d GetScale() => this.m_scale;

    public void SetID(int id) => this.m_id = id;

    public void SetActive(bool active) => this.m_active = active;

    public void SetVisible(bool visible) => this.m_visible = visible;

    public void SetRadius(int radius) => this.m_radius = radius;

    public void SetHasLimits(bool hasLimits) => this.m_hasLimits = hasLimits;

    public void SetLimits(int limitX, int limitY)
    {
      this.m_limitX = limitX;
      this.m_limitY = limitY;
    }

    public void SetUserData(object pData) => this.m_pData = pData;

    public void SetPosition(CVector2d position) => this.SetPosition(position.m_i, position.m_j);

    public void SetScale(CVector2d scale) => this.SetScale(scale.m_i, scale.m_j);

    public void Scale(CVector2d scale) => this.Scale(scale.m_i, scale.m_j);

    public void Move(CVector2d disp) => this.Move(disp.m_i, disp.m_j);

    public CGameObject()
    {
      this.m_id = 0;
      this.m_active = false;
      this.m_visible = false;
      this.m_position.Clear();
      this.m_orientation = 0;
      this.m_scale.Set(65536, 65536);
      this.m_hasLimits = false;
      this.m_limitX = 0;
      this.m_limitY = 0;
      this.m_radius = 0;
      this.m_pData = (object) null;
    }

    private void SetPosition(int x, int y)
    {
      if (this.m_hasLimits)
      {
        if (x < 0)
          x = 0;
        else if (x > this.m_limitX)
          x = this.m_limitX;
        if (y < 0)
          y = 0;
        else if (y > this.m_limitY)
          y = this.m_limitY;
      }
      this.m_position.Set(x, y);
    }

    private void SetOrientation(int orientation) => this.m_orientation = orientation;

    private void SetScale(int x, int y)
    {
      this.m_scale.m_i = x;
      this.m_scale.m_j = y;
    }

    private void Scale(int x, int y)
    {
      this.SetScale(CMathFixed.Mul(this.m_scale.m_i, x), CMathFixed.Mul(this.m_scale.m_j, y));
    }

    private void Rotate(int rotation)
    {
      if (rotation == 0)
        return;
      this.SetOrientation(this.m_orientation + rotation);
    }

    private void Move(int x, int y)
    {
      this.SetPosition(this.m_position.m_i + x, this.m_position.m_j + y);
    }

    private bool TestMovement(
      CGameObject obj1,
      CGameObject obj2,
      CVector2d disp1,
      int rotation1,
      CGameObject.eCollisionTestType type,
      CGameObject.tCollisionInfo info)
    {
      bool flag = false;
      if (obj1.GetRadius() > 0 && obj2.GetRadius() > 0 && (disp1.m_i != 0 || disp1.m_j != 0 || rotation1 != 0))
      {
        int disp1Len = disp1.Length();
        if (type == CGameObject.eCollisionTestType.COLLISION_TEST_SPHERE)
          flag = this.TestCollisionSphereSweep(obj1, obj2, disp1, disp1Len, info);
      }
      if (flag)
      {
        info.m_isTile = false;
        info.m_objID = obj2.GetID();
      }
      return flag;
    }

    private bool TestMovement(
      CGameObject obj1,
      CGameObject obj2,
      CVector2d disp1,
      CVector2d disp2,
      int rotation1,
      int rotation2,
      CGameObject.eCollisionTestType type,
      CGameObject.tCollisionInfo info)
    {
      CVector2d disp1_1 = disp1 - disp2;
      int rotation1_1 = rotation1 - rotation2;
      return this.TestMovement(obj1, obj2, disp1_1, rotation1_1, type, info);
    }

    private void CalculateCollisionSurface(
      CGameObject obj1,
      CVector2d point1,
      CGameObject.eCollisionTestType type,
      CVector2d surface)
    {
      if (type != CGameObject.eCollisionTestType.COLLISION_TEST_SPHERE)
        return;
      surface = obj1.GetPosition() - point1;
      surface.Set(-surface.m_j, surface.m_i);
    }

    private bool TestCollisionSphereSweep(
      CGameObject obj1,
      CGameObject obj2,
      CVector2d disp1,
      int disp1Len,
      CGameObject.tCollisionInfo info)
    {
      bool flag = false;
      if (disp1.m_i != 0 || disp1.m_j != 0)
      {
        info.m_u = 65536;
        CVector2d cvector2d1 = obj2.GetPosition() - obj1.GetPosition();
        int num1 = disp1Len + obj1.GetRadius() + obj2.GetRadius();
        if (CMathFixed.Abs(cvector2d1.m_i) <= num1 && CMathFixed.Abs(cvector2d1.m_j) <= num1)
        {
          CVector2d cvector2d2 = new CVector2d(-disp1.m_i, -disp1.m_j);
          int a = cvector2d2 * cvector2d2;
          int b = cvector2d2 * cvector2d1 << 1;
          int num2 = obj1.GetRadius() + obj2.GetRadius();
          int c = cvector2d1 * cvector2d1 - CMathFixed.Mul(num2, num2);
          if (c <= 0)
          {
            flag = true;
            info.m_containment = true;
            info.m_u = 0;
          }
          else
          {
            int root1;
            int root2;
            if (CMathFixed.SolveQuadraticForReals(out root1, out root2, a, b, c))
            {
              int num3 = CMathFixed.Min(root1, root2);
              if (num3 >= 0 && num3 <= 65536)
              {
                flag = true;
                info.m_containment = false;
                info.m_u = num3;
              }
            }
          }
          if (flag)
          {
            CVector2d cvector2d3 = new CVector2d(disp1);
            cvector2d3.Normalize();
            info.m_point.m_i = obj1.GetPosition().m_i + CMathFixed.Mul(obj1.GetRadius(), cvector2d3.m_i) + CMathFixed.Mul(info.m_u, disp1.m_i);
            info.m_point.m_j = obj1.GetPosition().m_j + CMathFixed.Mul(obj1.GetRadius(), cvector2d3.m_j) + CMathFixed.Mul(info.m_u, disp1.m_j);
            this.CalculateCollisionSurface(obj2, info.m_point, CGameObject.eCollisionTestType.COLLISION_TEST_SPHERE, info.m_surface);
          }
        }
      }
      return flag;
    }

    public enum eCollisionTestType
    {
      COLLISION_TEST_NONE,
      COLLISION_TEST_SPHERE,
    }

    private struct tCollisionInfo
    {
      public bool m_isTile;
      public bool m_containment;
      public int m_u;
      public CVector2d m_point;
      public CVector2d m_surface;
      public int m_objID;
      public int m_cellIndexX;
      public int m_cellIndexY;
    }
  }
}
