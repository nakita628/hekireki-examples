package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object JobConfigurationTable : IdTable<String>("job_configurations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "job_configurations_project_id_fkey",
        )
    val jobType = pgEnum("job_type", "\"JobType\"", JobType.entries, JobType::dbName)
    val status = pgEnum("status", "\"JobConfigState\"", JobConfigState.entries, JobConfigState::dbName).default(JobConfigState.ACTIVE)
    val blockedAt = pgTimestamp("blocked_at", 3).nullable()
    val blockReason =
        pgEnum("block_reason", "\"EvaluatorBlockReason\"", EvaluatorBlockReason.entries, EvaluatorBlockReason::dbName).nullable()
    val blockMessage = text("block_message").nullable()
    val evalTemplateId =
        optReference(
            "eval_template_id",
            EvalTemplateTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "job_configurations_eval_template_id_fkey",
        )
    val scoreName = text("score_name")
    val filter = jsonb<String>("filter", { it }, { it })
    val targetObject = text("target_object")
    val variableMapping = jsonb<String>("variable_mapping", { it }, { it })
    val sampling = decimal("sampling", 65, 30)
    val delay = integer("delay")
    val timeScope = array("time_scope", TextColumnType()).nullable().databaseDefault("'{\"NEW\"}'") { listOf("NEW") }
    override val primaryKey = PrimaryKey(id, name = "job_configurations_pkey")

    init {
        index("job_configurations_project_id_id_idx", false, projectId, id)
    }
}
