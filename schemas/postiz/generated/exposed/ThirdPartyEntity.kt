package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ThirdPartyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ThirdPartyEntity>(ThirdPartyTable)

    var organizationId by ThirdPartyTable.organizationId
    var identifier by ThirdPartyTable.identifier
    var name by ThirdPartyTable.name
    var internalId by ThirdPartyTable.internalId
    var apiKey by ThirdPartyTable.apiKey
    var createdAt by ThirdPartyTable.createdAt
    var updatedAt by ThirdPartyTable.updatedAt
    var deletedAt by ThirdPartyTable.deletedAt
    var organization by OrganizationEntity referencedOn ThirdPartyTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ThirdPartyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
