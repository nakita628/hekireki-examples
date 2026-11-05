package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object McpConnectionTable : IdTable<String>("\"McpConnection\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name")
    val isActive = bool("isActive").default(true)
    val accessToken = text("accessToken").nullable()
    val refreshToken = text("refreshToken").nullable()
    val apiKey = text("apiKey").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val integrationId =
        reference(
            "integrationId",
            McpIntegrationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"McpConnection_integrationId_fkey\"",
        )
    val emailAccountId =
        optReference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"McpConnection_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"McpConnection_pkey\"")

    init {
        uniqueIndex("\"McpConnection_emailAccountId_integrationId_key\"", emailAccountId, integrationId)
    }
}
