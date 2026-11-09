package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TaskLogTable : IdTable<UUID>("task_log") {
    override val id = javaUUID("id").clientDefault { UUID.randomUUID() }.entityId()
    val level = text("level")
    val logger = text("logger")
    val message = text("message")
    val syncId = text("sync_id")
    val taskId = text("task_id")
    val timestamp = pgTimestamptz("timestamp", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "task_log_pkey")

    init {
        index("task_log_sync_id_idx", false, syncId)
        index("task_log_task_id_idx", false, taskId)
        index("task_log_timestamp_idx", false, timestamp)
    }
}
