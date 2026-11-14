package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetVersionTable : IdTable<String>("\"DatasetVersion\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val datasetId = text("datasetId")
    val version = integer("version")
    val name = text("name").nullable()
    val trigger = text("trigger")
    val contentHash = text("contentHash")
    val columnTypes = jsonb<String>("columnTypes", { it }, { it })
    val rowCount = integer("rowCount")
    val sizeBytes = long("sizeBytes")
    val chunkCount = integer("chunkCount")
    val restoredFromVersion = integer("restoredFromVersion").nullable()
    val authorId = text("authorId").nullable()
    val authorKind = text("authorKind").nullable()
    val pinnedAt = pgTimestamp("pinnedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DatasetVersion_pkey\"")

    init {
        index("\"DatasetVersion_projectId_datasetId_idx\"", false, projectId, datasetId)
        uniqueIndex("\"DatasetVersion_datasetId_version_key\"", datasetId, version)
    }
}
