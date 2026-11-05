package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class McpConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, McpConnectionEntity>(McpConnectionTable)

    var createdAt by McpConnectionTable.createdAt
    var updatedAt by McpConnectionTable.updatedAt
    var name by McpConnectionTable.name
    var isActive by McpConnectionTable.isActive
    var accessToken by McpConnectionTable.accessToken
    var refreshToken by McpConnectionTable.refreshToken
    var apiKey by McpConnectionTable.apiKey
    var expiresAt by McpConnectionTable.expiresAt
    var integrationId by McpConnectionTable.integrationId
    var integration by McpIntegrationEntity referencedOn McpConnectionTable.integrationId
    var emailAccountId by McpConnectionTable.emailAccountId
    var emailAccount by EmailAccountEntity optionalReferencedOn McpConnectionTable.emailAccountId
    val tools by McpToolEntity referrersOn McpToolTable.connectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(McpConnectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
