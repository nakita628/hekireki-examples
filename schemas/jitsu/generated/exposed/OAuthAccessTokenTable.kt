package jitsu.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OAuthAccessTokenTable : IdTable<String>("\"OAuthAccessToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastUsed = pgTimestamp("lastUsed", 3).nullable()
    val hash = text("hash")
    val expiresAt = pgTimestamp("expiresAt", 3)
    val refreshTokenId =
        reference(
            "refreshTokenId",
            UserApiTokenTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OAuthAccessToken_refreshTokenId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"OAuthAccessToken_pkey\"")

    init {
        index("\"OAuthAccessToken_expiresAt_idx\"", false, expiresAt)
        index("\"OAuthAccessToken_refreshTokenId_idx\"", false, refreshTokenId)
    }
}
