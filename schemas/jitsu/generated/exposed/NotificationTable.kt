package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object NotificationTable : IdTable<UUID>("\"Notification\"") {
    override val id = javaUUID("id").clientDefault { UUID.randomUUID() }.entityId()
    val workspaceId = text("workspaceId")
    val actorId = text("actorId")
    val type = text("type")
    val tableName1 = text("tableName")
    val channelId = text("channelId")
    val statusChangeId = long("statusChangeId")
    val timestamp = pgTimestamptz("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status = text("status")
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Notification_pkey\"")

    init {
        index("\"Notification_actorId_type_tableName_idx\"", false, actorId, type, tableName1)
        index("\"Notification_timestamp_idx\"", false, timestamp)
    }
}
