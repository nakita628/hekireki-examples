package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceEntity>(SpaceTable)

    var createdAt by SpaceTable.createdAt
    var updatedAt by SpaceTable.updatedAt
    var name by SpaceTable.name
    var icon by SpaceTable.icon
    var workspaceId by SpaceTable.workspaceId
    var workspace by WorkspaceEntity referencedOn SpaceTable.workspaceId
    val typebots by TypebotEntity optionalReferrersOn TypebotTable.spaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
