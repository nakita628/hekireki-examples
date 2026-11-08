#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum AuthMethod
{
    [PgName("PASSWORD")]
    Password,

    [PgName("GOOGLE_OAUTH")]
    GoogleOauth,

    [PgName("GITHUB_OAUTH")]
    GithubOauth,
}
