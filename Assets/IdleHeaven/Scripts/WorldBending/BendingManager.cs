using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
public class BendingManager : MonoSingleton<BendingManager>
{
    private const string BENDING_FEATURE = "ENABLE_BENDING";
    public Material material;
    private void Awake()
    {
        if (Application.isPlaying)
        {
            enabled = true;
            Shader.EnableKeyword(BENDING_FEATURE);
        }
        else
        {
            enabled = false;
            Shader.DisableKeyword(BENDING_FEATURE);
        }
    }
    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        Shader.EnableKeyword(BENDING_FEATURE);
    }
    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
        Shader.DisableKeyword(BENDING_FEATURE);
    }

    private static void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        cam.cullingMatrix = Matrix4x4.Ortho(-99f, 99f, -99f, 99f, 0.001f, 200f) *
            cam.worldToCameraMatrix;
    }
    private static void OnEndCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
        cam.ResetCullingMatrix();
    }

    public Vector3 ModifiedPosition(Vector3 position)
    {
        Vector3 modifiedPosition = position;
        
        float offset = material.GetFloat("_CameraOffset");
        float amount = material.GetFloat("_Amount");

        Transform camTrf = Camera.main.transform;

        float dist = Vector3.Distance(camTrf.position + camTrf.forward * offset , position);

        float modifY = Mathf.Pow(dist, 2f) * -amount;

        modifiedPosition.y += modifY;

        return modifiedPosition;
    }
}
