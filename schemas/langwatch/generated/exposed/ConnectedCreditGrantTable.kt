package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ConnectedCreditGrantTable : IdTable<String>("\"ConnectedCreditGrant\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val accountId = text("accountId")
    val stripeCreditGrantId = text("stripeCreditGrantId").uniqueIndex("\"ConnectedCreditGrant_stripeCreditGrantId_key\"")
    val amountUsdCents = integer("amountUsdCents")
    val kind = pgEnum("kind", "\"ConnectedCreditGrantKind\"", ConnectedCreditGrantKind.entries, ConnectedCreditGrantKind::dbName)
    val termEndsAt = pgTimestamp("termEndsAt", 3)
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"ConnectedCreditGrant_pkey\"")

    init {
        index("\"ConnectedCreditGrant_accountId_idx\"", false, accountId)
    }
}
