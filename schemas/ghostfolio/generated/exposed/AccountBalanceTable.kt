package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AccountBalanceTable : IdTable<String>("\"AccountBalance\"") {
    val accountId = text("accountId")
    val comment = text("comment").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val date = pgTimestamp("date", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId = text("userId")
    val value = double("value")
    override val primaryKey = PrimaryKey(id, name = "\"AccountBalance_pkey\"")

    init {
        index("\"AccountBalance_accountId_idx\"", false, accountId)
        index("\"AccountBalance_date_idx\"", false, date)
        uniqueIndex("\"AccountBalance_accountId_userId_date_key\"", accountId, userId, date)
        foreignKey(
            accountId to AccountTable.id1,
            userId to AccountTable.userId,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"AccountBalance_accountId_userId_fkey\"",
        )
    }
}
