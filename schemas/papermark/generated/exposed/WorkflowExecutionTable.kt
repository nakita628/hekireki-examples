package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowExecutionTable : IdTable<String>("\"WorkflowExecution\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val workflowId =
        reference(
            "workflowId",
            WorkflowTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowExecution_workflowId_fkey\"",
        )
    val visitorEmail = text("visitorEmail").nullable()
    val visitorIp = text("visitorIp").nullable()
    val status = pgEnum("status", "\"ExecutionStatus\"", ExecutionStatus.entries, ExecutionStatus::dbName)
    val startedAt = pgTimestamp("startedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val completedAt = pgTimestamp("completedAt", 3).nullable()
    val result = jsonb<String>("result", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"WorkflowExecution_pkey\"")

    init {
        index("\"WorkflowExecution_workflowId_startedAt_idx\"", false, workflowId, startedAt)
        index("\"WorkflowExecution_visitorEmail_idx\"", false, visitorEmail)
        index("\"WorkflowExecution_status_idx\"", false, status)
    }
}
