package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AutomationExecutionTable : IdTable<String>("automation_executions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val sourceId = text("source_id")
    val automationId =
        reference(
            "automation_id",
            AutomationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "automation_executions_automation_id_fkey",
        )
    val triggerId =
        reference(
            "trigger_id",
            TriggerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "automation_executions_trigger_id_fkey",
        )
    val actionId =
        reference(
            "action_id",
            ActionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "automation_executions_action_id_fkey",
        )
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "automation_executions_project_id_fkey",
        )
    val status =
        pgEnum("status", "\"ActionExecutionStatus\"", ActionExecutionStatus.entries, ActionExecutionStatus::dbName).default(
            ActionExecutionStatus.PENDING,
        )
    val input = jsonb<String>("input", { it }, { it })
    val output = jsonb<String>("output", { it }, { it }).nullable()
    val startedAt = pgTimestamp("started_at", 3).nullable()
    val finishedAt = pgTimestamp("finished_at", 3).nullable()
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(id, name = "automation_executions_pkey")

    init {
        index("automation_executions_trigger_id_idx", false, triggerId)
        index("automation_executions_action_id_idx", false, actionId)
        index("automation_executions_project_id_idx", false, projectId)
    }
}
