//Do not edit! This file was generated by Unity-ROS MessageGeneration.
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Unity.Robotics.ROSTCPConnector.MessageGeneration;

namespace RosMessageTypes.ROSMessages
{
    [Serializable]
    public class MoveCubeActionFeedbackMsg : Message
    {
        public const string k_RosMessageName = "practice_msgs/MoveCubeActionFeedback";
        public override string RosMessageName => k_RosMessageName;

        public Std.HeaderMsg header;
        public Actionlib.GoalStatusMsg status;
        public MoveCubeFeedbackMsg feedback;

        public MoveCubeActionFeedbackMsg()
        {
            this.header = new Std.HeaderMsg();
            this.status = new Actionlib.GoalStatusMsg();
            this.feedback = new MoveCubeFeedbackMsg();
        }

        public MoveCubeActionFeedbackMsg(Std.HeaderMsg header, Actionlib.GoalStatusMsg status, MoveCubeFeedbackMsg feedback)
        {
            this.header = header;
            this.status = status;
            this.feedback = feedback;
        }

        public static MoveCubeActionFeedbackMsg Deserialize(MessageDeserializer deserializer) => new MoveCubeActionFeedbackMsg(deserializer);

        private MoveCubeActionFeedbackMsg(MessageDeserializer deserializer)
        {
            this.header = Std.HeaderMsg.Deserialize(deserializer);
            this.status = Actionlib.GoalStatusMsg.Deserialize(deserializer);
            this.feedback = MoveCubeFeedbackMsg.Deserialize(deserializer);
        }

        public override void SerializeTo(MessageSerializer serializer)
        {
            serializer.Write(this.header);
            serializer.Write(this.status);
            serializer.Write(this.feedback);
        }

        public override string ToString()
        {
            return "MoveCubeActionFeedbackMsg: " +
            "\nheader: " + header.ToString() +
            "\nstatus: " + status.ToString() +
            "\nfeedback: " + feedback.ToString();
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
