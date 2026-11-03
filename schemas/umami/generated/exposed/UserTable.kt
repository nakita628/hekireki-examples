package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UserTable : IdTable<UUID>("user") {
    override val id = javaUUID("user_id").entityId()
    val username = varchar("username", 255).uniqueIndex("user_username_key")
    val password = varchar("password", 60)
    val role = varchar("role", 50)
    val logoUrl = varchar("logo_url", 2183).nullable()
    val displayName = varchar("display_name", 255).nullable()
    val twoFactorRequired = bool("two_factor_required").default(false)
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamptz("deleted_at", 6).nullable()
    override val primaryKey = PrimaryKey(id, name = "user_pkey")
}
