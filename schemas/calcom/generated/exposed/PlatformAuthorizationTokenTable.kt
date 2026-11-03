package calcom.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PlatformAuthorizationTokenTable : IdTable<String>("\"PlatformAuthorizationToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val platformOAuthClientId =
        reference(
            "platformOAuthClientId",
            PlatformOAuthClientTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PlatformAuthorizationToken_platformOAuthClientId_fkey\"",
        )
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PlatformAuthorizationToken_userId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"PlatformAuthorizationToken_pkey\"")

    init {
        uniqueIndex("\"PlatformAuthorizationToken_userId_platformOAuthClientId_key\"", userId, platformOAuthClientId)
    }
}
