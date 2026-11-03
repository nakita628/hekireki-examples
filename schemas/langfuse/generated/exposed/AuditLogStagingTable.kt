package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AuditLogStagingTable : IdTable<UUID>("audit_log_staging") {
    override val id = javaUUID("id").entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val payload = jsonb<String>("payload", { it }, { it })
    val failedAt = pgTimestamp("failed_at", 3).nullable()
    val lastError = text("last_error").nullable()
    override val primaryKey = PrimaryKey(id, name = "audit_log_staging_pkey")
}
