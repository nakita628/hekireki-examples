package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object OrganizationInviteTable : IdTable<String>("\"OrganizationInvite\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val email = text("email")
    val inviteCode = text("inviteCode").uniqueIndex("\"OrganizationInvite_inviteCode_key\"")
    val expiration = pgTimestamp("expiration", 3).nullable()
    val status = pgEnum("status", "\"INVITE_STATUS\"", InviteStatus.entries, InviteStatus::dbName).default(InviteStatus.PENDING)
    val organizationId = text("organizationId")
    val teamIds = text("teamIds")
    val teamAssignments = jsonb<String>("teamAssignments", { it }, { it }).nullable()
    val role = pgEnum("role", "\"OrganizationUserRole\"", OrganizationUserRole.entries, OrganizationUserRole::dbName)
    val requestedBy = text("requestedBy").nullable()
    val subscriptionId = text("subscriptionId").nullable()
    val acceptedByUserId = text("acceptedByUserId").nullable()
    val acceptedViaIdentifierId = text("acceptedViaIdentifierId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationInvite_pkey\"")

    init {
        index("\"OrganizationInvite_organizationId_idx\"", false, organizationId)
        index("\"OrganizationInvite_requestedBy_idx\"", false, requestedBy)
    }
}
