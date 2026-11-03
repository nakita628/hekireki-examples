package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OutOfOfficeEntryEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, OutOfOfficeEntryEntity>(OutOfOfficeEntryTable)

    var uuid by OutOfOfficeEntryTable.uuid
    var start by OutOfOfficeEntryTable.start
    var end by OutOfOfficeEntryTable.end
    var notes by OutOfOfficeEntryTable.notes
    var showNotePublicly by OutOfOfficeEntryTable.showNotePublicly
    var userId by OutOfOfficeEntryTable.userId
    var user by UserEntity referencedOn OutOfOfficeEntryTable.userId
    var toUserId by OutOfOfficeEntryTable.toUserId
    var toUser by UserEntity optionalReferencedOn OutOfOfficeEntryTable.toUserId
    var reasonId by OutOfOfficeEntryTable.reasonId
    var reason by OutOfOfficeReasonEntity optionalReferencedOn OutOfOfficeEntryTable.reasonId
    var createdAt by OutOfOfficeEntryTable.createdAt
    var updatedAt by OutOfOfficeEntryTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OutOfOfficeEntryTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
