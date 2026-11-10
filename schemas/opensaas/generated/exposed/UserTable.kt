package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val email = text("email").nullable().uniqueIndex("\"User_email_key\"")
    val username = text("username").nullable().uniqueIndex("\"User_username_key\"")
    val isAdmin = bool("isAdmin").default(false)
    val paymentProcessorUserId = text("paymentProcessorUserId").nullable().uniqueIndex("\"User_paymentProcessorUserId_key\"")
    val subscriptionStatus = text("subscriptionStatus").nullable()
    val subscriptionPlan = text("subscriptionPlan").nullable()
    val datePaid = pgTimestamp("datePaid", 3).nullable()
    val credits = integer("credits").default(3)
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")
}
