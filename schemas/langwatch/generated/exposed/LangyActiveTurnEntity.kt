package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LangyActiveTurnEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LangyActiveTurnEntity>(LangyActiveTurnTable)

    var projectId by LangyActiveTurnTable.projectId
    var conversationId by LangyActiveTurnTable.conversationId
    var turnId by LangyActiveTurnTable.turnId
    var requestId by LangyActiveTurnTable.requestId
    var userId by LangyActiveTurnTable.userId
    var status by LangyActiveTurnTable.status
    var leaseOwner by LangyActiveTurnTable.leaseOwner
    var leaseExpiresAt by LangyActiveTurnTable.leaseExpiresAt
    var createdAt by LangyActiveTurnTable.createdAt
    var updatedAt by LangyActiveTurnTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LangyActiveTurnTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
