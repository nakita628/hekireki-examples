package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object BookingReportTable : IdTable<UUID>("\"BookingReport\"") {
    override val id = javaUUID("id").clientDefault { UUID.randomUUID() }.entityId()
    val bookingUid =
        text("bookingUid")
            .references(
                BookingTable.uid,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"BookingReport_bookingUid_fkey\"",
            ).uniqueIndex("\"BookingReport_bookingUid_key\"")
    val bookerEmail = text("bookerEmail")
    val reportedById =
        optReference(
            "reportedById",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingReport_reportedById_fkey\"",
        )
    val organizationId =
        optReference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingReport_organizationId_fkey\"",
        )
    val reason = pgEnum("reason", "\"BookingReportReason\"", BookingReportReason.entries, BookingReportReason::dbName)
    val description = text("description").nullable()
    val cancelled = bool("cancelled").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val status =
        pgEnum("status", "\"BookingReportStatus\"", BookingReportStatus.entries, BookingReportStatus::dbName).default(
            BookingReportStatus.PENDING,
        )
    val systemStatus =
        pgEnum("systemStatus", "\"SystemReportStatus\"", SystemReportStatus.entries, SystemReportStatus::dbName).default(
            SystemReportStatus.PENDING,
        )
    val watchlistId =
        optReference(
            "watchlistId",
            WatchlistTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingReport_watchlistId_fkey\"",
        )
    val globalWatchlistId =
        optReference(
            "globalWatchlistId",
            WatchlistTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingReport_globalWatchlistId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"BookingReport_pkey\"")

    init {
        index("\"BookingReport_bookerEmail_idx\"", false, bookerEmail)
        index("\"BookingReport_reportedById_idx\"", false, reportedById)
        index("\"BookingReport_organizationId_idx\"", false, organizationId)
        index("\"BookingReport_watchlistId_idx\"", false, watchlistId)
        index("\"BookingReport_globalWatchlistId_idx\"", false, globalWatchlistId)
        index("\"BookingReport_systemStatus_idx\"", false, systemStatus)
        index("\"BookingReport_createdAt_idx\"", false, createdAt)
    }
}
