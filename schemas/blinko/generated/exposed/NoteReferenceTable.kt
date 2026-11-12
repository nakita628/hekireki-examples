package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object NoteReferenceTable : IdTable<Int>("\"noteReference\"") {
    override val id = integer("id").autoIncrement().entityId()
    val fromNoteId =
        reference(
            "fromNoteId",
            NotesTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"noteReference_fromNoteId_fkey\"",
        )
    val toNoteId =
        reference(
            "toNoteId",
            NotesTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"noteReference_toNoteId_fkey\"",
        )
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"noteReference_pkey\"")

    init {
        uniqueIndex("\"noteReference_fromNoteId_toNoteId_key\"", fromNoteId, toNoteId)
    }
}
