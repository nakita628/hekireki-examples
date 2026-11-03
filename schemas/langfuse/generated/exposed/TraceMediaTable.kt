package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TraceMediaTable : IdTable<String>("trace_media") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId = text("project_id")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val mediaId = text("media_id")
    val traceId = text("trace_id")
    val field = text("field")
    val origin =
        pgEnum("origin", "\"MediaAssociationOrigin\"", MediaAssociationOrigin.entries, MediaAssociationOrigin::dbName).default(
            MediaAssociationOrigin.UNKNOWN,
        )
    override val primaryKey = PrimaryKey(id, name = "trace_media_pkey")

    init {
        index("trace_media_project_id_media_id_idx", false, projectId, mediaId)
        uniqueIndex("trace_media_project_id_trace_id_media_id_field_key", projectId, traceId, mediaId, field)
    }
}
