package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConnectedSeatChangeTable : IdTable<String>("\"ConnectedSeatChange\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val licenseId = text("licenseId").uniqueIndex("\"ConnectedSeatChange_licenseId_key\"")
    val accountId = text("accountId").nullable()
    val organizationId = text("organizationId").nullable()
    val changedAt = pgTimestamp("changedAt", 3)
    val addedSeats = integer("addedSeats")
    val unitAmountCents = integer("unitAmountCents")
    val amountCents = integer("amountCents")
    val currency = pgEnum("currency", "\"Currency\"", Currency.entries, Currency::dbName).nullable()
    val stripeInvoiceId = text("stripeInvoiceId").nullable()
    val state = pgEnum("state", "\"ConnectedSeatChangeState\"", ConnectedSeatChangeState.entries, ConnectedSeatChangeState::dbName)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConnectedSeatChange_pkey\"")

    init {
        index("\"ConnectedSeatChange_accountId_idx\"", false, accountId)
        index("\"ConnectedSeatChange_organizationId_idx\"", false, organizationId)
        index("\"ConnectedSeatChange_state_idx\"", false, state)
    }
}
