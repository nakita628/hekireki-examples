package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WatchlistTable : IdTable<UUID>("\"Watchlist\"") {
    override val id = javaUUID("id").clientDefault { UUID.randomUUID() }.entityId()
    val type = pgEnum("type", "\"WatchlistType\"", WatchlistType.entries, WatchlistType::dbName)
    val value = text("value")
    val description = text("description").nullable()
    val isGlobal = bool("isGlobal").default(false)
    val organizationId = integer("organizationId").nullable()
    val action = pgEnum("action", "\"WatchlistAction\"", WatchlistAction.entries, WatchlistAction::dbName).default(WatchlistAction.REPORT)
    val source1 = pgEnum("source", "\"WatchlistSource\"", WatchlistSource.entries, WatchlistSource::dbName).default(WatchlistSource.MANUAL)
    val lastUpdatedAt =
        pgTimestamp("lastUpdatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Watchlist_pkey\"")

    init {
        index("\"Watchlist_isGlobal_action_organizationId_type_value_idx\"", false, isGlobal, action, organizationId, type, value)
        uniqueIndex("\"Watchlist_type_value_organizationId_key\"", type, value, organizationId)
    }
}
