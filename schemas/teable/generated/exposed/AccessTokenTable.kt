package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AccessTokenTable : IdTable<String>("access_token") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val userId = text("user_id")
    val scopes = text("scopes")
    val spaceIds = text("space_ids").nullable()
    val baseIds = text("base_ids").nullable()
    val sign = text("sign")
    val clientId = text("client_id").nullable()
    val hasFullAccess = bool("has_full_access").nullable()
    val expiredTime = pgTimestamp("expired_time", 3)
    val lastUsedTime = pgTimestamp("last_used_time", 3).nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "access_token_pkey")

    init {
        index("access_token_user_id_idx", false, userId)
        index("access_token_client_id_idx", false, clientId)
    }
}
