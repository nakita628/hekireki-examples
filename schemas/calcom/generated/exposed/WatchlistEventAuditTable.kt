package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WatchlistEventAuditTable : IdTable<UUID>("\"WatchlistEventAudit\"") {
    override val id = javaUUID("id").clientDefault { uuidV7() }.entityId()
    val watchlistId = javaUUID("watchlistId")
    val eventTypeId = integer("eventTypeId")
    val actionTaken = pgEnum("actionTaken", "\"WatchlistAction\"", WatchlistAction.entries, WatchlistAction::dbName)
    val timestamp = pgTimestamp("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WatchlistEventAudit_pkey\"")
}
