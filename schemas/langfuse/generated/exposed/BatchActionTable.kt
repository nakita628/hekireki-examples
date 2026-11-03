package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object BatchActionTable : IdTable<String>("batch_actions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "batch_actions_project_id_fkey",
        )
    val userId = text("user_id")
    val actionType = text("action_type")
    val tableName1 = text("table_name")
    val status = text("status")
    val finishedAt = pgTimestamp("finished_at", 3).nullable()
    val query = jsonb<String>("query", { it }, { it })
    val config = jsonb<String>("config", { it }, { it }).nullable()
    val totalCount = integer("total_count").nullable()
    val processedCount = integer("processed_count").nullable()
    val failedCount = integer("failed_count").nullable()
    val log = text("log").nullable()
    override val primaryKey = PrimaryKey(id, name = "batch_actions_pkey")

    init {
        index("batch_actions_project_id_user_id_idx", false, projectId, userId)
        index("batch_actions_status_idx", false, status)
        index("batch_actions_project_id_action_type_idx", false, projectId, actionType)
    }
}
