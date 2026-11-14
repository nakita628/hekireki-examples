package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationUserTable : CompositeIdTable("\"OrganizationUser\"") {
    val userId = text("userId").entityId()
    val organizationId = text("organizationId").entityId()
    val role = pgEnum("role", "\"OrganizationUserRole\"", OrganizationUserRole.entries, OrganizationUserRole::dbName)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val departmentId = text("departmentId").nullable()
    val disabledAt = pgTimestamp("disabledAt", 3).nullable()
    val pendingSsoGrantId = text("pendingSsoGrantId").nullable()
    val membershipStamp = text("membershipStamp").databaseDefault("gen_random_uuid()::text")
    val fullSeatSince = pgTimestamp("fullSeatSince", 3).nullable()
    val seatPendingSince = pgTimestamp("seatPendingSince", 3).nullable()
    override val primaryKey = PrimaryKey(userId, organizationId, name = "\"OrganizationUser_pkey\"")

    init {
        index("\"OrganizationUser_organizationId_idx\"", false, organizationId)
        index("\"OrganizationUser_userId_idx\"", false, userId)
        index("\"OrganizationUser_departmentId_idx\"", false, departmentId)
    }
}
