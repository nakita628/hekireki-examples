package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.core.dao.id.EntityID

object TagsToNoteTable : CompositeIdTable("\"tagsToNote\"") {
    val id1 = integer("id").autoIncrement()
    val noteId =
        reference(
            "noteId",
            NotesTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"tagsToNote_noteId_fkey\"",
        ).default(EntityID(0, NotesTable))
    val tagId =
        reference(
            "tagId",
            TagTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"tagsToNote_tagId_fkey\"",
        ).default(EntityID(0, TagTable))
    override val primaryKey = PrimaryKey(noteId, tagId, name = "\"tagsToNote_pkey\"")

    init {
        addIdColumn(noteId)
        addIdColumn(tagId)
    }
}
