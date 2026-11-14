package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConnectedInvoiceTable : IdTable<String>("\"ConnectedInvoice\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val accountId = text("accountId")
    val stripeInvoiceId = text("stripeInvoiceId").uniqueIndex("\"ConnectedInvoice_stripeInvoiceId_key\"")
    val kind = pgEnum("kind", "\"ConnectedInvoiceKind\"", ConnectedInvoiceKind.entries, ConnectedInvoiceKind::dbName)
    val currency = pgEnum("currency", "\"Currency\"", Currency.entries, Currency::dbName)
    val amountCents = integer("amountCents")
    val status = text("status")
    val paidOutOfBandAt = pgTimestamp("paidOutOfBandAt", 3).nullable()
    val termStartsAt = pgTimestamp("termStartsAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConnectedInvoice_pkey\"")

    init {
        index("\"ConnectedInvoice_accountId_idx\"", false, accountId)
    }
}
