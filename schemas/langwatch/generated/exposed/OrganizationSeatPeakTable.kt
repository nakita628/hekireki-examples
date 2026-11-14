package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OrganizationSeatPeakTable : IdTable<String>("\"OrganizationSeatPeak\"") {
    override val id = text("organizationId").entityId()
    val openedAt = double("openedAt")
    val fullSeats = integer("fullSeats")
    val liteSeats = integer("liteSeats")
    val developerSeats = integer("developerSeats")
    val latestChangeAt = double("latestChangeAt")
    val mismatches = integer("mismatches")
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationSeatPeak_pkey\"")
}
