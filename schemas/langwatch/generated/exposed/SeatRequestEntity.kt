package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SeatRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SeatRequestEntity>(SeatRequestTable)

    var organizationId by SeatRequestTable.organizationId
    var email by SeatRequestTable.email
    var userId by SeatRequestTable.userId
    var name by SeatRequestTable.name
    var status by SeatRequestTable.status
    var source by SeatRequestTable.source1
    var idpRefused by SeatRequestTable.idpRefused
    var idpKind by SeatRequestTable.idpKind
    var salesNotifiedAt by SeatRequestTable.salesNotifiedAt
    var openedAt by SeatRequestTable.openedAt
    var resolvedAt by SeatRequestTable.resolvedAt
    var resolvedByUserId by SeatRequestTable.resolvedByUserId
    var seatPendingSince by SeatRequestTable.seatPendingSince
    var createdAt by SeatRequestTable.createdAt
    var updatedAt by SeatRequestTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SeatRequestTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
