package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SubscriptionPlanTable : IdTable<String>("subscription_plans") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val priceMonthly = decimal("priceMonthly", 10, 2).nullable()
    val priceYearly = decimal("priceYearly", 10, 2).nullable()
    val currency = text("currency").default("EUR")
    val interval = text("interval").default("month")
    val isActive = bool("isActive").default(true)
    val availableRegions = array("availableRegions", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "subscription_plans_pkey")
}
