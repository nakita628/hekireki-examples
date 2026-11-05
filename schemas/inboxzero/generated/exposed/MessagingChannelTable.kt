package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MessagingChannelTable : IdTable<String>("\"MessagingChannel\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val provider = pgEnum("provider", "\"MessagingProvider\"", MessagingProvider.entries, MessagingProvider::dbName)
    val isConnected = bool("isConnected").default(true)
    val teamId = text("teamId")
    val teamName = text("teamName").nullable()
    val providerUserId = text("providerUserId").nullable()
    val botUserId = text("botUserId").nullable()
    val accessToken = text("accessToken").nullable()
    val refreshToken = text("refreshToken").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MessagingChannel_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"MessagingChannel_pkey\"")

    init {
        index("\"MessagingChannel_emailAccountId_idx\"", false, emailAccountId)
        index("\"MessagingChannel_provider_teamId_idx\"", false, provider, teamId)
        uniqueIndex("\"MessagingChannel_emailAccountId_provider_teamId_key\"", emailAccountId, provider, teamId)
        uniqueIndex("\"MessagingChannel_id_emailAccountId_key\"", id, emailAccountId)
    }
}
