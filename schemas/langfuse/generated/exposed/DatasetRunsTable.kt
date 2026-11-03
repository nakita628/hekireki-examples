package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetRunsTable : CompositeIdTable("dataset_runs") {
    val id1 = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId = text("project_id").entityId()
    val name = text("name")
    val description = text("description").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val datasetId = text("dataset_id")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id1, projectId, name = "dataset_runs_pkey")

    init {
        index("dataset_runs_dataset_id_idx", false, datasetId, indexType = "hash")
        index("dataset_runs_created_at_idx", false, createdAt)
        index("dataset_runs_updated_at_idx", false, updatedAt)
        uniqueIndex("dataset_runs_dataset_id_project_id_name_key", datasetId, projectId, name)
        foreignKey(
            datasetId to DatasetTable.id1,
            projectId to DatasetTable.projectId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "dataset_runs_dataset_id_project_id_fkey",
        )
    }
}
