package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object WrongAssignmentReportTable : IdTable<UUID>("\"WrongAssignmentReport\"") {
    override val id = javaUUID("id").clientDefault { UUID.randomUUID() }.entityId()
    val bookingUid =
        text("bookingUid")
            .references(
                BookingTable.uid,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"WrongAssignmentReport_bookingUid_fkey\"",
            ).uniqueIndex("\"WrongAssignmentReport_bookingUid_key\"")
    val reportedById =
        optReference(
            "reportedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WrongAssignmentReport_reportedById_fkey\"",
        )
    val correctAssignee = text("correctAssignee").nullable()
    val additionalNotes = text("additionalNotes")
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WrongAssignmentReport_teamId_fkey\"",
        )
    val status =
        pgEnum("status", "\"WrongAssignmentReportStatus\"", WrongAssignmentReportStatus.entries, WrongAssignmentReportStatus::dbName)
            .default(WrongAssignmentReportStatus.PENDING)
    val reviewedById =
        optReference(
            "reviewedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"WrongAssignmentReport_reviewedById_fkey\"",
        )
    val reviewedAt = pgTimestamp("reviewedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"WrongAssignmentReport_pkey\"")

    init {
        index("\"WrongAssignmentReport_reportedById_idx\"", false, reportedById)
        index("\"WrongAssignmentReport_teamId_idx\"", false, teamId)
        index("\"WrongAssignmentReport_status_idx\"", false, status)
        index("\"WrongAssignmentReport_reviewedById_idx\"", false, reviewedById)
        index("\"WrongAssignmentReport_createdAt_idx\"", false, createdAt)
        index("\"WrongAssignmentReport_teamId_status_createdAt_idx\"", false, teamId, status, createdAt)
    }
}
