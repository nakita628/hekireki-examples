package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.json
import java.time.Instant
import java.time.temporal.ChronoUnit

object NotificationsTable : IdTable<Int>("notifications") {
    override val id = integer("id").autoIncrement().entityId()
    val type = pgVarchar("type")
    val title = pgVarchar("title")
    val content = text("content")
    val metadata = json<String>("metadata", { it }, { it }).nullable()
    val isRead = bool("isRead").default(false)
    val accountId =
        reference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"notifications_accountId_fkey\"",
        )
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "notifications_pkey")
}
