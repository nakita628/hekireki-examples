package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object TopicClusteringRunTable : IdTable<String>("topic_clustering_runs") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "topic_clustering_runs_project_id_fkey",
        )
    val facetId = text("facet_id")
    val facetVersion = integer("facet_version")
    val status = text("status").default("pending")
    val config = jsonb<String>("config", { it }, { it }).default("{}")
    val error = text("error").nullable()
    val createdAt = pgTimestamptz("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val startedAt = pgTimestamptz("started_at", 3).nullable()
    val finishedAt = pgTimestamptz("finished_at", 3).nullable()
    val topicVersionIds = array("topic_version_ids", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val ruleId = text("rule_id").nullable()
    val batchActionId = text("batch_action_id").nullable()
    override val primaryKey = PrimaryKey(id, name = "topic_clustering_runs_pkey")

    init {
        index(
            "topic_clustering_runs_serving_map_idx",
            false,
            functions =
                listOf(
                    indexExpression("\"project_id\""),
                    indexExpression("\"facet_id\""),
                    indexExpression("\"status\""),
                    indexExpression("\"facet_version\" DESC"),
                    indexExpression("\"created_at\" DESC"),
                    indexExpression("\"id\" DESC"),
                ),
        )
        index("topic_clustering_runs_rule_serving_map_idx", false, projectId, ruleId, facetId, status)
        index("topic_clustering_runs_project_id_batch_action_id_idx", false, projectId, batchActionId)
        foreignKey(
            facetId to EvaluatorVersionTable.evaluatorId,
            facetVersion to EvaluatorVersionTable.version,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "topic_clustering_runs_facet_id_facet_version_fkey",
        )
    }
}
