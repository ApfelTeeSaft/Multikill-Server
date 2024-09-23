using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Multikill_Server
{
    public class MultikillServer : INetEventListener
    {
        private NetManager server;
        private List<NetPeer> connectedClients = new List<NetPeer>();

        static void Main(string[] args)
        {
            MultikillServer multiplayerServer = new MultikillServer();
            multiplayerServer.StartServer();
        }

        public void StartServer()
        {
            server = new NetManager(this)
            {
                AutoRecycle = true
            };
            server.Start(9050);
            Console.WriteLine("Server started on port 9050.");

            while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Escape)
            {
                server.PollEvents();
                Thread.Sleep(15);
            }

            server.Stop();
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Console.WriteLine($"Client connected: {peer.Address}");
            connectedClients.Add(peer);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Console.WriteLine($"Client disconnected: {peer.Address}. Reason: {disconnectInfo.Reason}");
            connectedClients.Remove(peer);
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
        {
            string receivedMessage = reader.GetString();
            Console.WriteLine($"Received from {peer.Address}: {receivedMessage}");

            NetDataWriter writer = new NetDataWriter();
            writer.Put(receivedMessage);

            foreach (var client in connectedClients)
            {
                if (client != peer)
                {
                    client.Send(writer, DeliveryMethod.ReliableOrdered);
                }
            }
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

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            Console.WriteLine($"Received unconnected message from {remoteEndPoint}");
        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            Console.WriteLine($"Latency update for {peer.Address}: {latency}ms");
        }
    }
}