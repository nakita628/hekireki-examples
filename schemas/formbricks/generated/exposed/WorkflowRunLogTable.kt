package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb

object WorkflowRunLogTable : IdTable<String>("\"WorkflowRunLog\"") {
    override val id = text("id").clientDefault { CUID.randomCUID2().toString() }.entityId()
    val runId =
        reference(
            "runId",
            WorkflowRunTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowRunLog_runId_fkey\"",
        )
    val sequence = integer("sequence")
    val stepId = text("stepId")
    val stepType = text("stepType")
    val status =
        pgEnum("status", "\"WorkflowRunLogStatus\"", WorkflowRunLogStatus.entries, WorkflowRunLogStatus::dbName).default(
            WorkflowRunLogStatus.PENDING,
        )
    val input = jsonb<String>("input", { it }, { it }).default("{}")
    val output = jsonb<String>("output", { it }, { it }).default("{}")
    val error = text("error").nullable()
    val startedAt = pgTimestamp("startedAt", 3).nullable()
    val finishedAt = pgTimestamp("finishedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"WorkflowRunLog_pkey\"")

    init {
        index("\"WorkflowRunLog_runId_sequence_idx\"", false, runId, sequence)
        uniqueIndex("\"WorkflowRunLog_runId_stepId_key\"", runId, stepId)
    }
}
