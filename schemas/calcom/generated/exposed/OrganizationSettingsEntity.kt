package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OrganizationSettingsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, OrganizationSettingsEntity>(OrganizationSettingsTable)

    var organization by TeamEntity referencedOn OrganizationSettingsTable.organizationId
    var organizationId by OrganizationSettingsTable.organizationId
    var isOrganizationConfigured by OrganizationSettingsTable.isOrganizationConfigured
    var isOrganizationVerified by OrganizationSettingsTable.isOrganizationVerified
    var orgAutoAcceptEmail by OrganizationSettingsTable.orgAutoAcceptEmail
    var lockEventTypeCreationForUsers by OrganizationSettingsTable.lockEventTypeCreationForUsers
    var adminGetsNoSlotsNotification by OrganizationSettingsTable.adminGetsNoSlotsNotification
    var isAdminReviewed by OrganizationSettingsTable.isAdminReviewed
    var isAdminAPIEnabled by OrganizationSettingsTable.isAdminAPIEnabled
    var allowSEOIndexing by OrganizationSettingsTable.allowSEOIndexing
    var orgProfileRedirectsToVerifiedDomain by OrganizationSettingsTable.orgProfileRedirectsToVerifiedDomain
    var disablePhoneOnlySMSNotifications by OrganizationSettingsTable.disablePhoneOnlySMSNotifications
    var disableAutofillOnBookingPage by OrganizationSettingsTable.disableAutofillOnBookingPage
    var orgAutoJoinOnSignup by OrganizationSettingsTable.orgAutoJoinOnSignup
    var disableAttendeeConfirmationEmail by OrganizationSettingsTable.disableAttendeeConfirmationEmail
    var disableAttendeeCancellationEmail by OrganizationSettingsTable.disableAttendeeCancellationEmail
    var disableAttendeeRescheduledEmail by OrganizationSettingsTable.disableAttendeeRescheduledEmail
    var disableAttendeeRequestEmail by OrganizationSettingsTable.disableAttendeeRequestEmail
    var disableAttendeeReassignedEmail by OrganizationSettingsTable.disableAttendeeReassignedEmail
    var disableAttendeeAwaitingPaymentEmail by OrganizationSettingsTable.disableAttendeeAwaitingPaymentEmail
    var disableAttendeeRescheduleRequestEmail by OrganizationSettingsTable.disableAttendeeRescheduleRequestEmail
    var disableAttendeeLocationChangeEmail by OrganizationSettingsTable.disableAttendeeLocationChangeEmail
    var disableAttendeeNewEventEmail by OrganizationSettingsTable.disableAttendeeNewEventEmail
}
