package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserApiTokenTable : IdTable<String>("\"UserApiToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastUsed = pgTimestamp("lastUsed", 3).nullable()
    val hint = text("hint")
    val hash = text("hash")
    val userId =
        reference(
            "userId",
            UserProfileTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserApiToken_userId_fkey\"",
        )
    val type = text("type").nullable()
    val name = text("name").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val workspaceId = text("workspaceId").nullable()
    val role = text("role").nullable()
    val oauthClientId =
        optReference(
            "oauthClientId",
            OAuthClientTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserApiToken_oauthClientId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"UserApiToken_pkey\"")
}
