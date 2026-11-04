package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationTable : IdTable<String>("organizations") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val cloudConfig = jsonb<String>("cloud_config", { it }, { it }).nullable()
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val cloudBillingCycleAnchor =
        pgTimestamp("cloud_billing_cycle_anchor", 3)
            .nullable()
            .databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val cloudBillingCycleUpdatedAt = pgTimestamp("cloud_billing_cycle_updated_at", 3).nullable()
    val cloudCurrentCycleUsage = integer("cloud_current_cycle_usage").nullable()
    val cloudFreeTierUsageThresholdState = text("cloud_free_tier_usage_threshold_state").nullable()
    val aiFeaturesEnabled = bool("ai_features_enabled").default(false)
    val aiTelemetryEnabled = bool("ai_telemetry_enabled").default(true)
    val accessAuditLogEnabled = bool("access_audit_log_enabled").default(false)
    val featureFlagOrgDefaults = array("feature_flag_org_defaults", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val sfdcOrgId = text("sfdc_org_id").nullable()
    override val primaryKey = PrimaryKey(id, name = "organizations_pkey")
}
