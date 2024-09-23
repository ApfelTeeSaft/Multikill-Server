using LiteNetLib;
using LiteNetLib.Utils;
using Multikill_Server.Unity;
using System.Collections.Generic;

namespace Multikill_Server.Unity
{
    public class MovementHandler
    {
        private MultikillServer server;

        public MovementHandler(MultikillServer server)
        {
            this.server = server;
        }

        public void HandleMovement(NetPeer peer, NetPacketReader reader)
        {
            // Read position data sent from the client
            float posX = reader.GetFloat();
            float posY = reader.GetFloat();
            float posZ = reader.GetFloat();
            float rotX = reader.GetFloat();
            float rotY = reader.GetFloat();
            float rotZ = reader.GetFloat();

            // Update player data on the server
            if (server.connectedClients.ContainsKey(peer))
            {
                server.connectedClients[peer].PositionX = posX;
                server.connectedClients[peer].PositionY = posY;
                server.connectedClients[peer].PositionZ = posZ;
                server.connectedClients[peer].RotationX = rotX;
                server.connectedClients[peer].RotationY = rotY;
                server.connectedClients[peer].RotationZ = rotZ;
            }

            // Broadcast the player's movement to all other clients
            BroadcastMovement(peer, posX, posY, posZ, rotX, rotY, rotZ);
        }

        private void BroadcastMovement(NetPeer sender, float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(sender.Id);
            writer.Put(posX);
            writer.Put(posY);
            writer.Put(posZ);
            writer.Put(rotX);
            writer.Put(rotY);
            writer.Put(rotZ);

            foreach (var client in server.connectedClients.Keys)
            {
                if (client != sender)  // Don't send the update back to the sender
                {
                    client.Send(writer, DeliveryMethod.Unreliable);
                }
            }
        }
    }
}