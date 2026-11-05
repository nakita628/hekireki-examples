package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object McpIntegrationTable : IdTable<String>("\"McpIntegration\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name").uniqueIndex("\"McpIntegration_name_key\"")
    val registeredServerUrl = text("registeredServerUrl").nullable()
    val registeredAuthorizationUrl = text("registeredAuthorizationUrl").nullable()
    val registeredTokenUrl = text("registeredTokenUrl").nullable()
    val oauthClientId = text("oauthClientId").nullable()
    val oauthClientSecret = text("oauthClientSecret").nullable()
    val displayName = text("displayName").nullable()
    val serverUrl = text("serverUrl").nullable()
    val authType = pgEnum("authType", "\"McpAuthType\"", McpAuthType.entries, McpAuthType::dbName).nullable()
    val emailAccountId =
        optReference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"McpIntegration_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"McpIntegration_pkey\"")

    init {
        index("\"McpIntegration_emailAccountId_idx\"", false, emailAccountId)
    }
}
