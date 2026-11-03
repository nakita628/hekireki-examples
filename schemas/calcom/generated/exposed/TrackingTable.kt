package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TrackingTable : IdTable<Int>("\"Tracking\"") {
    override val id = integer("id").autoIncrement().entityId()
    val bookingId =
        reference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Tracking_bookingId_fkey\"",
        ).uniqueIndex("\"Tracking_bookingId_key\"")
    val utmSource = text("utm_source").nullable()
    val utmMedium = text("utm_medium").nullable()
    val utmCampaign = text("utm_campaign").nullable()
    val utmTerm = text("utm_term").nullable()
    val utmContent = text("utm_content").nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Tracking_pkey\"")
}
