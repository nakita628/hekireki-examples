package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class NotesEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, NotesEntity>(NotesTable)

    var type by NotesTable.type
    var content by NotesTable.content
    var isArchived by NotesTable.isArchived
    var isRecycle by NotesTable.isRecycle
    var isShare by NotesTable.isShare
    var isTop by NotesTable.isTop
    var isReviewed by NotesTable.isReviewed
    var sharePassword by NotesTable.sharePassword
    var shareEncryptedUrl by NotesTable.shareEncryptedUrl
    var shareExpiryDate by NotesTable.shareExpiryDate
    var shareMaxView by NotesTable.shareMaxView
    var shareViewCount by NotesTable.shareViewCount
    var metadata by NotesTable.metadata
    var accountId by NotesTable.accountId
    var sortOrder by NotesTable.sortOrder
    var createdAt by NotesTable.createdAt
    var updatedAt by NotesTable.updatedAt
    val attachments by AttachmentsEntity optionalReferrersOn AttachmentsTable.noteId
    val tags by TagsToNoteEntity referrersOn TagsToNoteTable.noteId
    var account by AccountsEntity optionalReferencedOn NotesTable.accountId
    val referencedBy by NoteReferenceEntity referrersOn NoteReferenceTable.toNoteId
    val references by NoteReferenceEntity referrersOn NoteReferenceTable.fromNoteId
    val comments by CommentsEntity referrersOn CommentsTable.noteId
    val histories by NoteHistoryEntity referrersOn NoteHistoryTable.noteId
    val internalShares by NoteInternalShareEntity referrersOn NoteInternalShareTable.noteId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(NotesTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
