using UnityEngine;
public static class ParticleSystemExtensions
{
    public static void TogglePlay(this ParticleSystem particleSystem)
    {
        if (particleSystem.isPlaying) particleSystem.Stop();
        else particleSystem.Play();
    }
}