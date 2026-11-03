#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ProjectAlertType
{
    [PgName("TASK_RUN")]
    TaskRun,

    [PgName("TASK_RUN_ATTEMPT")]
    TaskRunAttempt,

    [PgName("DEPLOYMENT_FAILURE")]
    DeploymentFailure,

    [PgName("DEPLOYMENT_SUCCESS")]
    DeploymentSuccess,

    [PgName("ERROR_GROUP")]
    ErrorGroup,

    [PgName("DASHBOARD_AGENT_WATCH")]
    DashboardAgentWatch,
}
