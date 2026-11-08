package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object IdempotencyKeyTable : IdTable<String>("idempotency_keys") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val key = text("key")
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"idempotency_keys_projectId_fkey\"",
        )
    val method = text("method")
    val path = text("path")
    val statusCode = integer("statusCode").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    override val primaryKey = PrimaryKey(id, name = "idempotency_keys_pkey")

    init {
        index("\"idempotency_keys_expiresAt_idx\"", false, expiresAt)
        uniqueIndex("\"idempotency_keys_projectId_key_key\"", projectId, key)
    }
}
