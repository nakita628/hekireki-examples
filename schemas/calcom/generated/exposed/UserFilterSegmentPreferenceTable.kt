package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserFilterSegmentPreferenceTable : IdTable<Int>("\"UserFilterSegmentPreference\"") {
    override val id = integer("id").autoIncrement().entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserFilterSegmentPreference_userId_fkey\"",
        )
    val tableIdentifier = text("tableIdentifier")
    val segmentId =
        optReference(
            "segmentId",
            FilterSegmentTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"UserFilterSegmentPreference_segmentId_fkey\"",
        )
    val systemSegmentId = text("systemSegmentId").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"UserFilterSegmentPreference_pkey\"")

    init {
        index("\"UserFilterSegmentPreference_userId_idx\"", false, userId)
        index("\"UserFilterSegmentPreference_segmentId_idx\"", false, segmentId)
        uniqueIndex("\"UserFilterSegmentPreference_userId_tableIdentifier_key\"", userId, tableIdentifier)
    }
}
