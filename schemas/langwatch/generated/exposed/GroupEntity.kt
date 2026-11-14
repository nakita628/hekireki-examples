package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class GroupEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, GroupEntity>(GroupTable)

    var organizationId by GroupTable.organizationId
    var name by GroupTable.name
    var slug by GroupTable.slug
    var externalId by GroupTable.externalId
    var scimSource by GroupTable.scimSource
    var scimConnectionId by GroupTable.scimConnectionId
    var createdAt by GroupTable.createdAt
    var updatedAt by GroupTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GroupTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
