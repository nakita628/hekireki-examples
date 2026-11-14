package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object IdempotencyReceiptTable : IdTable<String>("\"IdempotencyReceipt\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val scopeId = text("scopeId")
    val key = text("key")
    val claimId = text("claimId")
    val requestFingerprint = text("requestFingerprint")
    val responseStatus = integer("responseStatus").nullable()
    val responseBody = text("responseBody").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val heartbeatAt = pgTimestamp("heartbeatAt", 3)
    val expiresAt = pgTimestamp("expiresAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"IdempotencyReceipt_pkey\"")

    init {
        index("\"IdempotencyReceipt_expiresAt_idx\"", false, expiresAt)
        uniqueIndex("\"IdempotencyReceipt_scopeId_key_key\"", scopeId, key)
    }
}
