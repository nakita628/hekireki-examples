package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class NoteInternalShareEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, NoteInternalShareEntity>(NoteInternalShareTable)

    var noteId by NoteInternalShareTable.noteId
    var accountId by NoteInternalShareTable.accountId
    var canEdit by NoteInternalShareTable.canEdit
    var createdAt by NoteInternalShareTable.createdAt
    var updatedAt by NoteInternalShareTable.updatedAt
    var note by NotesEntity referencedOn NoteInternalShareTable.noteId
    var account by AccountsEntity referencedOn NoteInternalShareTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NoteInternalShareTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
