package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object BookingReferenceTable : IdTable<Int>("\"BookingReference\"") {
    override val id = integer("id").autoIncrement().entityId()
    val type = text("type")
    val uid = text("uid")
    val meetingId = text("meetingId").nullable()
    val thirdPartyRecurringEventId = text("thirdPartyRecurringEventId").nullable()
    val meetingPassword = text("meetingPassword").nullable()
    val meetingUrl = text("meetingUrl").nullable()
    val bookingId =
        optReference(
            "bookingId",
            BookingTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingReference_bookingId_fkey\"",
        )
    val externalCalendarId = text("externalCalendarId").nullable()
    val deleted = bool("deleted").nullable()
    val credentialId =
        optReference(
            "credentialId",
            CredentialTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingReference_credentialId_fkey\"",
        )
    val delegationCredentialId =
        optReference(
            "delegationCredentialId",
            DelegationCredentialTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"BookingReference_delegationCredentialId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"BookingReference_pkey\"")

    init {
        index("\"BookingReference_bookingId_idx\"", false, bookingId)
        index("\"BookingReference_type_idx\"", false, type)
        index("\"BookingReference_uid_idx\"", false, uid)
    }
}
