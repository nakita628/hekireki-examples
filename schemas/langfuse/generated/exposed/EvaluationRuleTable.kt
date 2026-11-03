package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EvaluationRuleTable : IdTable<String>("evaluation_rules") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluation_rules_project_id_fkey",
        )
    val createdByUserId =
        optReference(
            "created_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluation_rules_created_by_user_id_fkey",
        )
    val name = text("name")
    val status = pgEnum("status", "\"JobConfigState\"", JobConfigState.entries, JobConfigState::dbName).default(JobConfigState.ACTIVE)
    val targetObject = text("target_object")
    val filter = jsonb<String>("filter", { it }, { it })
    val sampling = decimal("sampling", 65, 30)
    val delay = integer("delay")
    val idleTime = integer("idle_time").nullable()
    val timeScope = array("time_scope", TextColumnType()).nullable().databaseDefault("'{\"NEW\"}'") { listOf("NEW") }
    override val primaryKey = PrimaryKey(id, name = "evaluation_rules_pkey")

    init {
        index(
            "evaluation_rules_project_id_updated_at_idx",
            false,
            functions = listOf(indexExpression("\"project_id\""), indexExpression("\"updated_at\" DESC")),
        )
    }
}
