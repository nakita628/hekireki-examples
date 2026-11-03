package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class TeamEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, TeamEntity>(TeamTable)

    var name by TeamTable.name
    var accessCode by TeamTable.accessCode
    var logoUrl by TeamTable.logoUrl
    var twoFactorRequired by TeamTable.twoFactorRequired
    var createdAt by TeamTable.createdAt
    var updatedAt by TeamTable.updatedAt
    var deletedAt by TeamTable.deletedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
