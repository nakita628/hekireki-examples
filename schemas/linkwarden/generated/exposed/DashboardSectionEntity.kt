package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DashboardSectionEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, DashboardSectionEntity>(DashboardSectionTable)

    var user by UserEntity referencedOn DashboardSectionTable.userId
    var userId by DashboardSectionTable.userId
    var collection by CollectionEntity optionalReferencedOn DashboardSectionTable.collectionId
    var collectionId by DashboardSectionTable.collectionId
    var type by DashboardSectionTable.type
    var order by DashboardSectionTable.order
    var createdAt by DashboardSectionTable.createdAt
    var updatedAt by DashboardSectionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DashboardSectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
