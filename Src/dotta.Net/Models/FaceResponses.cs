using System;

namespace dotta.Net
{
    public class ErrorResponse
    {
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class FaceAttributesResponse : ErrorResponse
    {
        public float Male { get; set; }
        public float Female { get; set; }
        public float Age { get; set; }
        public float Smile { get; set; }
        public float EyesOpen { get; set; }
        public float PassiveLiveness { get; set; }
        public string Orientation { get; set; }
    }

    public class FaceDetectResponse : ErrorResponse
    {
        public int PointX { get; set; }
        public int PointY { get; set; }
        public int Width { get; set; }
        public int Padding { get; set; }
        public double Angle { get; set; }
        public string Orientation { get; set; }
    }

    public class FaceMatchResponse : ErrorResponse
    {
        public float SimilarityScore { get; set; }
    }

    public class FaceActiveLivenessResponse : ErrorResponse
    {
        public double LivenessScore { get; set; }
    }
}
