package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object WorkflowStepTable : IdTable<String>("\"WorkflowStep\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val workflowId =
        reference(
            "workflowId",
            WorkflowTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WorkflowStep_workflowId_fkey\"",
        )
    val name = text("name")
    val stepOrder = integer("stepOrder")
    val stepType =
        pgEnum("stepType", "\"WorkflowStepType\"", WorkflowStepType.entries, WorkflowStepType::dbName).default(WorkflowStepType.ROUTER)
    val conditions = jsonb<String>("conditions", { it }, { it })
    val actions = jsonb<String>("actions", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WorkflowStep_pkey\"")

    init {
        index("\"WorkflowStep_workflowId_idx\"", false, workflowId)
        uniqueIndex("\"WorkflowStep_workflowId_stepOrder_key\"", workflowId, stepOrder)
    }
}
