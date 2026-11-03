package formbricks.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SegmentTable : IdTable<String>("\"Segment\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val title = text("title")
    val description = text("description").nullable()
    val isPrivate = bool("isPrivate").default(true)
    val filters = jsonb<String>("filters", { it }, { it }).default("[]")
    val workspaceId =
        reference(
            "workspaceId",
            WorkspaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Segment_workspaceId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Segment_pkey\"")

    init {
        index("\"Segment_workspaceId_idx\"", false, workspaceId)
        uniqueIndex("\"Segment_workspaceId_title_key\"", workspaceId, title)
    }
}
