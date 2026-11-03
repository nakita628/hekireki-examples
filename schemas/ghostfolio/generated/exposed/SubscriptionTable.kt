package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object SubscriptionTable : IdTable<String>("\"Subscription\"") {
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val expiresAt = pgTimestamp("expiresAt", 3)
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val price = double("price").nullable()
    val stripeCheckoutSessionId = text("stripeCheckoutSessionId").nullable().uniqueIndex("\"Subscription_stripeCheckoutSessionId_key\"")
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Subscription_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"Subscription_pkey\"")

    init {
        index("\"Subscription_userId_idx\"", false, userId)
    }
}
