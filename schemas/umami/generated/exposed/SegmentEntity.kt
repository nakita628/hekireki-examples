package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class SegmentEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, SegmentEntity>(SegmentTable)

    var websiteId by SegmentTable.websiteId
    var type by SegmentTable.type
    var name by SegmentTable.name
    var parameters by SegmentTable.parameters
    var createdAt by SegmentTable.createdAt
    var updatedAt by SegmentTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SegmentTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
