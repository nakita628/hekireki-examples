package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TraceEditOverlayTable : IdTable<String>("\"TraceEditOverlay\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val traceId = text("traceId")
    val patch = jsonb<String>("patch", { it }, { it })
    val createdById = text("createdById").nullable()
    val updatedById = text("updatedById").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TraceEditOverlay_pkey\"")

    init {
        index("\"TraceEditOverlay_projectId_idx\"", false, projectId)
        uniqueIndex("\"TraceEditOverlay_projectId_traceId_key\"", projectId, traceId)
    }
}
