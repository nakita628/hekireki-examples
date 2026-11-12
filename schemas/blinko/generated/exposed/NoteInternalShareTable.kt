package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object NoteInternalShareTable : IdTable<Int>("\"noteInternalShare\"") {
    override val id = integer("id").autoIncrement().entityId()
    val noteId =
        reference(
            "noteId",
            NotesTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"noteInternalShare_noteId_fkey\"",
        )
    val accountId =
        reference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"noteInternalShare_accountId_fkey\"",
        )
    val canEdit = bool("canEdit").default(true)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"noteInternalShare_pkey\"")

    init {
        index("\"noteInternalShare_noteId_idx\"", false, noteId)
        index("\"noteInternalShare_accountId_idx\"", false, accountId)
        uniqueIndex("\"noteInternalShare_noteId_accountId_key\"", noteId, accountId)
    }
}
