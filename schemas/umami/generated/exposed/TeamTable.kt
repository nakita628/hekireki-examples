package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object TeamTable : IdTable<UUID>("team") {
    override val id = javaUUID("team_id").entityId()
    val name = varchar("name", 50)
    val accessCode = varchar("access_code", 50).nullable().uniqueIndex("team_access_code_key")
    val logoUrl = varchar("logo_url", 2183).nullable()
    val twoFactorRequired = bool("two_factor_required").default(false)
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamptz("deleted_at", 6).nullable()
    override val primaryKey = PrimaryKey(id, name = "team_pkey")

    init {
        index("team_access_code_idx", false, accessCode)
    }
}
