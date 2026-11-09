package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object StatusChangeTable : IdTable<Long>("\"StatusChange\"") {
    override val id = long("id").autoIncrement().entityId()
    val workspaceId = text("workspaceId")
    val actorId = text("actorId")
    val type = text("type")
    val tableName1 = text("tableName")
    val timestamp = pgTimestamptz("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status = text("status")
    val startedAt = pgTimestamp("startedAt", 3)
    val counts = integer("counts").default(0)
    val description = text("description").nullable()
    val queueSize = integer("queueSize").default(0)
    override val primaryKey = PrimaryKey(id, name = "\"StatusChange_pkey\"")

    init {
        index("\"StatusChange_actorId_type_tableName_idx\"", false, actorId, type, tableName1)
        index("\"StatusChange_timestamp_idx\"", false, timestamp)
        index("\"StatusChange_startedAt_idx\"", false, startedAt)
    }
}
