package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object MessagesGroupTable : IdTable<String>("\"MessagesGroup\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val buyerOrganizationId =
        reference(
            "buyerOrganizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MessagesGroup_buyerOrganizationId_fkey\"",
        )
    val buyerId =
        reference(
            "buyerId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MessagesGroup_buyerId_fkey\"",
        )
    val sellerId =
        reference(
            "sellerId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MessagesGroup_sellerId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"MessagesGroup_pkey\"")

    init {
        index("\"MessagesGroup_createdAt_idx\"", false, createdAt)
        index("\"MessagesGroup_updatedAt_idx\"", false, updatedAt)
        index("\"MessagesGroup_buyerOrganizationId_idx\"", false, buyerOrganizationId)
        uniqueIndex("\"MessagesGroup_buyerId_sellerId_key\"", buyerId, sellerId)
    }
}
