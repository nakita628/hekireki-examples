package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TraceSessionTable : CompositeIdTable("trace_sessions") {
    val id1 = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "trace_sessions_project_id_fkey",
        )
    val bookmarked = bool("bookmarked").default(false)
    val public = bool("public").default(false)
    val environment = text("environment").default("default")
    override val primaryKey = PrimaryKey(id1, projectId, name = "trace_sessions_pkey")

    init {
        addIdColumn(projectId)
        index(
            "trace_sessions_project_id_created_at_idx",
            false,
            functions = listOf(indexExpression("\"project_id\""), indexExpression("\"created_at\" DESC")),
        )
    }
}
