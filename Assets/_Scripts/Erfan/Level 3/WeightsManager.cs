using UnityEngine;

public class WeightsManager : MonoBehaviour
{
    [SerializeField] Transform gridParent;
    [SerializeField] ScaleWeight prefabW;
    [SerializeField] bool isRight;
    public RectTransform Obj;
    private Vector3 defaultRectPos;
    private void Start()
    {
        defaultRectPos= new Vector3(Obj.anchoredPosition.x, Obj.anchoredPosition.y);
    }
    public void ResetScale()
    {
        Obj.anchoredPosition= defaultRectPos;
    }
    public void PutWeights(Equation equation)
    {
        ResetScale();
        foreach (Transform t in gridParent.transform)
        {
            Destroy(t.gameObject);
        }
        if (isRight)
        {
            for (int i = 0; i < equation.numberXR; i++)
            {
                var obj = Instantiate(prefabW, gridParent);
                obj.ShowText(equation.encapsulatingXR[0]);
            }
            if (equation.numberR != 0)
            {
                var obj2 = Instantiate(prefabW, gridParent);
                obj2.ShowText(equation.numberR.ToString());
            }
        }
        else
        {
            for (int i = 0; i < equation.numberXL; i++)
            {
                var obj = Instantiate(prefabW, gridParent);
                obj.ShowText(equation.encapsulatingXL[0]);
            }
            if (equation.numberL != 0)
            {
                var obj2 = Instantiate(prefabW, gridParent);
                obj2.ShowText(equation.numberL.ToString());
            }
        }
    }
}
