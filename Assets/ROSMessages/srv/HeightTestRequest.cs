//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.ROSMessages
{
    [Serializable]
    public class HeightTestRequest : Message
    {
        public const string k_RosMessageName = "practice_msgs/HeightTest";
        public override string RosMessageName => k_RosMessageName;

        public float height_cube_mts;

        public HeightTestRequest()
        {
            this.height_cube_mts = 0.0f;
        }

        public HeightTestRequest(float height_cube_mts)
        {
            this.height_cube_mts = height_cube_mts;
        }

        public static HeightTestRequest Deserialize(MessageDeserializer deserializer) => new HeightTestRequest(deserializer);

        private HeightTestRequest(MessageDeserializer deserializer)
        {
            deserializer.Read(out this.height_cube_mts);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.height_cube_mts);
        }

        public override string ToString()
        {
            return "HeightTestRequest: " +
            "\nheight_cube_mts: " + height_cube_mts.ToString();
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
