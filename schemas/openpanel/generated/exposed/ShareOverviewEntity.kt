package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ShareOverviewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ShareOverviewEntity>(ShareOverviewTable)

    var projectId by ShareOverviewTable.projectId
    var project by ProjectEntity referencedOn ShareOverviewTable.projectId
    var organization by OrganizationEntity referencedOn ShareOverviewTable.organizationId
    var organizationId by ShareOverviewTable.organizationId
    var public by ShareOverviewTable.public
    var password by ShareOverviewTable.password
    var createdAt by ShareOverviewTable.createdAt
    var updatedAt by ShareOverviewTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ShareOverviewTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
