using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;
using System.IO;

public class ConnectAPI : IConnectionListener<PegasusPacket>
{
    private static bool s_initialized = false;
    private static ConnectAPI s_connectAPI;
    private static ClientConnection<PegasusPacket> s_gameServer;
    private static Queue<PegasusPacket> s_gamePackets = new Queue<PegasusPacket>();
    private static Queue<PegasusPacket> s_utilPackets = new Queue<PegasusPacket>();
    private static Queue<PegasusPacket> s_debugPackets = new Queue<PegasusPacket>();

    private static SortedDictionary<int, ConnectAPI.PacketDecoder> s_packetDecoders = new SortedDictionary<int, ConnectAPI.PacketDecoder>();

    public static void ConnectInit()
    {
        if (!ConnectAPI.s_initialized)
        {
            ConnectAPI.s_connectAPI = new ConnectAPI();

            ApplicationMgr.Get().Resetting += new System.Action(ConnectAPI.OnReset);
            ConnectAPI.s_gameServer = new ClientConnection<PegasusPacket>();
            ConnectAPI.s_gameServer.AddListener(ConnectAPI.s_connectAPI, ConnectAPI.s_gamePackets);
            ConnectAPI.s_gameServer.AddConnectHandler(new ClientConnection<PegasusPacket>.ConnectHandler(
                ConnectAPI.s_connectAPI.OnGameServerConnectCallback));
            ConnectAPI.s_gameServer.AddDisconnectHandler(new ClientConnection<PegasusPacket>.DisconnectHandler(
                ConnectAPI.s_connectAPI.OnGameServerDisconnectCallback));

            ConnectAPI.s_packetDecoders.Add(1, new ConnectAPI.DefaultProtobufPacketDecoder<Tutorial.AddressBook>());


            ConnectAPI.s_initialized = true;
        }
    }
	private static void OnReset()
    {

    }

    private void OnGameServerConnectCallback(BattleNetErrors error)
    {
        Log.GameMgr.Print("Connecting to game server with error code " + error, new object[0]);
        if (error != BattleNetErrors.ERROR_OK)
        {
            //ConnectAPI.GameStartState gameStartState = ConnectAPI.s_gameStartState;
            //ConnectAPI.s_gameStartState = ConnectAPI.GameStartState.INVALID;
            //if (global::Network.ShouldBeConnectedToAurora())
            //{
            //    BattleNet.GameServerInfo lastGameServerJoined = global::Network.Get().GetLastGameServerJoined();
            //    if (lastGameServerJoined != null && gameStartState == ConnectAPI.GameStartState.RECONNECTING)
            //    {
            //        return;
            //    }
            //    global::Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_NO_GAME_SERVER", 0f);
            //    Debug.LogError("Failed to connect to game server with error " + error);
            //}
            //else
            //{
            //    global::Network.Get().ShowBreakingNewsOrError("GLOBAL_ERROR_NETWORK_NO_GAME_SERVER", 0f);
            //    Debug.LogError("Failed to connect to game server with error " + error);
            //}
        }
    }
    private void OnGameServerDisconnectCallback(BattleNetErrors error)
    {
        Log.GameMgr.Print("Disconnected from game server with error {0} {1}", new object[]
		{
			(int)error,
			error.ToString()
		});
        //bool flag = false;
        //if (error != BattleNetErrors.ERROR_OK)
        //{
        //    if (ConnectAPI.s_gameStartState == ConnectAPI.GameStartState.RECONNECTING)
        //    {
        //        flag = true;
        //    }
        //    else
        //    {
        //        if (ConnectAPI.s_gameStartState == ConnectAPI.GameStartState.INITIAL_START)
        //        {
        //            BattleNet.GameServerInfo lastGameServerJoined = global::Network.Get().GetLastGameServerJoined();
        //            if (lastGameServerJoined == null || !lastGameServerJoined.SpectatorMode)
        //            {
        //                ConnectAPI.ConnectErrorParams connectErrorParams = new ConnectAPI.ConnectErrorParams();
        //                connectErrorParams.m_message = GameStrings.Format((error != BattleNetErrors.ERROR_RPC_CONNECTION_TIMED_OUT) ? "GLOBAL_ERROR_NETWORK_DISCONNECT_GAME_SERVER" : "GLOBAL_ERROR_NETWORK_CONNECTION_TIMEOUT", new object[0]);
        //                ConnectAPI.s_errorList.Add(connectErrorParams);
        //                Debug.LogError("Disconnected from game server with error " + error);
        //                flag = true;
        //            }
        //        }
        //    }
        //    ConnectAPI.s_gameStartState = ConnectAPI.GameStartState.INVALID;
        //}
        //if (!flag)
        //{
        //    if (ConnectAPI.GameServerDisconnectEvents == null)
        //    {
        //        ConnectAPI.GameServerDisconnectEvents = new List<BattleNetErrors>();
        //    }
        //    ConnectAPI.GameServerDisconnectEvents.Add(error);
        //}
    }
    public void PacketReceived(PegasusPacket p, object state)
    {
        ConnectAPI.QueuePacketReceived(p, (Queue<PegasusPacket>)state);
    }
    private static void QueuePacketReceived(PegasusPacket packet, Queue<PegasusPacket> queue)
    {
        //if (queue == ConnectAPI.s_gamePackets && packet.Type == 16)
        //{
        //    ConnectAPI.s_gameStartState = ConnectAPI.GameStartState.INVALID;
        //}
        //if (queue == ConnectAPI.s_gamePackets && packet.Type == 116)
        //{
        //    ConnectAPI.s_lastPingReceived = Time.realtimeSinceStartup;
        //    ConnectAPI.s_pingsSentSinceLastPong = 0;
        //}
        ConnectAPI.PacketDecoder packetDecoder;
        if (ConnectAPI.s_packetDecoders.TryGetValue(packet.Type, out packetDecoder))
        {
            PegasusPacket pegasusPacket = packetDecoder.HandlePacket(packet);
            if (pegasusPacket != null)
            {
                queue.Enqueue(pegasusPacket);
            }
        }
        else
        {
            Debug.LogError("Could not find a packet decoder for a packet of type " + packet.Type);
        }
    }
    public abstract class PacketDecoder
    {
        public PacketDecoder()
        {
        }
        public static PegasusPacket HandleProtobuf<T>(PegasusPacket p) where T : IProtoBuf, new()
        {
            byte[] buffer = (byte[])p.Body;
            T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
            t.Deserialize(new MemoryStream(buffer));
            p.Body = t;
            return p;
        }
        public abstract PegasusPacket HandlePacket(PegasusPacket p);
    }
    public class DefaultProtobufPacketDecoder<T> : ConnectAPI.PacketDecoder where T : IProtoBuf, new()
    {
        public override PegasusPacket HandlePacket(PegasusPacket p)
        {
            return ConnectAPI.PacketDecoder.HandleProtobuf<T>(p);
        }
    }
}
