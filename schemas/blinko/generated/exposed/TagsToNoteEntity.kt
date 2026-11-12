package blinko.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class TagsToNoteEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<TagsToNoteEntity>(TagsToNoteTable)

    var id1 by TagsToNoteTable.id1
    val noteId by TagsToNoteTable.noteId
    val tagId by TagsToNoteTable.tagId
    val note by NotesEntity referencedOn TagsToNoteTable.noteId
    val tag by TagEntity referencedOn TagsToNoteTable.tagId
}
