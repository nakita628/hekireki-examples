package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CustomerEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CustomerEntity>(CustomerTable)

    var name by CustomerTable.name
    var orgId by CustomerTable.orgId
    var createdAt by CustomerTable.createdAt
    var updatedAt by CustomerTable.updatedAt
    var deletedAt by CustomerTable.deletedAt
    var organization by OrganizationEntity referencedOn CustomerTable.orgId
    val integrations by IntegrationEntity optionalReferrersOn IntegrationTable.customerId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CustomerTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
