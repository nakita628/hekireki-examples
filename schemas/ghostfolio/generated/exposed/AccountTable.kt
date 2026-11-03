package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object AccountTable : CompositeIdTable("\"Account\"") {
    val comment = text("comment").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val currency = text("currency").nullable()
    val id1 = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name").nullable()
    val platformId =
        optReference(
            "platformId",
            PlatformTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Account_platformId_fkey\"",
        )
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Account_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id1, userId, name = "\"Account_pkey\"")

    init {
        addIdColumn(userId)
        index("\"Account_currency_idx\"", false, currency)
        index("\"Account_id_idx\"", false, id1)
        index("\"Account_name_idx\"", false, name)
        index("\"Account_userId_idx\"", false, userId)
    }
}
