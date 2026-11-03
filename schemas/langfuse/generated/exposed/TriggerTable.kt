package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TriggerTable : IdTable<String>("triggers") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "triggers_project_id_fkey",
        )
    val eventSource = text("eventSource")
    val eventActions = array("eventActions", TextColumnType()).nullable()
    val filter = jsonb<String>("filter", { it }, { it }).nullable()
    val status = pgEnum("status", "\"JobConfigState\"", JobConfigState.entries, JobConfigState::dbName).default(JobConfigState.ACTIVE)
    override val primaryKey = PrimaryKey(id, name = "triggers_pkey")

    init {
        index("triggers_project_id_idx", false, projectId)
    }
}
