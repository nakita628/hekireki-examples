package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object NoteHistoryTable : IdTable<Int>("\"noteHistory\"") {
    override val id = integer("id").autoIncrement().entityId()
    val noteId =
        reference(
            "noteId",
            NotesTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"noteHistory_noteId_fkey\"",
        )
    val content = text("content")
    val metadata = json<String>("metadata", { it }, { it }).nullable()
    val version = integer("version")
    val accountId = integer("accountId").nullable()
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"noteHistory_pkey\"")

    init {
        index("\"noteHistory_noteId_idx\"", false, noteId)
        index("\"noteHistory_accountId_idx\"", false, accountId)
    }
}
