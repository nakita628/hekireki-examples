package workoutcool.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PlanProviderMappingTable : IdTable<String>("plan_provider_mappings") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val planId =
        reference(
            "planId",
            SubscriptionPlanTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"plan_provider_mappings_planId_fkey\"",
        )
    val provider = pgEnum("provider", "\"PaymentProcessor\"", PaymentProcessor.entries, PaymentProcessor::dbName)
    val externalId = text("externalId")
    val region = text("region").nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val isActive = bool("isActive").default(true)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "plan_provider_mappings_pkey")

    init {
        index("\"plan_provider_mappings_provider_externalId_idx\"", false, provider, externalId)
        uniqueIndex("\"plan_provider_mappings_planId_provider_region_key\"", planId, provider, region)
    }
}
