package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LangyTurnRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LangyTurnRequestEntity>(LangyTurnRequestTable)

    var projectId by LangyTurnRequestTable.projectId
    var userId by LangyTurnRequestTable.userId
    var requestId by LangyTurnRequestTable.requestId
    var conversationId by LangyTurnRequestTable.conversationId
    var turnId by LangyTurnRequestTable.turnId
    var status by LangyTurnRequestTable.status
    var leaseOwner by LangyTurnRequestTable.leaseOwner
    var leaseExpiresAt by LangyTurnRequestTable.leaseExpiresAt
    var createdAt by LangyTurnRequestTable.createdAt
    var updatedAt by LangyTurnRequestTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LangyTurnRequestTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
