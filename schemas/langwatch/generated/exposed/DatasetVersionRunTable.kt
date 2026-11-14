package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetVersionRunTable : IdTable<String>("\"DatasetVersionRun\"") {
    override val id = text("id").entityId()
    val projectId = text("projectId")
    val datasetId = text("datasetId")
    val versionId = text("versionId")
    val runId = text("runId")
    val experimentId = text("experimentId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DatasetVersionRun_pkey\"")

    init {
        index("\"DatasetVersionRun_versionId_idx\"", false, versionId)
        uniqueIndex("\"DatasetVersionRun_projectId_runId_datasetId_key\"", projectId, runId, datasetId)
    }
}
