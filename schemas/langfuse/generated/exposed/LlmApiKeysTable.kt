package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object LlmApiKeysTable : IdTable<String>("llm_api_keys") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val provider = text("provider")
    val adapter = text("adapter")
    val displaySecretKey = text("display_secret_key")
    val secretKey = text("secret_key")
    val baseURL = text("base_url").nullable()
    val customModels = array("custom_models", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val withDefaultModels = bool("with_default_models").default(true)
    val extraHeaders = text("extra_headers").nullable()
    val extraHeaderKeys = array("extra_header_keys", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val config = jsonb<String>("config", { it }, { it }).nullable()
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "llm_api_keys_project_id_fkey",
        )
    val organizationId =
        optReference(
            "organization_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "llm_api_keys_organization_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "llm_api_keys_pkey")

    init {
        uniqueIndex("llm_api_keys_id_key", id)
        uniqueIndex("llm_api_keys_project_id_provider_key", projectId, provider)
        uniqueIndex("llm_api_keys_organization_id_provider_key", organizationId, provider)
    }
}
