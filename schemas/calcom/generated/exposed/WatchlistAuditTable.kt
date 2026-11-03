package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WatchlistAuditTable : IdTable<UUID>("\"WatchlistAudit\"") {
    override val id = javaUUID("id").clientDefault { uuidV7() }.entityId()
    val type = pgEnum("type", "\"WatchlistType\"", WatchlistType.entries, WatchlistType::dbName)
    val value = text("value")
    val description = text("description").nullable()
    val action = pgEnum("action", "\"WatchlistAction\"", WatchlistAction.entries, WatchlistAction::dbName).default(WatchlistAction.REPORT)
    val changedAt = pgTimestamp("changedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val changedByUserId = integer("changedByUserId").nullable()
    val watchlistId =
        optReference(
            "watchlistId",
            WatchlistTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WatchlistAudit_watchlistId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"WatchlistAudit_pkey\"")

    init {
        index("\"WatchlistAudit_watchlistId_changedAt_idx\"", false, watchlistId, changedAt)
    }
}
