package teable.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ComputedUpdateRunHistoryTable : IdTable<String>("computed_update_run_history") {
    override val id = text("task_id").entityId()
    val baseId = text("base_id")
    val seedTableId = text("seed_table_id")
    val changeType = text("change_type")
    val runId = text("run_id")
    val originRunIds = array("origin_run_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val steps = jsonb<String>("steps", { it }, { it }).nullable()
    val edges = jsonb<String>("edges", { it }, { it }).nullable()
    val affectedTableIds = array("affected_table_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val affectedFieldIds = array("affected_field_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val sourceFieldIds = array("source_field_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val seedRecordCount = integer("seed_record_count").default(0)
    val stageDepth = integer("stage_depth").default(0)
    val predecessorTaskId = text("predecessor_task_id").nullable()
    val runTotalSteps = integer("run_total_steps").default(0)
    val runCompletedStepsBefore = integer("run_completed_steps_before").default(0)
    val syncMaxLevel = integer("sync_max_level").nullable()
    val estimatedComplexity = integer("estimated_complexity").default(0)
    val attempts = integer("attempts").default(0)
    val outcome = text("outcome")
    val sourceChangedAt = pgTimestamp("source_changed_at", 3).nullable()
    val enqueuedAt = pgTimestamp("enqueued_at", 3)
    val startedAt = pgTimestamp("started_at", 3).nullable()
    val completedAt = pgTimestamp("completed_at", 3)
    val durationMs = integer("duration_ms").default(0)
    override val primaryKey = PrimaryKey(id, name = "computed_update_run_history_pkey")

    init {
        index("computed_update_run_history_run_id_idx", false, runId)
        index("computed_update_run_history_base_id_completed_at_idx", false, baseId, completedAt)
        index("computed_update_run_history_completed_at_idx", false, completedAt)
        index("computed_update_run_history_origin_run_ids_gin", false, originRunIds, indexType = "gin")
    }
}
