package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiKeyTable : IdTable<String>("api_keys") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val keyHash = text("key_hash").uniqueIndex("api_keys_key_hash_key")
    val lastFour = text("last_four")
    val runtimeEnvironmentId =
        reference(
            "runtime_environment_id",
            RuntimeEnvironmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_keys_runtime_environment_id_fkey",
        )
    val createdByUserId =
        optReference(
            "created_by_user_id",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "api_keys_created_by_user_id_fkey",
        )
    val presetId = text("preset_id").nullable()
    val scopes = array("scopes", TextColumnType()).nullable()
    val lastUsedAt = pgTimestamp("last_used_at", 3).nullable()
    val revokedAt = pgTimestamp("revoked_at", 3).nullable()
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "api_keys_pkey")

    init {
        index(
            "api_keys_runtime_environment_id_revoked_at_created_at_idx",
            false,
            functions =
                listOf(
                    indexExpression("\"runtime_environment_id\""),
                    indexExpression("\"revoked_at\""),
                    indexExpression("\"created_at\" DESC"),
                ),
        )
    }
}
