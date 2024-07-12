using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace MainArtery.Utilities.Unity.UI
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
     *  Capture the current camera view and render it out to a texture.
     *  
     *  Based on code from:
     *  https://crappycoding.com/2014/12/create-gameobject-image-using-render-textures/
     *  https://docs.unity3d.com/ScriptReference/ImageConversion.EncodeToPNG.html
     */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
	public class CaptureToTexture : MonoBehaviour
	{
        /// =======================================================================================
        /// Fields
        /// =======================================================================================
        private Camera _cam;

        public int _textureSize = 256;

        public Image TargetImage;
        [Space]
        public string ImageName;

        /// =======================================================================================
        /// Mono
        /// =======================================================================================
		void Awake()
		{
            _cam = this.GetComponent<Camera>();
        }
        /// =======================================================================================
        /// Methods
        /// =======================================================================================
        /// ---------------------------------------------------------------------------------------
        public Texture2D Render()
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(_textureSize, _textureSize, 24);
            _cam.targetTexture = renderTexture;
            _cam.Render();

            RenderTexture saveActive = RenderTexture.active;
            RenderTexture.active = _cam.targetTexture;

            Texture2D texture = new Texture2D(_textureSize, _textureSize);
            texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            texture.Apply();

            RenderTexture.active = saveActive;
            _cam.targetTexture = null;
            RenderTexture.ReleaseTemporary(renderTexture);

            return texture;
        }
        /// ---------------------------------------------------------------------------------------
        public void RenderToTarget()
        {
            if (this.TargetImage == null)
                return;

            this.TargetImage.sprite = Sprite.Create(
                this.Render(),
                new Rect(0, 0, _textureSize, _textureSize), 
                new Vector2(0.5f, 0.5f)
                );
        }
        /// ---------------------------------------------------------------------------------------
        public void RenderToPath(string path)
        {
            Texture2D tex = this.Render();
            byte[] bytes = tex.EncodeToPNG();

            UnityEngine.Object.DestroyImmediate(tex);

            File.WriteAllBytes(path, bytes);
        }
        /// ---------------------------------------------------------------------------------------
        /// =======================================================================================
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}