package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object JobExecutionTable : IdTable<String>("job_executions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "job_executions_project_id_fkey",
        )
    val jobConfigurationId = text("job_configuration_id")
    val jobTemplateId = text("job_template_id").nullable()
    val status = pgEnum("status", "\"JobExecutionStatus\"", JobExecutionStatus.entries, JobExecutionStatus::dbName)
    val startTime = pgTimestamp("start_time", 3).nullable()
    val endTime = pgTimestamp("end_time", 3).nullable()
    val error = text("error").nullable()
    val jobInputTraceId = text("job_input_trace_id").nullable()
    val jobInputTraceTimestamp = pgTimestamp("job_input_trace_timestamp", 3).nullable()
    val jobInputObservationId = text("job_input_observation_id").nullable()
    val jobInputDatasetItemId = text("job_input_dataset_item_id").nullable()
    val jobInputDatasetItemValidFrom = pgTimestamp("job_input_dataset_item_valid_from", 3).nullable()
    val jobOutputScoreId = text("job_output_score_id").nullable()
    val executionTraceId = text("execution_trace_id").nullable()
    override val primaryKey = PrimaryKey(id, name = "job_executions_pkey")

    init {
        index("job_executions_project_id_job_configuration_id_job_input_tr_idx", false, projectId, jobConfigurationId, jobInputTraceId)
        index("job_executions_project_id_job_output_score_id_idx", false, projectId, jobOutputScoreId)
        index("job_executions_job_configuration_id_idx", false, jobConfigurationId)
    }
}
