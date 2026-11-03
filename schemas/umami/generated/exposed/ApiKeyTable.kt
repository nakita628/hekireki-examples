package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ApiKeyTable : IdTable<UUID>("api_key") {
    override val id = javaUUID("api_key_id").entityId()
    val userId = javaUUID("user_id")
    val name = varchar("name", 255)
    val keyHash = varchar("key_hash", 128).uniqueIndex("api_key_key_hash_key")
    val keyPrefix = varchar("key_prefix", 20)
    val lastUsedAt = pgTimestamptz("last_used_at", 6).nullable()
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "api_key_pkey")

    init {
        index("api_key_user_id_idx", false, userId)
    }
}
