package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class UserEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserEntity>(UserTable)

    var uuid by UserTable.uuid
    var username by UserTable.username
    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    val password by UserPasswordEntity optionalBackReferencedOn UserPasswordTable.id
    var bio by UserTable.bio
    var avatarUrl by UserTable.avatarUrl
    var timeZone by UserTable.timeZone
    val travelSchedules by TravelScheduleEntity referrersOn TravelScheduleTable.userId
    var weekStart by UserTable.weekStart
    var bufferTime by UserTable.bufferTime
    var hideBranding by UserTable.hideBranding
    var theme by UserTable.theme
    var appTheme by UserTable.appTheme
    var createdDate by UserTable.createdDate
    var trialEndsAt by UserTable.trialEndsAt
    var lastActiveAt by UserTable.lastActiveAt
    var eventTypes by EventTypeEntity via UserEventtypeTable
    val credentials by CredentialEntity optionalReferrersOn CredentialTable.userId
    val teams by MembershipEntity referrersOn MembershipTable.userId
    val bookings by BookingEntity optionalReferrersOn BookingTable.userId
    val schedules by ScheduleEntity referrersOn ScheduleTable.userId
    var defaultScheduleId by UserTable.defaultScheduleId
    val selectedCalendars by SelectedCalendarEntity referrersOn SelectedCalendarTable.userId
    var completedOnboarding by UserTable.completedOnboarding
    var locale by UserTable.locale
    var timeFormat by UserTable.timeFormat
    var twoFactorSecret by UserTable.twoFactorSecret
    var twoFactorEnabled by UserTable.twoFactorEnabled
    var backupCodes by UserTable.backupCodes
    var identityProvider by UserTable.identityProvider
    var identityProviderId by UserTable.identityProviderId
    val availability by AvailabilityEntity optionalReferrersOn AvailabilityTable.userId
    var invitedTo by UserTable.invitedTo
    val webhooks by WebhookEntity optionalReferrersOn WebhookTable.userId
    var brandColor by UserTable.brandColor
    var darkBrandColor by UserTable.darkBrandColor
    val destinationCalendar by DestinationCalendarEntity optionalBackReferencedOn DestinationCalendarTable.userId
    var allowDynamicBooking by UserTable.allowDynamicBooking
    var allowSEOIndexing by UserTable.allowSEOIndexing
    var receiveMonthlyDigestEmail by UserTable.receiveMonthlyDigestEmail
    var requiresBookerEmailVerification by UserTable.requiresBookerEmailVerification
    var metadata by UserTable.metadata
    var verified by UserTable.verified
    var role by UserTable.role
    val apiKeys by ApiKeyEntity referrersOn ApiKeyTable.userId
    val accounts by AccountEntity referrersOn AccountTable.userId
    val sessions by SessionEntity referrersOn SessionTable.userId
    val feedback by FeedbackEntity referrersOn FeedbackTable.userId
    val ownedEventTypes by EventTypeEntity optionalReferrersOn EventTypeTable.userId
    val verifiedNumbers by VerifiedNumberEntity optionalReferrersOn VerifiedNumberTable.userId
    val verifiedEmails by VerifiedEmailEntity optionalReferrersOn VerifiedEmailTable.userId
    val hosts by HostEntity referrersOn HostTable.userId
    var organizationId by UserTable.organizationId
    var organization by TeamEntity optionalReferencedOn UserTable.organizationId
    val accessCodes by AccessCodeEntity optionalReferrersOn AccessCodeTable.userId
    val bookingRedirects by OutOfOfficeEntryEntity referrersOn OutOfOfficeEntryTable.userId
    val bookingRedirectsTo by OutOfOfficeEntryEntity optionalReferrersOn OutOfOfficeEntryTable.toUserId
    var locked by UserTable.locked
    var platformOAuthClients by PlatformOAuthClientEntity via PlatformOAuthClientToUserTable
    val accessToken by AccessTokenEntity referrersOn AccessTokenTable.userId
    val refreshToken by RefreshTokenEntity referrersOn RefreshTokenTable.userId
    val platformAuthorizationToken by PlatformAuthorizationTokenEntity referrersOn PlatformAuthorizationTokenTable.userId
    val profiles by ProfileEntity referrersOn ProfileTable.userId
    var movedToProfileId by UserTable.movedToProfileId
    var movedToProfile by ProfileEntity optionalReferencedOn UserTable.movedToProfileId
    val secondaryEmails by SecondaryEmailEntity referrersOn SecondaryEmailTable.userId
    var isPlatformManaged by UserTable.isPlatformManaged
    val outOfOfficeReasons by OutOfOfficeReasonEntity optionalReferrersOn OutOfOfficeReasonTable.userId
    var smsLockState by UserTable.smsLockState
    var smsLockReviewedByAdmin by UserTable.smsLockReviewedByAdmin
    val notificationsSubscriptions by NotificationsSubscriptionsEntity referrersOn NotificationsSubscriptionsTable.userId
    var referralLinkId by UserTable.referralLinkId
    val features by UserFeaturesEntity referrersOn UserFeaturesTable.userId
    val reassignedBookings by BookingEntity optionalReferrersOn BookingTable.reassignById
    val createdAttributeToUsers by AttributeToUserEntity optionalReferrersOn AttributeToUserTable.createdById
    val updatedAttributeToUsers by AttributeToUserEntity optionalReferrersOn AttributeToUserTable.updatedById
    val createdTranslations by EventTypeTranslationEntity referrersOn EventTypeTranslationTable.createdBy
    val updatedTranslations by EventTypeTranslationEntity optionalReferrersOn EventTypeTranslationTable.updatedBy
    val reportedBookings by BookingReportEntity optionalReferrersOn BookingReportTable.reportedById
    val wrongAssignmentReports by WrongAssignmentReportEntity optionalReferrersOn WrongAssignmentReportTable.reportedById
    val reviewedWrongAssignmentReports by WrongAssignmentReportEntity optionalReferrersOn WrongAssignmentReportTable.reviewedById
    val bookingInternalNote by BookingInternalNoteEntity referrersOn BookingInternalNoteTable.createdById
    var creationSource by UserTable.creationSource
    val createdOrganizationOnboardings by OrganizationOnboardingEntity referrersOn OrganizationOnboardingTable.createdById
    val filterSegments by FilterSegmentEntity referrersOn FilterSegmentTable.userId
    val filterSegmentPreferences by UserFilterSegmentPreferenceEntity referrersOn UserFilterSegmentPreferenceTable.userId
    val creditBalance by CreditBalanceEntity optionalBackReferencedOn CreditBalanceTable.userId
    val calAiPhoneNumbers by CalAiPhoneNumberEntity optionalReferrersOn CalAiPhoneNumberTable.userId
    val agents by AgentEntity optionalReferrersOn AgentTable.userId
    val oAuthClients by OAuthClientEntity optionalReferrersOn OAuthClientTable.userId
    val holidaySettings by UserHolidaySettingsEntity optionalBackReferencedOn UserHolidaySettingsTable.userId
    var autoOptInFeatures by UserTable.autoOptInFeatures
}
