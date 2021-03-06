//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.ROSMessages
{
    [Serializable]
    public class PoseOriginTargetMsg : Message
    {
        public const string k_RosMessageName = "practice_msgs/PoseOriginTarget";
        public override string RosMessageName => k_RosMessageName;

        public Geometry.PoseMsg pose_origin;
        public Geometry.PoseMsg pose_target;

        public PoseOriginTargetMsg()
        {
            this.pose_origin = new Geometry.PoseMsg();
            this.pose_target = new Geometry.PoseMsg();
        }

        public PoseOriginTargetMsg(Geometry.PoseMsg pose_origin, Geometry.PoseMsg pose_target)
        {
            this.pose_origin = pose_origin;
            this.pose_target = pose_target;
        }

        public static PoseOriginTargetMsg Deserialize(MessageDeserializer deserializer) => new PoseOriginTargetMsg(deserializer);

        private PoseOriginTargetMsg(MessageDeserializer deserializer)
        {
            this.pose_origin = Geometry.PoseMsg.Deserialize(deserializer);
            this.pose_target = Geometry.PoseMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.pose_origin);
            serializer.Write(this.pose_target);
        }

        public override string ToString()
        {
            return "PoseOriginTargetMsg: " +
            "\npose_origin: " + pose_origin.ToString() +
            "\npose_target: " + pose_target.ToString();
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
