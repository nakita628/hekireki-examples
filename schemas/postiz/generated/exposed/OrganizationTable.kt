package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object OrganizationTable : IdTable<String>("\"Organization\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val name = text("name")
    val description = text("description").nullable()
    val apiKey = text("apiKey").nullable()
    val paymentId = text("paymentId").nullable()
    val streakSince = pgTimestamp("streakSince", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val allowTrial = bool("allowTrial").default(false)
    val isTrailing = bool("isTrailing").default(false)
    val shortlink =
        pgEnum("shortlink", "\"ShortLinkPreference\"", ShortLinkPreference.entries, ShortLinkPreference::dbName).default(
            ShortLinkPreference.ASK,
        )
    override val primaryKey = PrimaryKey(id, name = "\"Organization_pkey\"")

    init {
        index("\"Organization_apiKey_idx\"", false, apiKey)
        index("\"Organization_streakSince_idx\"", false, streakSince)
        index("\"Organization_paymentId_idx\"", false, paymentId)
        index("\"Organization_deletedAt_idx\"", false, deletedAt)
    }
}
