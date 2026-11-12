package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class NoteReferenceEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, NoteReferenceEntity>(NoteReferenceTable)

    var fromNoteId by NoteReferenceTable.fromNoteId
    var toNoteId by NoteReferenceTable.toNoteId
    var createdAt by NoteReferenceTable.createdAt
    var fromNote by NotesEntity referencedOn NoteReferenceTable.fromNoteId
    var toNote by NotesEntity referencedOn NoteReferenceTable.toNoteId
}
