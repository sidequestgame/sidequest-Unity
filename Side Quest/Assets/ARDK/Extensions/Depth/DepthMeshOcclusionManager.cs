// Copyright 2022 Niantic, Inc. All Rights Reserved.

using System;

namespace Niantic.ARDK.Extensions.Depth
{
  /// <summary>
  /// This helper can be placed in a scene to easily add occlusions, with minimal setup time. It
  /// reads synchronized depth output from ARFrame, and feeds it into an DepthMeshOcclusionEffect
  /// that then performs the actual shader occlusion. Both precision options of
  /// DepthMeshOcclusionEffect are available, and can be toggled between.
  /// </summary>
  [Obsolete("Use the ARDepthManager with its OcclusionMode property set to ScreenSpaceMesh instead.")]
  public class DepthMeshOcclusionManager:
    UnityLifecycleDriver
  {
  }
}
