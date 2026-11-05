package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PendingPremiumGrantTable : IdTable<String>("\"PendingPremiumGrant\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email").uniqueIndex("\"PendingPremiumGrant_email_key\"")
    val tier = pgEnum("tier", "\"PremiumTier\"", PremiumTier.entries, PremiumTier::dbName)
    val count = integer("count").default(1)
    val emailAccountsAccess = integer("emailAccountsAccess").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"PendingPremiumGrant_pkey\"")
}
