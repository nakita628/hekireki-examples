package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WorkflowStepTable : IdTable<String>("workflow_steps") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val type = pgEnum("type", "\"WorkflowStepType\"", WorkflowStepType.entries, WorkflowStepType::dbName)
    val name = text("name")
    val position = jsonb<String>("position", { it }, { it })
    val config = jsonb<String>("config", { it }, { it })
    val workflowId =
        reference(
            "workflowId",
            WorkflowTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_steps_workflowId_fkey\"",
        )
    val templateId =
        optReference(
            "templateId",
            TemplateTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_steps_templateId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "workflow_steps_pkey")

    init {
        index("\"workflow_steps_workflowId_idx\"", false, workflowId)
        index("\"workflow_steps_templateId_idx\"", false, templateId)
    }
}
