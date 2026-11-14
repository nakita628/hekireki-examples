package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetRecordTable : IdTable<String>("\"DatasetRecord\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val datasetId = text("datasetId")
    val projectId = text("projectId")
    val entry = jsonb<String>("entry", { it }, { it })
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DatasetRecord_pkey\"")

    init {
        index("\"DatasetRecord_datasetId_idx\"", false, datasetId)
        index("\"DatasetRecord_projectId_idx\"", false, projectId)
        index("\"DatasetRecord_createdAt_idx\"", false, createdAt)
        index("\"DatasetRecord_projectId_datasetId_idx\"", false, projectId, datasetId)
    }
}
