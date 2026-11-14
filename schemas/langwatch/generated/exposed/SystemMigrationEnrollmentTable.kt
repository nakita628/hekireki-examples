package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SystemMigrationEnrollmentTable : CompositeIdTable("\"SystemMigrationEnrollment\"") {
    val organizationId = text("organizationId").entityId()
    val migrationName = text("migrationName").entityId()
    val enrolledByUserId = text("enrolledByUserId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(organizationId, migrationName, name = "\"SystemMigrationEnrollment_pkey\"")

    init {
        index("\"SystemMigrationEnrollment_migrationName_idx\"", false, migrationName)
    }
}
