package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AuthzOrganizationPolicyTable : IdTable<String>("\"AuthzOrganizationPolicy\"") {
    override val id = text("organizationId").entityId()
    val braidsOrganizationAdminsOnly = bool("braidsOrganizationAdminsOnly").default(false)
    val updatedByUserId = text("updatedByUserId")
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AuthzOrganizationPolicy_pkey\"")
}
