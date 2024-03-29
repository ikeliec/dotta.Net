﻿using System;

namespace dotta.Net
{
    public class HttpDottaResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
    }

    public class HttpDottaFaceAttributesResponse : HttpDottaResponse
    {
        public HttpDottaFaceAttributesResponseData data { get; set; }
    }
    public class HttpDottaFaceAttributesResponseData
    {
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public float male { get; set; }
        public float female { get; set; }
        public float age { get; set; }
        public float smile { get; set; }
        public float eyesOpen { get; set; }
        public float passiveLiveness { get; set; }
        public string orientation { get; set; }
    }

    public class HttpDottaFaceDetectResponse : HttpDottaResponse
    {
        public HttpDottaFaceDetectResponseData data { get; set; }
    }
    public class HttpDottaFaceDetectResponseData
    {
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public int pointX { get; set; }
        public int pointY { get; set; }
        public int width { get; set; }
        public int padding { get; set; }
        public double angle { get; set; }
        public string orientation { get; set; }
    }

    public class HttpDottaFaceMatchResponse : HttpDottaResponse
    {
        public HttpDottaFaceMatchResponseData data { get; set; }
    }
    public class HttpDottaFaceMatchResponseData
    {
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public float similarityScore { get; set; }
    }

    public class HttpDottaFaceActiveLivenessResponse : HttpDottaResponse
    {
        public HttpDottaFaceActiveLivenessResponseData data { get; set; }
    }
    public class HttpDottaFaceActiveLivenessResponseData
    {
        public int? errorCode { get; set; }
        public string errorMessage { get; set; }
        public double livenessScore { get; set; }
    }
}
