using UnityEngine.UI;
using UnityEngine;

namespace MainArtery.Utilities.Unity
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
     * Extends the base Image component to allow Skew/Shear.
     * 
     * Code courtesty of user NIOS at
     * https://answers.unity.com/questions/1074814/is-it-possible-to-skew-or-shear-ui-elements-in-uni.html
     */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    public class SkewedImage : Image
    {
        [SerializeField] private float _skewX;
        [SerializeField] private float _skewY;

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);


            var height = rectTransform.rect.height;
            var width = rectTransform.rect.width;
            var xskew = height * Mathf.Tan(Mathf.Deg2Rad * _skewX);
            var yskew = width * Mathf.Tan(Mathf.Deg2Rad * _skewY);

            var ymin = rectTransform.rect.yMin;
            var xmin = rectTransform.rect.xMin;
            UIVertex v = new UIVertex();
            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref v, i);
                v.position += new Vector3(
                    Mathf.Lerp(0, xskew, (v.position.y - ymin) / height),
                    Mathf.Lerp(0, yskew, (v.position.x - xmin) / width),
                    0);
                vh.SetUIVertex(v, i);
            }

        }
    }
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
}