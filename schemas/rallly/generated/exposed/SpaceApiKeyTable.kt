package rallly.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SpaceApiKeyTable : IdTable<String>("space_api_keys") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "space_api_keys_space_id_fkey",
        )
    val name = text("name")
    val prefix = text("prefix").uniqueIndex("space_api_keys_prefix_key")
    val hashedKey = text("hashed_key")
    val lastUsedAt = pgTimestamp("last_used_at", 3).nullable()
    val expiresAt = pgTimestamp("expires_at", 3).nullable()
    val revokedAt = pgTimestamp("revoked_at", 3).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "space_api_keys_pkey")

    init {
        index("space_api_key_space_idx", false, spaceId)
        index("space_api_key_expires_idx", false, expiresAt)
        index("space_api_key_revoked_idx", false, revokedAt)
    }
}
