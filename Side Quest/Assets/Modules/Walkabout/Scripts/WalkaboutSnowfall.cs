// Copyright 2022 Niantic, Inc. All Rights Reserved.

using UnityEngine;

namespace Niantic.ARVoyage.Walkabout
{
    /// <summary>
    /// Snowfall particle effects for Walkabout demo 
    /// </summary>
    [RequireComponent(typeof(ParticleSystem))]
    public class WalkaboutSnowfall : MonoBehaviour
    {
        private WalkaboutActor yetiAndSnowball;
        private ParticleSystem.EmissionModule emissionModule;

        [SerializeField] GameObject killTrigger;

        void Awake()
        {
            yetiAndSnowball = SceneLookup.Get<WalkaboutManager>().yetiAndSnowball;

            ParticleSystem snowParticleSystem = GetComponent<ParticleSystem>();
            emissionModule = snowParticleSystem.emission;
            emissionModule.enabled = false;
        }

        void Update()
        {
            // Constrain to snowing above Yeti's position
            transform.position = yetiAndSnowball.transform.position;

            // Enable if Yeti is active and is not transparent
            bool enabled = yetiAndSnowball.gameObject.activeInHierarchy && !yetiAndSnowball.IsTransparent;
            emissionModule.enabled = enabled;
            killTrigger.SetActive(enabled);
        }
    }
}
