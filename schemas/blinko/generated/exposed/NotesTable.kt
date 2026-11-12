package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object NotesTable : IdTable<Int>("notes") {
    override val id = integer("id").autoIncrement().entityId()
    val type = integer("type").default(0)
    val content = pgVarchar("content").default("")
    val isArchived = bool("isArchived").default(false)
    val isRecycle = bool("isRecycle").default(false)
    val isShare = bool("isShare").default(false)
    val isTop = bool("isTop").default(false)
    val isReviewed = bool("isReviewed").default(false)
    val sharePassword = pgVarchar("sharePassword").default("")
    val shareEncryptedUrl = pgVarchar("shareEncryptedUrl").nullable()
    val shareExpiryDate = pgTimestamptz("shareExpiryDate", 6).nullable()
    val shareMaxView = integer("shareMaxView").nullable().default(0)
    val shareViewCount = integer("shareViewCount").nullable().default(0)
    val metadata = json<String>("metadata", { it }, { it }).nullable()
    val accountId =
        optReference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"notes_accountId_fkey\"",
        )
    val sortOrder = integer("sortOrder").default(0)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "notes_pkey")
}
