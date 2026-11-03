package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class NotificationsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, NotificationsEntity>(NotificationsTable)

    var organizationId by NotificationsTable.organizationId
    var content by NotificationsTable.content
    var link by NotificationsTable.link
    var createdAt by NotificationsTable.createdAt
    var updatedAt by NotificationsTable.updatedAt
    var deletedAt by NotificationsTable.deletedAt
    var organization by OrganizationEntity referencedOn NotificationsTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NotificationsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
