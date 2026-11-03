package triggerdev.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationDataStoreEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationDataStoreEntity>(OrganizationDataStoreTable)

    var key by OrganizationDataStoreTable.key
    var organizationIds by OrganizationDataStoreTable.organizationIds
    var kind by OrganizationDataStoreTable.kind
    var config by OrganizationDataStoreTable.config
    var createdAt by OrganizationDataStoreTable.createdAt
    var updatedAt by OrganizationDataStoreTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationDataStoreTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
