package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WorkflowTable : IdTable<String>("workflows") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val enabled = bool("enabled").default(false)
    val triggerType = pgEnum("triggerType", "\"WorkflowTriggerType\"", WorkflowTriggerType.entries, WorkflowTriggerType::dbName)
    val triggerConfig = jsonb<String>("triggerConfig", { it }, { it }).nullable()
    val allowReentry = bool("allowReentry").default(false)
    val throttleWindowSeconds = integer("throttleWindowSeconds").nullable()
    val throttleKey = text("throttleKey").nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"workflows_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "workflows_pkey")

    init {
        index("\"workflows_projectId_idx\"", false, projectId)
        index("\"workflows_projectId_enabled_idx\"", false, projectId, enabled)
    }
}
