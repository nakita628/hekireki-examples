package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetItemMediaTable : IdTable<String>("dataset_item_media") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId = text("project_id")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val mediaId = text("media_id")
    val datasetId = text("dataset_id")
    val datasetItemId = text("dataset_item_id")
    val datasetItemValidFrom = pgTimestamp("dataset_item_valid_from", 3).nullable()
    val field = text("field")
    val jsonPath = text("json_path").nullable()
    val referenceString = text("reference_string").nullable()
    override val primaryKey = PrimaryKey(id, name = "dataset_item_media_pkey")

    init {
        index("dataset_item_media_project_id_media_id_idx", false, projectId, mediaId)
        index("dataset_item_media_project_id_dataset_id_idx", false, projectId, datasetId)
        uniqueIndex("dataset_item_media_item_version_field_path_key", projectId, datasetItemId, datasetItemValidFrom, field, jsonPath)
    }
}
