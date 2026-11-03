package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var image by UserTable.image
    var timeZone by UserTable.timeZone
    var weekStart by UserTable.weekStart
    var timeFormat by UserTable.timeFormat
    var locale by UserTable.locale
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var customerId by UserTable.customerId
    var banned by UserTable.banned
    var bannedAt by UserTable.bannedAt
    var banReason by UserTable.banReason
    var banExpires by UserTable.banExpires
    var role by UserTable.role
    var lastLoginMethod by UserTable.lastLoginMethod
    var isAnonymous by UserTable.isAnonymous
    var lastSeenAt by UserTable.lastSeenAt
    var deletedAt by UserTable.deletedAt
    var jobTitle by UserTable.jobTitle
    var reviewRequestedAt by UserTable.reviewRequestedAt
    val comments by CommentEntity optionalReferrersOn CommentTable.userId
    val polls by PollEntity optionalReferrersOn PollTable.userId
    val notificationPreferences by UserNotificationPreferencesEntity optionalBackReferencedOn UserNotificationPreferencesTable.userId
    val queuedEmails by QueuedEmailEntity referrersOn QueuedEmailTable.userId
    val accounts by AccountEntity referrersOn AccountTable.userId
    val participants by ParticipantEntity optionalReferrersOn ParticipantTable.userId
    val paymentMethods by PaymentMethodEntity referrersOn PaymentMethodTable.userId
    val spaceMemberInvites by SpaceMemberInviteEntity referrersOn SpaceMemberInviteTable.inviterId
    val nonprofitApplications by NonprofitApplicationEntity referrersOn NonprofitApplicationTable.userId
    val subscriptions by SubscriptionEntity referrersOn SubscriptionTable.userId
    val spaces by SpaceEntity referrersOn SpaceTable.ownerId
    val memberOf by SpaceMemberEntity referrersOn SpaceMemberTable.userId
    val scheduledEvents by ScheduledEventEntity referrersOn ScheduledEventTable.userId
    val scheduledEventInvites by ScheduledEventInviteEntity optionalReferrersOn ScheduledEventInviteTable.inviteeId
    val hostedEventTypes by EventTypeEntity referrersOn EventTypeTable.hostId
    val hostedSheets by SheetEntity referrersOn SheetTable.hostId
    val calendarConnections by CalendarConnectionEntity referrersOn CalendarConnectionTable.userId
    val conferencingConnections by ConferencingConnectionEntity referrersOn ConferencingConnectionTable.userId
    val credentials by CredentialEntity referrersOn CredentialTable.userId
    var defaultDestinationCalendarId by UserTable.defaultDestinationCalendarId
    var defaultDestinationCalendar by ProviderCalendarEntity optionalReferencedOn UserTable.defaultDestinationCalendarId
    val sessions by SessionEntity referrersOn SessionTable.userId
    val impersonatedSessions by SessionEntity optionalReferrersOn SessionTable.impersonatedBy

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
