package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowStepLogTable : IdTable<String>("\"WorkflowStepLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val executionId =
        reference(
            "executionId",
            WorkflowExecutionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowStepLog_executionId_fkey\"",
        )
    val workflowStepId =
        reference(
            "workflowStepId",
            WorkflowStepTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowStepLog_workflowStepId_fkey\"",
        )
    val conditionsMatched = bool("conditionsMatched")
    val conditionResults = jsonb<String>("conditionResults", { it }, { it }).nullable()
    val actionsExecuted = jsonb<String>("actionsExecuted", { it }, { it }).nullable()
    val executedAt = pgTimestamp("executedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val duration = integer("duration").nullable()
    val error = text("error").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"WorkflowStepLog_pkey\"")

    init {
        index("\"WorkflowStepLog_executionId_idx\"", false, executionId)
        index("\"WorkflowStepLog_workflowStepId_idx\"", false, workflowStepId)
    }
}
