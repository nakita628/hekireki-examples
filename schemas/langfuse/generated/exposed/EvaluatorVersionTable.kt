package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EvaluatorVersionTable : IdTable<String>("evaluator_versions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val evaluatorId =
        reference(
            "evaluator_id",
            EvaluatorTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluator_versions_evaluator_id_fkey",
        )
    val version = integer("version")
    val createdByUserId =
        optReference(
            "created_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "evaluator_versions_created_by_user_id_fkey",
        )
    val prompt = text("prompt").nullable()
    val promptMessages = jsonb<String>("prompt_messages", { it }, { it }).nullable()
    val partner = text("partner").nullable()
    val model = text("model").nullable()
    val provider = text("provider").nullable()
    val modelParams = jsonb<String>("model_params", { it }, { it }).nullable()
    val vars = array("vars", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val variableMapping = jsonb<String>("variable_mapping", { it }, { it }).nullable()
    val outputDefinition = jsonb<String>("output_definition", { it }, { it }).nullable()
    val sourceCode = varchar("source_code", 262144).nullable()
    val sourceCodeLanguage =
        pgEnum(
            "source_code_language",
            "\"EvaluatorSourceCodeLanguage\"",
            EvaluatorSourceCodeLanguage.entries,
            EvaluatorSourceCodeLanguage::dbName,
        ).nullable()
    val questions = jsonb<String>("questions", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "evaluator_versions_pkey")

    init {
        uniqueIndex(
            "evaluator_versions_evaluator_id_version_key",
            functions = listOf(indexExpression("\"evaluator_id\""), indexExpression("\"version\" DESC")),
        )
    }
}
