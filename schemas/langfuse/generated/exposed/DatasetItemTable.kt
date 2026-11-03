package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetItemTable : CompositeIdTable("dataset_items") {
    val id1 = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId = text("project_id").entityId()
    val status =
        pgEnum("status", "\"DatasetStatus\"", DatasetStatus.entries, DatasetStatus::dbName).nullable().default(DatasetStatus.ACTIVE)
    val input = jsonb<String>("input", { it }, { it }).nullable()
    val expectedOutput = jsonb<String>("expected_output", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val sourceTraceId = text("source_trace_id").nullable()
    val sourceObservationId = text("source_observation_id").nullable()
    val datasetId = text("dataset_id")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val validFrom =
        pgTimestamp("valid_from", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }.entityId()
    val validTo = pgTimestamp("valid_to", 3).nullable()
    val isDeleted = bool("is_deleted").default(false)
    override val primaryKey = PrimaryKey(id1, projectId, validFrom, name = "dataset_items_pkey")

    init {
        index("dataset_items_project_id_valid_to_idx", false, projectId, validTo)
        index("dataset_items_project_id_id_valid_from_idx", false, projectId, id1, validFrom)
        index("dataset_items_source_trace_id_idx", false, sourceTraceId, indexType = "hash")
        index("dataset_items_source_observation_id_idx", false, sourceObservationId, indexType = "hash")
        index("dataset_items_dataset_id_idx", false, datasetId, indexType = "hash")
        index("dataset_items_created_at_idx", false, createdAt)
        index("dataset_items_updated_at_idx", false, updatedAt)
        foreignKey(
            datasetId to DatasetTable.id1,
            projectId to DatasetTable.projectId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "dataset_items_dataset_id_project_id_fkey",
        )
    }
}
