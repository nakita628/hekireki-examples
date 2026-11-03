package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class PixelEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, PixelEntity>(PixelTable)

    var name by PixelTable.name
    var slug by PixelTable.slug
    var userId by PixelTable.userId
    var teamId by PixelTable.teamId
    var createdAt by PixelTable.createdAt
    var updatedAt by PixelTable.updatedAt
    var deletedAt by PixelTable.deletedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PixelTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
