package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SubscriptionTable : IdTable<String>("subscriptions") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"subscriptions_userId_fkey\"",
        )
    val planId =
        reference(
            "planId",
            SubscriptionPlanTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"subscriptions_planId_fkey\"",
        )
    val revenueCatUserId = text("revenueCatUserId").nullable()
    val status = pgEnum("status", "\"SubscriptionStatus\"", SubscriptionStatus.entries, SubscriptionStatus::dbName)
    val startedAt = pgTimestamp("startedAt", 3)
    val currentPeriodEnd = pgTimestamp("currentPeriodEnd", 3).nullable()
    val cancelledAt = pgTimestamp("cancelledAt", 3).nullable()
    val platform = pgEnum("platform", "\"Platform\"", Platform.entries, Platform::dbName).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "subscriptions_pkey")

    init {
        uniqueIndex("\"subscriptions_userId_platform_key\"", userId, platform)
    }
}
