using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UI/Effects/Gradient")]
public class UIGradient : BaseMeshEffect
{
    public Color m_color1 = Color.white;
    public Color m_color2 = Color.white;
    public bool useMidColor;
    public Color m_color_mid = Color.white;

    [Range(-180f, 180f)]
    public float m_angle = 0f;
    public bool m_ignoreRatio = true;

    public override void ModifyMesh(VertexHelper vh)
    {
        if (enabled)
        {
            Rect rect = graphic.rectTransform.rect;
            Vector2 dir = UIGradientUtils.RotationDir(m_angle);

            if (!m_ignoreRatio)
                dir = UIGradientUtils.CompensateAspectRatio(rect, dir);

            UIGradientUtils.Matrix2x3 localPositionMatrix = UIGradientUtils.LocalPositionMatrix(rect, dir);

            UIVertex vertex = default(UIVertex);
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                Vector2 localPosition = localPositionMatrix * vertex.position;
                if (!useMidColor)
                {
                    vertex.color *= Color.Lerp(m_color2, m_color1, localPosition.y);
                }
                else
                {
                    if (localPosition.y < 0.5f)
                        vertex.color *= Color.Lerp(m_color2, m_color_mid, localPosition.y * 2);
                    else
                        vertex.color *= Color.Lerp(m_color_mid, m_color1, (localPosition.y - 0.5f) * 2);
                }
                vh.SetUIVertex(vertex, i);
            }
        }
    }

    public void SetDirty()
    {
        graphic.SetVerticesDirty();
    }
}
