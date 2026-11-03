package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.Table

object UserWatchlistTable : Table("\"_UserWatchlist\"") {
    val a =
        reference(
            "\"A\"",
            SymbolProfileTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_UserWatchlist_A_fkey\"",
        )
    val b =
        reference(
            "\"B\"",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"_UserWatchlist_B_fkey\"",
        )
    override val primaryKey = PrimaryKey(a, b, name = "\"_UserWatchlist_AB_pkey\"")

    init {
        index("\"_UserWatchlist_B_index\"", false, b)
    }
}
