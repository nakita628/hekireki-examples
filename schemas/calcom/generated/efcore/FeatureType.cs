#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum FeatureType
{
    [PgName("RELEASE")]
    Release,

    [PgName("EXPERIMENT")]
    Experiment,

    [PgName("OPERATIONAL")]
    Operational,

    [PgName("KILL_SWITCH")]
    KillSwitch,

    [PgName("PERMISSION")]
    Permission,
}
