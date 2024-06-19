using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class BendingManager : MonoBehaviour
{
    private const string BENDING_FEATURE = "ENABLE_BENDING";
    private void Awake()
    {
        if (Application.isPlaying)
        {
            Shader.EnableKeyword(BENDING_FEATURE);
        }
        else
        {
            Shader.DisableKeyword(BENDING_FEATURE);
        }
    }
    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }
    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }

    private static void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        cam.cullingMatrix = Matrix4x4.Ortho(-99f, 99f, -99f, 99f, 0.001f, 99f) *
            cam.worldToCameraMatrix;
    }
    private static void OnEndCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        cam.ResetCullingMatrix();
    }
}
