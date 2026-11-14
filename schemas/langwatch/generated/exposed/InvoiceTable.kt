package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvoiceTable : IdTable<String>("\"Invoice\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val subscriptionId = text("subscriptionId")
    val totalAmount = double("totalAmount")
    val currency = text("currency")
    val dueDate = pgTimestamp("dueDate", 3)
    val paid = bool("paid").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Invoice_pkey\"")

    init {
        index("\"Invoice_subscriptionId_idx\"", false, subscriptionId)
    }
}
