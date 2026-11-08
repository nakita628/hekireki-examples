package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WorkflowTransitionTable : IdTable<String>("workflow_transitions") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val fromStepId =
        reference(
            "fromStepId",
            WorkflowStepTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_transitions_fromStepId_fkey\"",
        )
    val toStepId =
        reference(
            "toStepId",
            WorkflowStepTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflow_transitions_toStepId_fkey\"",
        )
    val condition = jsonb<String>("condition", { it }, { it }).nullable()
    val priority = integer("priority").default(0)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "workflow_transitions_pkey")

    init {
        index("\"workflow_transitions_fromStepId_idx\"", false, fromStepId)
        index("\"workflow_transitions_toStepId_idx\"", false, toStepId)
    }
}
