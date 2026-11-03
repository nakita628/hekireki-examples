package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EvaluatorTable : IdTable<String>("evaluators") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluators_project_id_fkey",
        )
    val name = text("name")
    val type = pgEnum("type", "\"EvalTemplateType\"", EvalTemplateType.entries, EvalTemplateType::dbName)
    val isBuiltIn = bool("is_built_in").default(false)
    val description = text("description").nullable()
    val createdByUserId =
        optReference(
            "created_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluators_created_by_user_id_fkey",
        )
    val blockedAt = pgTimestamp("blocked_at", 3).nullable()
    val blockReason =
        pgEnum("block_reason", "\"EvaluatorBlockReason\"", EvaluatorBlockReason.entries, EvaluatorBlockReason::dbName).nullable()
    val blockMessage = text("block_message").nullable()
    override val primaryKey = PrimaryKey(id, name = "evaluators_pkey")

    init {
        index(
            "evaluators_project_id_created_at_idx",
            false,
            functions = listOf(indexExpression("\"project_id\""), indexExpression("\"created_at\" DESC")),
        )
    }
}
