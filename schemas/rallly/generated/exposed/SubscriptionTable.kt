package rallly.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SubscriptionTable : IdTable<String>("subscriptions") {
    override val id = text("id").entityId()
    val priceId = text("price_id")
    val quantity = integer("quantity").default(1)
    val subscriptionItemId = text("subscription_item_id")
    val amount = integer("amount")
    val discountPercentOff = double("discount_percent_off").nullable()
    val discountAmountOff = integer("discount_amount_off").nullable()
    val status = pgEnum("status", "\"subscription_status\"", SubscriptionStatus.entries, SubscriptionStatus::dbName)
    val active = bool("active")
    val currency = text("currency")
    val interval = pgEnum("interval", "\"subscription_interval\"", SubscriptionInterval.entries, SubscriptionInterval::dbName)
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val periodStart = pgTimestamp("period_start", 3)
    val periodEnd = pgTimestamp("period_end", 3)
    val cancelAtPeriodEnd = bool("cancel_at_period_end").default(false)
    val userId =
        reference(
            "user_id",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "subscriptions_user_id_fkey",
        )
    val spaceId =
        reference(
            "space_id",
            SpaceTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "subscriptions_space_id_fkey",
        )
    val apiAddonItemId = text("api_addon_item_id").nullable()
    val apiAddonPriceId = text("api_addon_price_id").nullable()
    val apiAddonAmount = integer("api_addon_amount").nullable()
    val apiPollLimit = integer("api_poll_limit").nullable()
    val apiPollRetainedLimit = integer("api_poll_retained_limit").nullable()
    val apiPollRetainedUntil = pgTimestamp("api_poll_retained_until", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "subscriptions_pkey")

    init {
        index("subscriptions_user_id_idx", false, userId)
        index("subscriptions_space_id_idx", false, spaceId)
    }
}
