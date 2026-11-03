package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GatewayAiConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GatewayAiConnectionEntity>(GatewayAiConnectionTable)

    var organizationId by GatewayAiConnectionTable.organizationId
    var organization by OrganizationEntity referencedOn GatewayAiConnectionTable.organizationId
    var name by GatewayAiConnectionTable.name
    var provider by GatewayAiConnectionTable.provider
    var encryptedCredential by GatewayAiConnectionTable.encryptedCredential
    var displaySecret by GatewayAiConnectionTable.displaySecret
    var createdById by GatewayAiConnectionTable.createdById
    var status by GatewayAiConnectionTable.status
    var allowedModels by GatewayAiConnectionTable.allowedModels
    var createdAt by GatewayAiConnectionTable.createdAt
    var updatedAt by GatewayAiConnectionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GatewayAiConnectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
