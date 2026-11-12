package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AttachmentsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, AttachmentsEntity>(AttachmentsTable)

    var isShare by AttachmentsTable.isShare
    var sharePassword by AttachmentsTable.sharePassword
    var name by AttachmentsTable.name
    var path by AttachmentsTable.path
    var size by AttachmentsTable.size
    var type by AttachmentsTable.type
    var noteId by AttachmentsTable.noteId
    var accountId by AttachmentsTable.accountId
    var sortOrder by AttachmentsTable.sortOrder
    var createdAt by AttachmentsTable.createdAt
    var updatedAt by AttachmentsTable.updatedAt
    var perfixPath by AttachmentsTable.perfixPath
    var depth by AttachmentsTable.depth
    var metadata by AttachmentsTable.metadata
    var note by NotesEntity optionalReferencedOn AttachmentsTable.noteId
    var account by AccountsEntity optionalReferencedOn AttachmentsTable.accountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AttachmentsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
