#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMErrorLogs
{
    public string RequestId { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string ApiBase { get; set; } = "";

    public string ModelGroup { get; set; } = "";

    public string LitellmModelName { get; set; } = "";

    public string ModelId { get; set; } = "";

    public string RequestKwargs { get; set; } = "{}";

    public string ExceptionType { get; set; } = "";

    public string ExceptionString { get; set; } = "";

    public string StatusCode { get; set; } = "";
}
