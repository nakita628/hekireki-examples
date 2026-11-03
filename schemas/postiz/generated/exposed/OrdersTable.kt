package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OrdersTable : IdTable<String>("\"Orders\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val buyerId =
        reference(
            "buyerId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Orders_buyerId_fkey\"",
        )
    val sellerId =
        reference(
            "sellerId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Orders_sellerId_fkey\"",
        )
    val status = pgEnum("status", "\"OrderStatus\"", OrderStatus.entries, OrderStatus::dbName)
    val messageGroupId =
        reference(
            "messageGroupId",
            MessagesGroupTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Orders_messageGroupId_fkey\"",
        )
    val captureId = text("captureId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Orders_pkey\"")

    init {
        index("\"Orders_buyerId_idx\"", false, buyerId)
        index("\"Orders_sellerId_idx\"", false, sellerId)
        index("\"Orders_updatedAt_idx\"", false, updatedAt)
        index("\"Orders_createdAt_idx\"", false, createdAt)
        index("\"Orders_messageGroupId_idx\"", false, messageGroupId)
    }
}
