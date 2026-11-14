package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvoiceItemTable : IdTable<String>("\"InvoiceItem\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val invoiceId = text("invoiceId")
    val description = text("description")
    val amount = double("amount")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"InvoiceItem_pkey\"")

    init {
        index("\"InvoiceItem_invoiceId_idx\"", false, invoiceId)
    }
}
