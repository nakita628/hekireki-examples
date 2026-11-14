package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object DepartmentMembershipHistoryTable : IdTable<String>("\"DepartmentMembershipHistory\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val userId = text("userId")
    val departmentId = text("departmentId")
    val validFrom = pgTimestamp("validFrom", 3)
    val validTo = pgTimestamp("validTo", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"DepartmentMembershipHistory_pkey\"")

    init {
        index("DepartmentMembershipHistory_organizationId_userId_validFrom_idx", false, organizationId, userId, validFrom)
    }
}
