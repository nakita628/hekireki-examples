package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TeamEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, TeamEntity>(TeamTable)

    var name by TeamTable.name
    var slug by TeamTable.slug
    var logoUrl by TeamTable.logoUrl
    var calVideoLogo by TeamTable.calVideoLogo
    var appLogo by TeamTable.appLogo
    var appIconLogo by TeamTable.appIconLogo
    var bio by TeamTable.bio
    var hideBranding by TeamTable.hideBranding
    var hideTeamProfileLink by TeamTable.hideTeamProfileLink
    var isPrivate by TeamTable.isPrivate
    var hideBookATeamMember by TeamTable.hideBookATeamMember
    val members by MembershipEntity referrersOn MembershipTable.teamId
    val eventTypes by EventTypeEntity optionalReferrersOn EventTypeTable.teamId
    var createdAt by TeamTable.createdAt
    var metadata by TeamTable.metadata
    var theme by TeamTable.theme
    var rrResetInterval by TeamTable.rrResetInterval
    var rrTimestampBasis by TeamTable.rrTimestampBasis
    var brandColor by TeamTable.brandColor
    var darkBrandColor by TeamTable.darkBrandColor
    val verifiedNumbers by VerifiedNumberEntity optionalReferrersOn VerifiedNumberTable.teamId
    val verifiedEmails by VerifiedEmailEntity optionalReferrersOn VerifiedEmailTable.teamId
    var bannerUrl by TeamTable.bannerUrl
    var parentId by TeamTable.parentId
    var parent by TeamEntity optionalReferencedOn TeamTable.parentId
    val children by TeamEntity optionalReferrersOn TeamTable.parentId
    val orgUsers by UserEntity optionalReferrersOn UserTable.organizationId
    val inviteTokens by VerificationTokenEntity optionalReferrersOn VerificationTokenTable.teamId
    val webhooks by WebhookEntity optionalReferrersOn WebhookTable.teamId
    var timeFormat by TeamTable.timeFormat
    var timeZone by TeamTable.timeZone
    var weekStart by TeamTable.weekStart
    val apiKeys by ApiKeyEntity optionalReferrersOn ApiKeyTable.teamId
    val credentials by CredentialEntity optionalReferrersOn CredentialTable.teamId
    val accessCodes by AccessCodeEntity optionalReferrersOn AccessCodeTable.teamId
    var isOrganization by TeamTable.isOrganization
    val organizationSettings by OrganizationSettingsEntity optionalBackReferencedOn OrganizationSettingsTable.organizationId
    val instantMeetingTokens by InstantMeetingTokenEntity referrersOn InstantMeetingTokenTable.teamId
    val orgProfiles by ProfileEntity referrersOn ProfileTable.organizationId
    var pendingPayment by TeamTable.pendingPayment
    val dsyncTeamGroupMapping by DSyncTeamGroupMappingEntity referrersOn DSyncTeamGroupMappingTable.teamId
    var isPlatform by TeamTable.isPlatform
    val platformOAuthClient by PlatformOAuthClientEntity referrersOn PlatformOAuthClientTable.organizationId
    var createdByOAuthClient by PlatformOAuthClientEntity optionalReferencedOn TeamTable.createdByOAuthClientId
    var createdByOAuthClientId by TeamTable.createdByOAuthClientId
    var smsLockState by TeamTable.smsLockState
    val platformBilling by PlatformBillingEntity optionalBackReferencedOn PlatformBillingTable.id
    val attributes by AttributeEntity referrersOn AttributeTable.teamId
    var smsLockReviewedByAdmin by TeamTable.smsLockReviewedByAdmin
    val delegationCredentials by DelegationCredentialEntity referrersOn DelegationCredentialTable.organizationId
    val roles by RoleEntity optionalReferrersOn RoleTable.teamId
    val calAiPhoneNumbers by CalAiPhoneNumberEntity optionalReferrersOn CalAiPhoneNumberTable.teamId
    val agents by AgentEntity optionalReferrersOn AgentTable.teamId
    val bookingReports by BookingReportEntity optionalReferrersOn BookingReportTable.organizationId
    val wrongAssignmentReports by WrongAssignmentReportEntity optionalReferrersOn WrongAssignmentReportTable.teamId
    val features by TeamFeaturesEntity referrersOn TeamFeaturesTable.teamId
    var bookingLimits by TeamTable.bookingLimits
    var includeManagedEventsInLimits by TeamTable.includeManagedEventsInLimits
    val internalNotePresets by InternalNotePresetEntity referrersOn InternalNotePresetTable.teamId
    val creditBalance by CreditBalanceEntity optionalBackReferencedOn CreditBalanceTable.teamId
    val organizationOnboarding by OrganizationOnboardingEntity optionalBackReferencedOn OrganizationOnboardingTable.organizationId
    val managedOrganization by ManagedOrganizationEntity optionalBackReferencedOn ManagedOrganizationTable.id
    val managedOrganizations by ManagedOrganizationEntity referrersOn ManagedOrganizationTable.managerOrganizationId
    val filterSegments by FilterSegmentEntity optionalReferrersOn FilterSegmentTable.teamId
    val teamBilling by TeamBillingEntity optionalBackReferencedOn TeamBillingTable.teamId
    val organizationBilling by OrganizationBillingEntity optionalBackReferencedOn OrganizationBillingTable.teamId
    val integrationAttributeSyncs by IntegrationAttributeSyncEntity referrersOn IntegrationAttributeSyncTable.organizationId
    var autoOptInFeatures by TeamTable.autoOptInFeatures
    val seatChangeLogs by SeatChangeLogEntity referrersOn SeatChangeLogTable.teamId
    val monthlyProrations by MonthlyProrationEntity referrersOn MonthlyProrationTable.teamId
}
