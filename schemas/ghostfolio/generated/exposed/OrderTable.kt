package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OrderTable : IdTable<String>("\"Order\"") {
    val accountId = text("accountId").nullable()
    val accountUserId = text("accountUserId").nullable()
    val comment = text("comment").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val currency = text("currency").nullable()
    val date = pgTimestamp("date", 3)
    val fee = double("fee")
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val quantity = double("quantity")
    val symbolProfileId =
        reference(
            "symbolProfileId",
            SymbolProfileTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Order_symbolProfileId_fkey\"",
        )
    val type = pgEnum("type", "\"Type\"", Type.entries, Type::dbName)
    val unitPrice = double("unitPrice")
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Order_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Order_pkey\"")

    init {
        index("\"Order_accountId_idx\"", false, accountId)
        index("\"Order_date_idx\"", false, date)
        index("\"Order_symbolProfileId_date_idx\"", false, symbolProfileId, date)
        index("\"Order_userId_date_idx\"", false, userId, date)
        index("\"Order_userId_type_date_idx\"", false, userId, type, date)
        foreignKey(
            accountId to AccountTable.id1,
            accountUserId to AccountTable.userId,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            name = "\"Order_accountId_accountUserId_fkey\"",
        )
    }
}
