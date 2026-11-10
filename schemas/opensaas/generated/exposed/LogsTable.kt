package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object LogsTable : IdTable<Int>("\"Logs\"") {
    override val id = integer("id").autoIncrement().entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val message = text("message")
    val level = text("level")
    override val primaryKey = PrimaryKey(id, name = "\"Logs_pkey\"")
}
