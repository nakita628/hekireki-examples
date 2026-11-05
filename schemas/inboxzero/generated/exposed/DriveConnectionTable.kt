package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DriveConnectionTable : IdTable<String>("\"DriveConnection\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val provider = text("provider")
    val email = text("email")
    val accessToken = text("accessToken").nullable()
    val refreshToken = text("refreshToken").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val isConnected = bool("isConnected").default(true)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"DriveConnection_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"DriveConnection_pkey\"")

    init {
        index("\"DriveConnection_emailAccountId_idx\"", false, emailAccountId)
        uniqueIndex("\"DriveConnection_emailAccountId_provider_key\"", emailAccountId, provider)
    }
}
