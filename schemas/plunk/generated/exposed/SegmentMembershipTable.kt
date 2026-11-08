package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object SegmentMembershipTable : CompositeIdTable("segment_memberships") {
    val contactId =
        reference(
            "contactId",
            ContactTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"segment_memberships_contactId_fkey\"",
        )
    val segmentId =
        reference(
            "segmentId",
            SegmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"segment_memberships_segmentId_fkey\"",
        )
    val enteredAt = pgTimestamp("enteredAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val exitedAt = pgTimestamp("exitedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(contactId, segmentId, name = "segment_memberships_pkey")

    init {
        addIdColumn(contactId)
        addIdColumn(segmentId)
        index("\"segment_memberships_segmentId_exitedAt_idx\"", false, segmentId, exitedAt)
        index("\"segment_memberships_contactId_exitedAt_idx\"", false, contactId, exitedAt)
        index("\"segment_memberships_enteredAt_idx\"", false, enteredAt)
    }
}
