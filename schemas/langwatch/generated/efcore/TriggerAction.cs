#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum TriggerAction
{
    [PgName("SEND_EMAIL")]
    SendEmail,

    [PgName("ADD_TO_DATASET")]
    AddToDataset,

    [PgName("ADD_TO_ANNOTATION_QUEUE")]
    AddToAnnotationQueue,

    [PgName("SEND_SLACK_MESSAGE")]
    SendSlackMessage,

    [PgName("SEND_WEBHOOK")]
    SendWebhook,
}
