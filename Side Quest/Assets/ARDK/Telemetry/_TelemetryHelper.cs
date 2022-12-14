using Niantic.ARDK.AR;
using Niantic.ARDK.AR.ARSessionEventArgs;
using Niantic.ARDK.AR.Protobuf;

using UnityEngine;

namespace Niantic.ARDK.Telemetry
{
  public static class _TelemetryHelper
  {
    private static IARSession _session;
    
    public static void Start()
    {
      ARSessionFactory.SessionInitialized += LogEventForInitialization;
    }

    private static void LogEventForInitialization(AnyARSessionInitializedArgs args)
    {
      _session = args.Session;
      _TelemetryService.RecordEvent(new ARSessionEvent()
      {
        SessionState = ARSessionEvent.Types.State.Created,
        BatteryLevel = SystemInfo.batteryLevel,
      });
      
      _session.Deinitialized += LogEventOnSessionClose;
      _session.SessionFailed += LogEventOnFailedSession;
    }

    private static void LogEventOnFailedSession(ARSessionFailedArgs args)
    {
      _TelemetryService.RecordEvent(
        new ARSessionEvent()
        {
          SessionState = ARSessionEvent.Types.State.Disposed,
          BatteryLevel = SystemInfo.batteryLevel,
        });

      UnlinkSessionCloseEvents();
    }
    
    private static void LogEventOnSessionClose(ARSessionDeinitializedArgs args)
    {
      _TelemetryService.RecordEvent(new ARSessionEvent()
      {
        SessionState = ARSessionEvent.Types.State.Disposed,
        BatteryLevel = SystemInfo.batteryLevel,
      });

      UnlinkSessionCloseEvents();
    }
    
    private static void UnlinkSessionCloseEvents()
    {
      _session.Deinitialized -= LogEventOnSessionClose;
      _session.SessionFailed -= LogEventOnFailedSession;
    }
  }
}
