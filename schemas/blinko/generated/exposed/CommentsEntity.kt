package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class CommentsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, CommentsEntity>(CommentsTable)

    var content by CommentsTable.content
    var accountId by CommentsTable.accountId
    var guestName by CommentsTable.guestName
    var guestIP by CommentsTable.guestIP
    var guestUA by CommentsTable.guestUA
    var noteId by CommentsTable.noteId
    var parentId by CommentsTable.parentId
    var createdAt by CommentsTable.createdAt
    var updatedAt by CommentsTable.updatedAt
    var note by NotesEntity referencedOn CommentsTable.noteId
    var account by AccountsEntity optionalReferencedOn CommentsTable.accountId
    var parent by CommentsEntity optionalReferencedOn CommentsTable.parentId
    val replies by CommentsEntity optionalReferrersOn CommentsTable.parentId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(CommentsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
