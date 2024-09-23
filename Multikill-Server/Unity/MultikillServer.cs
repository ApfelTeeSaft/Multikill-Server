using LiteNetLib;
using Multikill_Server;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Multikill_Server.Unity
{
    public class MultikillServer : INetEventListener
    {
        private NetManager server;
        public Dictionary<NetPeer, PlayerData> connectedClients = new Dictionary<NetPeer, PlayerData>();
        private MovementHandler movementHandler;

        public MultikillServer()
        {
            movementHandler = new MovementHandler(this);
        }

        public void StartServer()
        {
            server = new NetManager(this)
            {
                AutoRecycle = true
            };
            server.Start(9050);  // Listen on port 9050
            Console.WriteLine("Server started on port 9050.");
        }

        public void StopServer()
        {
            server.Stop();
        }

        public void PollEvents()
        {
            server.PollEvents();
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Console.WriteLine($"Client connected: {peer.Address}");
            connectedClients.Add(peer, new PlayerData());
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Console.WriteLine($"Client disconnected: {peer.Address}. Reason: {disconnectInfo.Reason}");
            connectedClients.Remove(peer);
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {
            request.Accept();
            Console.WriteLine($"Incoming connection request from: {request.RemoteEndPoint}");
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {
            Console.WriteLine($"Network error occurred at {endPoint}: {socketError}");
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            Console.WriteLine($"Latency update for {peer.Address}: {latency}ms");
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
        {
            // Delegate movement data handling to MovementHandler
            movementHandler.HandleMovement(peer, reader);
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            Console.WriteLine($"Received unconnected message from {remoteEndPoint}");
        }
    }
}