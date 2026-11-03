#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum ApiTokenAlgorithm
{
    [PgName("SHA512")]
    Sha512,
}
