using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPManager : MonoBehaviour
{
    public static PPManager Instance;
    [NotNull][SerializeField] private PostProcessProfile postProcessProfile;
    public ChromaticAberration ChromaticAberration { get; private set; }
    public ColorGrading ColorGrading { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else Instance = this;
    }

    private void Start()
    {
        postProcessProfile.TryGetSettings<ChromaticAberration>(out var chromaticAberration);
        ChromaticAberration = chromaticAberration;

        postProcessProfile.TryGetSettings<ColorGrading>(out var colorGrading);
        ColorGrading = colorGrading;
    }
}
