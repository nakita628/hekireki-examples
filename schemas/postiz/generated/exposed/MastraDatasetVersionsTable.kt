package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MastraDatasetVersionsTable : IdTable<String>("mastra_dataset_versions") {
    override val id = text("id").entityId()
    val datasetId = text("datasetId")
    val version = integer("version")
    val createdAt = pgTimestamp("createdAt", 6)
    val createdAtZ =
        pgTimestamptz("createdAtZ", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "mastra_dataset_versions_pkey")

    init {
        index("idx_dataset_versions_dataset_version", false, datasetId, version)
        uniqueIndex("idx_dataset_versions_dataset_version_unique", datasetId, version)
    }
}
