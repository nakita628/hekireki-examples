package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SetsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SetsEntity>(SetsTable)

    var organizationId by SetsTable.organizationId
    var name by SetsTable.name
    var content by SetsTable.content
    var createdAt by SetsTable.createdAt
    var updatedAt by SetsTable.updatedAt
    var organization by OrganizationEntity referencedOn SetsTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SetsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
