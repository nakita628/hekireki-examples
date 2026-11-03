package teable.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SchemaOperationTable : IdTable<String>("schema_operation") {
    override val id = text("id").entityId()
    val type = text("type")
    val status = text("status")
    val phase = text("phase")
    val resourceType = text("resource_type")
    val resourceId = text("resource_id")
    val baseId = text("base_id").nullable()
    val tableId = text("table_id").nullable()
    val idempotencyKey = text("idempotency_key").uniqueIndex("schema_operation_idempotency_key_key")
    val payload = jsonb<String>("payload", { it }, { it }).nullable()
    val result = jsonb<String>("result", { it }, { it }).nullable()
    val attempts = integer("attempts").default(0)
    val maxAttempts = integer("max_attempts").default(8)
    val nextRunAt = pgTimestamptz("next_run_at", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lockedAt = pgTimestamptz("locked_at", 6).nullable()
    val lockedBy = text("locked_by").nullable()
    val lastError = text("last_error").nullable()
    val createdTime = pgTimestamptz("created_time", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamptz("last_modified_time", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "schema_operation_pkey")

    init {
        index("schema_operation_status_next_run_at_idx", false, status, nextRunAt)
        index("schema_operation_resource_status_idx", false, resourceType, resourceId, status)
        index("schema_operation_base_status_idx", false, baseId, status)
        index("schema_operation_table_status_idx", false, tableId, status)
    }
}
