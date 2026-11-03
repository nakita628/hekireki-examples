package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.json
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object DatasetTable : CompositeIdTable("datasets") {
    val id1 = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "datasets_project_id_fkey",
        )
    val name = text("name")
    val description = text("description").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val remoteExperimentUrl = text("remote_experiment_url").nullable()
    val remoteExperimentPayload = jsonb<String>("remote_experiment_payload", { it }, { it }).nullable()
    val remoteExperimentEnabled = bool("remote_experiment_enabled").default(true)
    val remoteExperimentSecretKey = text("remote_experiment_secret_key").nullable()
    val remoteExperimentDisplaySecretKey = text("remote_experiment_display_secret_key").nullable()
    val remoteExperimentRequestHeaders = jsonb<String>("remote_experiment_request_headers", { it }, { it }).nullable()
    val remoteExperimentDisplayHeaders = jsonb<String>("remote_experiment_display_headers", { it }, { it }).nullable()
    val inputSchema = json<String>("input_schema", { it }, { it }).nullable()
    val expectedOutputSchema = json<String>("expected_output_schema", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id1, projectId, name = "datasets_pkey")

    init {
        addIdColumn(projectId)
        index("datasets_created_at_idx", false, createdAt)
        index("datasets_updated_at_idx", false, updatedAt)
        uniqueIndex("datasets_project_id_name_key", projectId, name)
    }
}
