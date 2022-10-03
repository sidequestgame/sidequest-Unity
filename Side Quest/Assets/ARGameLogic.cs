using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Niantic.ARDK.Utilities;
using Niantic.ARDK.Utilities.Input.Legacy;
using Niantic.ARDK.Extensions;
using Niantic.ARDK.Networking.MultipeerNetworkingEventArgs;
using Niantic.ARDK.AR.HitTest;

using System.IO;
using Niantic.ARDK.Utilities.BinarySerialization;
using Niantic.ARDK.Networking;

public class ARGameLogic : MonoBehaviour
{
    public ARNetworkingManager manager;
    public Camera camera;
    public GameObject objectPrefab;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        manager.NetworkSessionManager.Networking.Connected += OnNetworkInitialized;
    }

    void OnNetworkInitialized(ConnectedArgs args)
    {
        // Connection initialized
        manager.NetworkSessionManager.Networking.PeerDataReceived += OnPeerDataReceived;
    }

    void OnPeerDataReceived(PeerDataReceivedArgs args)
    {
        if(args.Tag == 0)
        {
            var stream = new MemoryStream(args.CopyData());
            Vector3 position = (Vector3)GlobalSerializer.Deserialize(stream);
        }
    }

    // creating way to instantiate object
    void CreateObject(Vector3 position)
    {
        GameObject obj = Instantiate(objectPrefab, this.transform);
        obj.transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlatformAgnosticInput.touchCount <= 0) return;
        var touch = PlatformAgnosticInput.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            OnTapScreen(touch);
        }
    }
    
    void OnTapScreen(Touch touch)
    {
        // Touch event
        var currentFrame = manager.ARSessionManager.ARSession.CurrentFrame;

        if (currentFrame == null) return;

        var hitTestResults = currentFrame.HitTest (
               camera.pixelWidth,
               camera.pixelHeight,
               touch.position,
               ARHitTestResultType.EstimatedHorizontalPlane
               );
        
        if (hitTestResults.Count <= 0) return;

        var position = hitTestResults[0].WorldTransform.ToPosition();

        var stream = new MemoryStream();
        GlobalSerializer.Serialize(stream, position);
        byte[] data = stream.ToArray();

        manager.ARNetworking.Networking.BroadcastData(0, data, TransportType.ReliableOrdered,true);
    }
}
