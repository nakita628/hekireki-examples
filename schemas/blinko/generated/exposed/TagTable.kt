package blinko.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TagTable : IdTable<Int>("tag") {
    override val id = integer("id").autoIncrement().entityId()
    val name = pgVarchar("name").default("")
    val icon = pgVarchar("icon").default("")
    val parent = integer("parent").default(0)
    val accountId =
        optReference(
            "accountId",
            AccountsTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"tag_accountId_fkey\"",
        )
    val createdAt = pgTimestamptz("createdAt", 6).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updatedAt", 6).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val sortOrder = integer("sortOrder").default(0)
    override val primaryKey = PrimaryKey(id, name = "tag_pkey")
}
