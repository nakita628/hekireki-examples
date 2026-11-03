#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum From
{
    [PgName("BUYER")]
    Buyer,

    [PgName("SELLER")]
    Seller,
}
