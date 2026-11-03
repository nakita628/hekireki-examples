package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiKeyTable : IdTable<String>("api_keys") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val note = text("note").nullable()
    val publicKey = text("public_key").uniqueIndex("api_keys_public_key_key")
    val hashedSecretKey = text("hashed_secret_key").uniqueIndex("api_keys_hashed_secret_key_key")
    val fastHashedSecretKey = text("fast_hashed_secret_key").nullable().uniqueIndex("api_keys_fast_hashed_secret_key_key")
    val displaySecretKey = text("display_secret_key")
    val lastUsedAt = pgTimestamp("last_used_at", 3).nullable()
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val isInAppAgentKey = bool("is_in_app_agent_key").default(false)
    val projectId =
        optReference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_keys_project_id_fkey",
        )
    val orgId =
        optReference(
            "organization_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_keys_organization_id_fkey",
        )
    val scope = pgEnum("scope", "\"ApiKeyScope\"", ApiKeyScope.entries, ApiKeyScope::dbName).default(ApiKeyScope.PROJECT)
    val createdByUserId =
        optReference(
            "created_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_keys_created_by_user_id_fkey",
        )
    val userId =
        optReference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_keys_user_id_fkey",
        )
    val createdByApiKeyId =
        optReference(
            "created_by_api_key_id",
            ApiKeyTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_keys_created_by_api_key_id_fkey",
        )
    override val primaryKey = PrimaryKey(id, name = "api_keys_pkey")

    init {
        uniqueIndex("api_keys_id_key", id)
        index("api_keys_organization_id_idx", false, orgId)
        index("api_keys_project_id_idx", false, projectId)
        index("api_keys_created_by_api_key_id_idx", false, createdByApiKeyId)
        index("api_keys_user_id_organization_id_idx", false, userId, orgId)
        index("api_keys_public_key_idx", false, publicKey)
        index("api_keys_hashed_secret_key_idx", false, hashedSecretKey)
        index("api_keys_fast_hashed_secret_key_idx", false, fastHashedSecretKey)
    }
}
