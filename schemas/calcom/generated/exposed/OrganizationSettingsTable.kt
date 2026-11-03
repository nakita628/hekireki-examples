package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object OrganizationSettingsTable : IdTable<Int>("\"OrganizationSettings\"") {
    override val id = integer("id").autoIncrement().entityId()
    val organizationId =
        reference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"OrganizationSettings_organizationId_fkey\"",
        ).uniqueIndex("\"OrganizationSettings_organizationId_key\"")
    val isOrganizationConfigured = bool("isOrganizationConfigured").default(false)
    val isOrganizationVerified = bool("isOrganizationVerified").default(false)
    val orgAutoAcceptEmail = text("orgAutoAcceptEmail")
    val lockEventTypeCreationForUsers = bool("lockEventTypeCreationForUsers").default(false)
    val adminGetsNoSlotsNotification = bool("adminGetsNoSlotsNotification").default(false)
    val isAdminReviewed = bool("isAdminReviewed").default(false)
    val isAdminAPIEnabled = bool("isAdminAPIEnabled").default(false)
    val allowSEOIndexing = bool("allowSEOIndexing").default(false)
    val orgProfileRedirectsToVerifiedDomain = bool("orgProfileRedirectsToVerifiedDomain").default(false)
    val disablePhoneOnlySMSNotifications = bool("disablePhoneOnlySMSNotifications").default(false)
    val disableAutofillOnBookingPage = bool("disableAutofillOnBookingPage").default(false)
    val orgAutoJoinOnSignup = bool("orgAutoJoinOnSignup").default(true)
    val disableAttendeeConfirmationEmail = bool("disableAttendeeConfirmationEmail").default(false)
    val disableAttendeeCancellationEmail = bool("disableAttendeeCancellationEmail").default(false)
    val disableAttendeeRescheduledEmail = bool("disableAttendeeRescheduledEmail").default(false)
    val disableAttendeeRequestEmail = bool("disableAttendeeRequestEmail").default(false)
    val disableAttendeeReassignedEmail = bool("disableAttendeeReassignedEmail").default(false)
    val disableAttendeeAwaitingPaymentEmail = bool("disableAttendeeAwaitingPaymentEmail").default(false)
    val disableAttendeeRescheduleRequestEmail = bool("disableAttendeeRescheduleRequestEmail").default(false)
    val disableAttendeeLocationChangeEmail = bool("disableAttendeeLocationChangeEmail").default(false)
    val disableAttendeeNewEventEmail = bool("disableAttendeeNewEventEmail").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"OrganizationSettings_pkey\"")
}
