package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SupportAccessRequestTable : IdTable<String>("\"SupportAccessRequest\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SupportAccessRequest_organizationId_fkey\"",
        )
    val requestedById =
        optReference(
            "requestedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SupportAccessRequest_requestedById_fkey\"",
        )
    val reason = text("reason")
    val status =
        pgEnum("status", "\"SupportAccessRequestStatus\"", SupportAccessRequestStatus.entries, SupportAccessRequestStatus::dbName).default(
            SupportAccessRequestStatus.PENDING,
        )
    val approvedById =
        optReference(
            "approvedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SupportAccessRequest_approvedById_fkey\"",
        )
    val approvedAt = pgTimestamp("approvedAt", 3).nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val cancelledById = text("cancelledById").nullable()
    val cancelledAt = pgTimestamp("cancelledAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SupportAccessRequest_pkey\"")

    init {
        index("\"SupportAccessRequest_organizationId_status_expiresAt_idx\"", false, organizationId, status, expiresAt)
        index("\"SupportAccessRequest_requestedById_idx\"", false, requestedById)
        index("\"SupportAccessRequest_approvedById_idx\"", false, approvedById)
    }
}
