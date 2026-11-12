package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.math.BigDecimal
import java.time.Instant
import java.time.temporal.ChronoUnit

object AttachmentsTable : IdTable<Int>("attachments") {
    override val id = integer("id").autoIncrement().entityId()
    val isShare = bool("isShare").default(false)
    val sharePassword = pgVarchar("sharePassword").default("")
    val name = pgVarchar("name").default("")
    val path = pgVarchar("path").default("")
    val size = pgNumeric("size").default(BigDecimal("0"))
    val type = pgVarchar("type").default("")
    val noteId =
        optReference(
            "noteId",
            NotesTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"attachments_noteId_fkey\"",
        )
    val accountId =
        optReference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"attachments_accountId_fkey\"",
        )
    val sortOrder = integer("sortOrder").default(0)
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val perfixPath = pgVarchar("perfixPath").nullable().default("")
    val depth = integer("depth").nullable()
    val metadata = json<String>("metadata", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "attachments_pkey")
}
