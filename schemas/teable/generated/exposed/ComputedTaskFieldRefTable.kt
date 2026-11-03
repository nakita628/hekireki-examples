package teable.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ComputedTaskFieldRefTable : CompositeIdTable("computed_task_field_ref") {
    val taskId = text("task_id").entityId()
    val fieldId = text("field_id").entityId()
    val tableId = text("table_id")
    val baseId = text("base_id")
    val wasProcessing = bool("was_processing").default(false)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(taskId, fieldId, name = "computed_task_field_ref_pkey")

    init {
        index("computed_task_field_ref_field_id_idx", false, fieldId)
        index("computed_task_field_ref_table_id_idx", false, tableId)
    }
}
