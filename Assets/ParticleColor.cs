using UnityEngine;
using PixelCrushers;

public class ParticleColor : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particle;

    public void ChangeColor(){

        particle.startColor = Color.blue;
    }

}
