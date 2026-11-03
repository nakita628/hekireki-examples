package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object ChartTable : IdTable<String>("\"Chart\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val type = pgEnum("type", "\"ChartType\"", ChartType.entries, ChartType::dbName)
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chart_workspaceId_fkey\"",
        )
    val query = jsonb<String>("query", { it }, { it }).default("{}")
    val config = jsonb<String>("config", { it }, { it }).default("{}")
    val createdBy =
        optReference(
            "createdBy",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chart_createdBy_fkey\"",
        )
    val feedbackDirectoryId =
        reference(
            "feedbackDirectoryId",
            FeedbackDirectoryTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Chart_feedbackDirectoryId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Chart_pkey\"")

    init {
        index("\"Chart_workspaceId_created_at_idx\"", false, workspaceId, createdAt)
        index("\"Chart_feedbackDirectoryId_idx\"", false, feedbackDirectoryId)
        uniqueIndex("\"Chart_workspaceId_name_key\"", workspaceId, name)
    }
}
