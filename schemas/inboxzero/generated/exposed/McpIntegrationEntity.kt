package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class McpIntegrationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, McpIntegrationEntity>(McpIntegrationTable)

    var createdAt by McpIntegrationTable.createdAt
    var updatedAt by McpIntegrationTable.updatedAt
    var name by McpIntegrationTable.name
    var registeredServerUrl by McpIntegrationTable.registeredServerUrl
    var registeredAuthorizationUrl by McpIntegrationTable.registeredAuthorizationUrl
    var registeredTokenUrl by McpIntegrationTable.registeredTokenUrl
    var oauthClientId by McpIntegrationTable.oauthClientId
    var oauthClientSecret by McpIntegrationTable.oauthClientSecret
    var displayName by McpIntegrationTable.displayName
    var serverUrl by McpIntegrationTable.serverUrl
    var authType by McpIntegrationTable.authType
    var emailAccountId by McpIntegrationTable.emailAccountId
    var emailAccount by EmailAccountEntity optionalReferencedOn McpIntegrationTable.emailAccountId
    val connections by McpConnectionEntity referrersOn McpConnectionTable.integrationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(McpIntegrationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
