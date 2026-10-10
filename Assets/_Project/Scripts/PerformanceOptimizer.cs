using UnityEngine;

public class PerformanceOptimizer : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 60;
    [SerializeField] private bool useVSync = true;

    private void Start()
    {
        OptimizePerformance();
    }

    private void OptimizePerformance()
    {
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = useVSync ? 1 : 0;

        Physics.defaultSolverIterations = 4;
        Physics.defaultSolverVelocityIterations = 2;

        Debug.Log($"Performance optimizado: {targetFrameRate} FPS, VSync: {useVSync}");
    }

    private void OnGUI()
    {
        if (Input.GetKey(KeyCode.F3))
        {
            GUI.Label(new Rect(10, 10, 300, 100),
                $"FPS: {(1f / Time.deltaTime):F0}\n" +
                $"Memory: {System.GC.GetTotalMemory(false) / 1048576}MB\n" +
                $"Batches: {UnityStats.batches}");
        }
    }
}
