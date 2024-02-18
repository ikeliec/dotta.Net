namespace dotta.Net.Demo;

public class FaceAttributesCommand
{
    public IFormFile Photo { get; set; } = default!;
}

public class FaceDetectCommand
{
    public IFormFile Photo { get; set; } = default!;
}

public class FaceMatchCommand
{
    public IFormFile PhotoOne { get; set; } = default!;
    public IFormFile PhotoTwo { get; set; } = default!;
}