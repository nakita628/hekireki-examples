package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class WebsiteEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, WebsiteEntity>(WebsiteTable)

    var name by WebsiteTable.name
    var domain by WebsiteTable.domain
    var resetAt by WebsiteTable.resetAt
    var userId by WebsiteTable.userId
    var teamId by WebsiteTable.teamId
    var createdBy by WebsiteTable.createdBy
    var createdAt by WebsiteTable.createdAt
    var updatedAt by WebsiteTable.updatedAt
    var deletedAt by WebsiteTable.deletedAt
    var recorderEnabled by WebsiteTable.recorderEnabled
    var replayConfig by WebsiteTable.replayConfig

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WebsiteTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
