//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.ROSMessages
{
    [Serializable]
    public class MoveCubeActionResultMsg : Message
    {
        public const string k_RosMessageName = "practice_msgs/MoveCubeActionResult";
        public override string RosMessageName => k_RosMessageName;

        public Std.HeaderMsg header;
        public Actionlib.GoalStatusMsg status;
        public MoveCubeResultMsg result;

        public MoveCubeActionResultMsg()
        {
            this.header = new Std.HeaderMsg();
            this.status = new Actionlib.GoalStatusMsg();
            this.result = new MoveCubeResultMsg();
        }

        public MoveCubeActionResultMsg(Std.HeaderMsg header, Actionlib.GoalStatusMsg status, MoveCubeResultMsg result)
        {
            this.header = header;
            this.status = status;
            this.result = result;
        }

        public static MoveCubeActionResultMsg Deserialize(MessageDeserializer deserializer) => new MoveCubeActionResultMsg(deserializer);

        private MoveCubeActionResultMsg(MessageDeserializer deserializer)
        {
            this.header = Std.HeaderMsg.Deserialize(deserializer);
            this.status = Actionlib.GoalStatusMsg.Deserialize(deserializer);
            this.result = MoveCubeResultMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.header);
            serializer.Write(this.status);
            serializer.Write(this.result);
        }

        public override string ToString()
        {
            return "MoveCubeActionResultMsg: " +
            "\nheader: " + header.ToString() +
            "\nstatus: " + status.ToString() +
            "\nresult: " + result.ToString();
        }

#if UNITY_EDITOR
        [UnityEditor.InitializeOnLoadMethod]
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod]
#endif
        public static void Register()
        {
            MessageRegistry.Register(k_RosMessageName, Deserialize);
        }
    }
}
