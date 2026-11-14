package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SystemMigrationTenantStateTable : CompositeIdTable("\"SystemMigrationTenantState\"") {
    val migrationName = text("migrationName").entityId()
    val tenantId = text("tenantId").entityId()
    val status = text("status")
    val report = jsonb<String>("report", { it }, { it }).nullable()
    val occurredAt = pgTimestamp("occurredAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(migrationName, tenantId, name = "\"SystemMigrationTenantState_pkey\"")

    init {
        index("\"SystemMigrationTenantState_migrationName_status_idx\"", false, migrationName, status)
    }
}
