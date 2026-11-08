package postiz.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PostTable : IdTable<String>("\"Post\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val state = pgEnum("state", "\"State\"", State.entries, State::dbName).default(State.QUEUE)
    val publishDate = pgTimestamp("publishDate", 3)
    val organizationId =
        reference(
            "organizationId",
            OrganizationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Post_organizationId_fkey\"",
        )
    val integrationId =
        reference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Post_integrationId_fkey\"",
        )
    val content = text("content")
    val delay = integer("delay").default(0)
    val group = text("group")
    val batchId = text("batchId").nullable()
    val title = text("title").nullable()
    val description = text("description").nullable()
    val parentPostId =
        optReference(
            "parentPostId",
            PostTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Post_parentPostId_fkey\"",
        )
    val releaseId = text("releaseId").nullable()
    val releaseURL = text("releaseURL").nullable()
    val settings = text("settings").nullable()
    val image = text("image").nullable()
    val submittedForOrderId =
        optReference(
            "submittedForOrderId",
            OrdersTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Post_submittedForOrderId_fkey\"",
        )
    val submittedForOrganizationId =
        optReference(
            "submittedForOrganizationId",
            OrganizationTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Post_submittedForOrganizationId_fkey\"",
        )
    val approvedSubmitForOrder =
        pgEnum("approvedSubmitForOrder", "\"APPROVED_SUBMIT_FOR_ORDER\"", ApprovedSubmitForOrder.entries, ApprovedSubmitForOrder::dbName)
            .default(ApprovedSubmitForOrder.NO)
    val creationMethod =
        pgEnum("creationMethod", "\"CreationMethod\"", CreationMethod.entries, CreationMethod::dbName).default(CreationMethod.UNKNOWN)
    val lastMessageId =
        optReference(
            "lastMessageId",
            MessagesTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Post_lastMessageId_fkey\"",
        )
    val intervalInDays = integer("intervalInDays").nullable()
    val error = text("error").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Post_pkey\"")

    init {
        index("\"Post_group_idx\"", false, group)
        index("\"Post_batchId_idx\"", false, batchId)
        index("\"Post_deletedAt_idx\"", false, deletedAt)
        index("\"Post_publishDate_idx\"", false, publishDate)
        index(
            "Post_publishDate_state_deletedAt_parentPostId_integrationId_idx",
            false,
            publishDate,
            state,
            deletedAt,
            parentPostId,
            integrationId,
            organizationId,
            creationMethod,
        )
        index("\"Post_state_idx\"", false, state)
        index("\"Post_organizationId_idx\"", false, organizationId)
        index("\"Post_organizationId_publishDate_idx\"", false, organizationId, publishDate)
        index("\"Post_parentPostId_idx\"", false, parentPostId)
        index("\"Post_submittedForOrderId_idx\"", false, submittedForOrderId)
        index("\"Post_intervalInDays_idx\"", false, intervalInDays)
        index("\"Post_approvedSubmitForOrder_idx\"", false, approvedSubmitForOrder)
        index("\"Post_creationMethod_idx\"", false, creationMethod)
        index("\"Post_lastMessageId_idx\"", false, lastMessageId)
        index("\"Post_createdAt_idx\"", false, createdAt)
        index("\"Post_updatedAt_idx\"", false, updatedAt)
        index("\"Post_releaseURL_idx\"", false, releaseURL)
        index("\"Post_integrationId_idx\"", false, integrationId)
    }
}
