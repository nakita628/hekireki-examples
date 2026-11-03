package teable.models

import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object ComputedUpdateDeadLetterTable : IdTable<String>("computed_update_dead_letter") {
    override val id = text("id").entityId()
    val baseId = text("base_id")
    val seedTableId = text("seed_table_id")
    val seedRecordIds = jsonb<String>("seed_record_ids", { it }, { it }).nullable()
    val changeType = text("change_type")
    val steps = jsonb<String>("steps", { it }, { it }).nullable()
    val edges = jsonb<String>("edges", { it }, { it }).nullable()
    val status = text("status")
    val attempts = integer("attempts").default(0)
    val maxAttempts = integer("max_attempts").default(8)
    val nextRunAt = pgTimestamp("next_run_at", 3)
    val lockedAt = pgTimestamp("locked_at", 3).nullable()
    val lockedBy = text("locked_by").nullable()
    val lastError = text("last_error").nullable()
    val estimatedComplexity = integer("estimated_complexity").default(0)
    val planHash = text("plan_hash")
    val dirtyStats = jsonb<String>("dirty_stats", { it }, { it }).nullable()
    val runId = text("run_id")
    val originRunIds = array("origin_run_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val runTotalSteps = integer("run_total_steps").default(0)
    val runCompletedStepsBefore = integer("run_completed_steps_before").default(0)
    val affectedTableIds = array("affected_table_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val affectedFieldIds = array("affected_field_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val syncMaxLevel = integer("sync_max_level").nullable()
    val sourceChangedAt = pgTimestamp("source_changed_at", 3).nullable()
    val stageDepth = integer("stage_depth").default(0)
    val predecessorTaskId = text("predecessor_task_id").nullable()
    val traceData = jsonb<String>("trace_data", { it }, { it }).nullable()
    val failedAt = pgTimestamp("failed_at", 3)
    val createdAt = pgTimestamp("created_at", 3)
    val updatedAt = pgTimestamp("updated_at", 3)
    override val primaryKey = PrimaryKey(id, name = "computed_update_dead_letter_pkey")

    init {
        index("computed_update_dead_letter_base_id_seed_table_id_idx", false, baseId, seedTableId)
        index("computed_update_dead_letter_plan_hash_idx", false, planHash)
        index("computed_update_dead_letter_run_id_idx", false, runId)
    }
}
