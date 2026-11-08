package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ThrottleClaimTable : IdTable<String>("throttle_claims") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"throttle_claims_projectId_fkey\"",
        )
    val key = text("key")
    val recipientKey = text("recipientKey")
    val contactId =
        optReference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"throttle_claims_contactId_fkey\"",
        )
    val emailId = text("emailId").nullable()
    val workflowId =
        optReference(
            "workflowId",
            WorkflowTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"throttle_claims_workflowId_fkey\"",
        )
    val windowSeconds = integer("windowSeconds").nullable()
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "throttle_claims_pkey")

    init {
        index("\"throttle_claims_emailId_idx\"", false, emailId)
        index("\"throttle_claims_workflowId_idx\"", false, workflowId)
        index("\"throttle_claims_contactId_idx\"", false, contactId)
        index("\"throttle_claims_expiresAt_idx\"", false, expiresAt)
        uniqueIndex("\"throttle_claims_projectId_key_recipientKey_key\"", projectId, key, recipientKey)
    }
}
