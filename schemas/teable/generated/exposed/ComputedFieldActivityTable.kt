package teable.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ComputedFieldActivityTable : IdTable<String>("computed_field_activity") {
    override val id = text("field_id").entityId()
    val tableId = text("table_id")
    val baseId = text("base_id")
    val status = text("status")
    val activeTaskCount = integer("active_task_count").default(0)
    val processingTaskCount = integer("processing_task_count").default(0)
    val generation = long("generation").default(0L)
    val estimatedComplexity = long("estimated_complexity").default(0L)
    val estimatedDirtyRecords = long("estimated_dirty_records").default(0L)
    val hasAllTargetRecords = bool("has_all_target_records").default(false)
    val queuedAt = pgTimestamp("queued_at", 3).nullable()
    val startedAt = pgTimestamp("started_at", 3).nullable()
    val lastCompletedAt = pgTimestamp("last_completed_at", 3).nullable()
    val lastDurationMs = integer("last_duration_ms").nullable()
    val lastError = jsonb<String>("last_error", { it }, { it }).nullable()
    val extensions = jsonb<String>("extensions", { it }, { it }).nullable()
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "computed_field_activity_pkey")

    init {
        index("computed_field_activity_table_id_status_idx", false, tableId, status)
        index("computed_field_activity_base_id_status_idx", false, baseId, status)
    }
}
