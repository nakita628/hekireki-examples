package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class LinkEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, LinkEntity>(LinkTable)

    var name by LinkTable.name
    var url by LinkTable.url
    var slug by LinkTable.slug
    var userId by LinkTable.userId
    var teamId by LinkTable.teamId
    var createdAt by LinkTable.createdAt
    var updatedAt by LinkTable.updatedAt
    var deletedAt by LinkTable.deletedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LinkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
