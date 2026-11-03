package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AutomationTable : IdTable<String>("automations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val triggerId =
        reference(
            "trigger_id",
            TriggerTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "automations_trigger_id_fkey",
        )
    val actionId =
        reference(
            "action_id",
            ActionTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "automations_action_id_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "automations_project_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "automations_pkey")

    init {
        index("automations_project_id_action_id_trigger_id_idx", false, projectId, actionId, triggerId)
        index("automations_project_id_name_idx", false, projectId, name)
    }
}
