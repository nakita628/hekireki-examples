package linkwarden.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SubscriptionTable : IdTable<Int>("\"Subscription\"") {
    override val id = integer("id").autoIncrement().entityId()
    val active = bool("active")
    val provider = pgEnum("provider", "\"SubscriptionProvider\"", SubscriptionProvider.entries, SubscriptionProvider::dbName)
    val currentPeriodStart = pgTimestamp("currentPeriodStart", 3)
    val currentPeriodEnd = pgTimestamp("currentPeriodEnd", 3)
    val quantity = integer("quantity").default(1)
    val storeOriginalTransactionId =
        text("storeOriginalTransactionId").nullable().uniqueIndex("\"Subscription_storeOriginalTransactionId_key\"")
    val storeProductId = text("storeProductId").nullable()
    val googlePurchaseToken = text("googlePurchaseToken").nullable().uniqueIndex("\"Subscription_googlePurchaseToken_key\"")
    val storeMetadata = jsonb<String>("storeMetadata", { it }, { it }).nullable()
    val stripeSubscriptionId = text("stripeSubscriptionId").nullable().uniqueIndex("\"Subscription_stripeSubscriptionId_key\"")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Subscription_userId_fkey\"",
        ).uniqueIndex("\"Subscription_userId_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Subscription_pkey\"")
}
