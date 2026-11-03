package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EvalTemplateTable : IdTable<String>("eval_templates") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "eval_templates_project_id_fkey",
        )
    val name = text("name")
    val version = integer("version")
    val prompt = text("prompt").nullable()
    val type =
        pgEnum("type", "\"EvalTemplateType\"", EvalTemplateType.entries, EvalTemplateType::dbName).default(EvalTemplateType.LLM_AS_JUDGE)
    val partner = text("partner").nullable()
    val model = text("model").nullable()
    val provider = text("provider").nullable()
    val modelParams = jsonb<String>("model_params", { it }, { it }).nullable()
    val vars = array("vars", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val outputDefinition = jsonb<String>("output_schema", { it }, { it }).nullable()
    val sourceCode = varchar("source_code", 262144).nullable()
    val sourceCodeLanguage =
        pgEnum(
            "source_code_language",
            "\"EvalTemplateSourceCodeLanguage\"",
            EvalTemplateSourceCodeLanguage.entries,
            EvalTemplateSourceCodeLanguage::dbName,
        ).nullable()
    override val primaryKey = PrimaryKey(id, name = "eval_templates_pkey")

    init {
        index("eval_templates_project_id_id_idx", false, projectId, id)
        uniqueIndex("eval_templates_project_id_name_version_key", projectId, name, version)
    }
}
