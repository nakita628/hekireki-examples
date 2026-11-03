package papermark.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RestrictedTokenTable : IdTable<String>("\"RestrictedToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val hashedKey = text("hashedKey").uniqueIndex("\"RestrictedToken_hashedKey_key\"")
    val partialKey = text("partialKey")
    val scopes = text("scopes").nullable()
    val mode = text("mode").default("live")
    val source1 = text("source").default("dashboard")
    val subjectType = text("subjectType").default("user")
    val clientId =
        optReference(
            "clientId",
            OAuthClientTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RestrictedToken_clientId_fkey\"",
        )
    val expires = pgTimestamp("expires", 3).nullable()
    val lastUsed = pgTimestamp("lastUsed", 3).nullable()
    val rateLimit = integer("rateLimit").default(60)
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RestrictedToken_userId_fkey\"",
        )
    val teamId =
        reference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RestrictedToken_teamId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RestrictedToken_pkey\"")

    init {
        index("\"RestrictedToken_userId_idx\"", false, userId)
        index("\"RestrictedToken_teamId_idx\"", false, teamId)
        index("\"RestrictedToken_hashedKey_idx\"", false, hashedKey)
        index("\"RestrictedToken_clientId_idx\"", false, clientId)
    }
}
