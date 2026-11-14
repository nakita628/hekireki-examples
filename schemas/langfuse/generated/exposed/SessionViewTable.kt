package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SessionViewTable : IdTable<String>("session_views") {
    val isPinnedByDefaultForProject = bool("is_pinned_by_default_for_project").default(false)
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "session_views_project_id_fkey",
        )
    val name = text("name")
    val filters = jsonb<String>("filters", { it }, { it })
    val viewMode = pgEnum("view_mode", "\"SessionViewMode\"", SessionViewMode.entries, SessionViewMode::dbName)
    val unmatchedTraces =
        pgEnum("unmatched_traces", "\"SessionUnmatchedTraces\"", SessionUnmatchedTraces.entries, SessionUnmatchedTraces::dbName)
    val createdBy =
        optReference(
            "created_by",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "session_views_created_by_fkey",
        )
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "session_views_pkey")

    init {
        uniqueIndex("session_views_project_id_name_key", projectId, name)
        uniqueIndex("session_views_project_id_id_key", projectId, id)
    }
}
