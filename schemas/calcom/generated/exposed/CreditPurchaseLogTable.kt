package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object CreditPurchaseLogTable : IdTable<String>("\"CreditPurchaseLog\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val creditBalanceId =
        reference(
            "creditBalanceId",
            CreditBalanceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreditPurchaseLog_creditBalanceId_fkey\"",
        )
    val credits = integer("credits")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"CreditPurchaseLog_pkey\"")
}
