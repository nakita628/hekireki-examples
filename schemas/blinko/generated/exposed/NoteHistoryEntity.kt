package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class NoteHistoryEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, NoteHistoryEntity>(NoteHistoryTable)

    var noteId by NoteHistoryTable.noteId
    var content by NoteHistoryTable.content
    var metadata by NoteHistoryTable.metadata
    var version by NoteHistoryTable.version
    var accountId by NoteHistoryTable.accountId
    var createdAt by NoteHistoryTable.createdAt
    var note by NotesEntity referencedOn NoteHistoryTable.noteId
}
