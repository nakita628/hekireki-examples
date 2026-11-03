package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class BoardEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, BoardEntity>(BoardTable)

    var type by BoardTable.type
    var name by BoardTable.name
    var description by BoardTable.description
    var parameters by BoardTable.parameters
    var userId by BoardTable.userId
    var teamId by BoardTable.teamId
    var createdAt by BoardTable.createdAt
    var updatedAt by BoardTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BoardTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
