package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object CommentsTable : IdTable<Int>("comments") {
    override val id = integer("id").autoIncrement().entityId()
    val content = text("content")
    val accountId =
        optReference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"comments_accountId_fkey\"",
        )
    val guestName = pgVarchar("guestName").nullable()
    val guestIP = pgVarchar("guestIP").nullable()
    val guestUA = pgVarchar("guestUA").nullable()
    val noteId =
        reference(
            "noteId",
            NotesTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"comments_noteId_fkey\"",
        )
    val parentId =
        optReference(
            "parentId",
            CommentsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"comments_parentId_fkey\"",
        )
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "comments_pkey")
}
