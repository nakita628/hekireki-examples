package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class EmailAccountEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailAccountEntity>(EmailAccountTable)

    var email by EmailAccountTable.email
    var createdAt by EmailAccountTable.createdAt
    var updatedAt by EmailAccountTable.updatedAt
    var name by EmailAccountTable.name
    var image by EmailAccountTable.image
    var sortOrder by EmailAccountTable.sortOrder
    var about by EmailAccountTable.about
    var writingStyle by EmailAccountTable.writingStyle
    var signature by EmailAccountTable.signature
    var sentSignatureScan by EmailAccountTable.sentSignatureScan
    var sentSignatureScannedAt by EmailAccountTable.sentSignatureScannedAt
    var includeReferralSignature by EmailAccountTable.includeReferralSignature
    var includeSentWithSignature by EmailAccountTable.includeSentWithSignature
    var sentMessageOpenTrackingEnabled by EmailAccountTable.sentMessageOpenTrackingEnabled
    var watchEmailsExpirationDate by EmailAccountTable.watchEmailsExpirationDate
    var watchEmailsSubscriptionId by EmailAccountTable.watchEmailsSubscriptionId
    var watchEmailsSubscriptionHistory by EmailAccountTable.watchEmailsSubscriptionHistory
    var lastSyncedHistoryId by EmailAccountTable.lastSyncedHistoryId
    var behaviorProfile by EmailAccountTable.behaviorProfile
    var personaAnalysis by EmailAccountTable.personaAnalysis
    var role by EmailAccountTable.role
    var timezone by EmailAccountTable.timezone
    var calendarBookingLink by EmailAccountTable.calendarBookingLink
    var draftDocsUrls by EmailAccountTable.draftDocsUrls
    var statsEmailFrequency by EmailAccountTable.statsEmailFrequency
    var summaryEmailFrequency by EmailAccountTable.summaryEmailFrequency
    var lastSummaryEmailAt by EmailAccountTable.lastSummaryEmailAt
    var lastInboxHealthEmailAt by EmailAccountTable.lastInboxHealthEmailAt
    var coldEmailBlocker by EmailAccountTable.coldEmailBlocker
    var coldEmailDigest by EmailAccountTable.coldEmailDigest
    var coldEmailPrompt by EmailAccountTable.coldEmailPrompt
    var rulesPrompt by EmailAccountTable.rulesPrompt
    var autoCategorizeSenders by EmailAccountTable.autoCategorizeSenders
    var multiRuleSelectionEnabled by EmailAccountTable.multiRuleSelectionEnabled
    var sensitiveDataPolicy by EmailAccountTable.sensitiveDataPolicy
    var draftReplyConfidence by EmailAccountTable.draftReplyConfidence
    var draftCleanupDays by EmailAccountTable.draftCleanupDays
    var allowHiddenAiDraftLinks by EmailAccountTable.allowHiddenAiDraftLinks
    var rulesRevision by EmailAccountTable.rulesRevision
    var mailLayout by EmailAccountTable.mailLayout
    var mailExpandedPreview by EmailAccountTable.mailExpandedPreview
    var mailBundleInbox by EmailAccountTable.mailBundleInbox
    var mailDefaultSplitId by EmailAccountTable.mailDefaultSplitId
    var mailSetup by EmailAccountTable.mailSetup
    var includeInAllAccounts by EmailAccountTable.includeInAllAccounts
    var meetingBriefingsEnabled by EmailAccountTable.meetingBriefingsEnabled
    var meetingBriefingsMinutesBefore by EmailAccountTable.meetingBriefingsMinutesBefore
    var meetingBriefsSendEmail by EmailAccountTable.meetingBriefsSendEmail
    var meetingRecorderEnabled by EmailAccountTable.meetingRecorderEnabled
    var meetingRecorderJoinRule by EmailAccountTable.meetingRecorderJoinRule
    var meetingRecorderRecapEmailEnabled by EmailAccountTable.meetingRecorderRecapEmailEnabled
    var meetingRecorderFollowUpDraftEnabled by EmailAccountTable.meetingRecorderFollowUpDraftEnabled
    var filingEnabled by EmailAccountTable.filingEnabled
    var filingPrompt by EmailAccountTable.filingPrompt
    var filingConfirmationSendEmail by EmailAccountTable.filingConfirmationSendEmail
    var followUpAwaitingReplyDays by EmailAccountTable.followUpAwaitingReplyDays
    var followUpNeedsReplyDays by EmailAccountTable.followUpNeedsReplyDays
    var followUpAutoDraftEnabled by EmailAccountTable.followUpAutoDraftEnabled
    var digestSendEmail by EmailAccountTable.digestSendEmail
    val digestSchedule by ScheduleEntity optionalBackReferencedOn ScheduleTable.emailAccountId
    var learnedWritingStyle by EmailAccountTable.learnedWritingStyle
    var userId by EmailAccountTable.userId
    var user by UserEntity referencedOn EmailAccountTable.userId
    var accountId by EmailAccountTable.accountId
    var account by AccountEntity referencedOn EmailAccountTable.accountId
    val labels by LabelEntity referrersOn LabelTable.emailAccountId
    val rules by RuleEntity referrersOn RuleTable.emailAccountId
    val executedRules by ExecutedRuleEntity referrersOn ExecutedRuleTable.emailAccountId
    val newsletters by NewsletterEntity referrersOn NewsletterTable.emailAccountId
    val coldEmails by ColdEmailEntity referrersOn ColdEmailTable.emailAccountId
    val groups by GroupEntity referrersOn GroupTable.emailAccountId
    val categories by CategoryEntity referrersOn CategoryTable.emailAccountId
    val threadTrackers by ThreadTrackerEntity referrersOn ThreadTrackerTable.emailAccountId
    val cleanupJobs by CleanupJobEntity referrersOn CleanupJobTable.emailAccountId
    val cleanupThreads by CleanupThreadEntity referrersOn CleanupThreadTable.emailAccountId
    val emailMessages by EmailMessageEntity referrersOn EmailMessageTable.emailAccountId
    val emailTokens by EmailTokenEntity referrersOn EmailTokenTable.emailAccountId
    val knowledge by KnowledgeEntity referrersOn KnowledgeTable.emailAccountId
    val snippets by SnippetEntity referrersOn SnippetTable.emailAccountId
    val emailSignatures by EmailSignatureEntity referrersOn EmailSignatureTable.emailAccountId
    val replyMemories by ReplyMemoryEntity referrersOn ReplyMemoryTable.emailAccountId
    val chats by ChatEntity referrersOn ChatTable.emailAccountId
    val chatMemories by ChatMemoryEntity referrersOn ChatMemoryTable.emailAccountId
    val digests by DigestEntity referrersOn DigestTable.emailAccountId
    val scheduledActions by ScheduledActionEntity referrersOn ScheduledActionTable.emailAccountId
    val snoozedThreads by SnoozedThreadEntity referrersOn SnoozedThreadTable.emailAccountId
    val emailSendOperations by EmailSendOperationEntity referrersOn EmailSendOperationTable.emailAccountId
    val sentMessageOpens by SentMessageOpenEntity referrersOn SentMessageOpenTable.emailAccountId
    val scheduledEmails by ScheduledEmailEntity referrersOn ScheduledEmailTable.emailAccountId
    val responseTimes by ResponseTimeEntity referrersOn ResponseTimeTable.emailAccountId
    val actions by ActionEntity referrersOn ActionTable.emailAccountId
    val members by MemberEntity referrersOn MemberTable.emailAccountId
    val invitations by InvitationEntity referrersOn InvitationTable.inviterId
    val ssoproviders by SsoProviderEntity optionalReferrersOn SsoProviderTable.emailAccountId
    val calendarConnections by CalendarConnectionEntity referrersOn CalendarConnectionTable.emailAccountId
    val availabilitySchedules by AvailabilityScheduleEntity referrersOn AvailabilityScheduleTable.emailAccountId
    val bookingLinks by BookingLinkEntity referrersOn BookingLinkTable.emailAccountId
    val bookings by BookingEntity referrersOn BookingTable.emailAccountId
    val mcpConnections by McpConnectionEntity optionalReferrersOn McpConnectionTable.emailAccountId
    val mcpIntegrations by McpIntegrationEntity optionalReferrersOn McpIntegrationTable.emailAccountId
    val meetingBriefings by MeetingBriefingEntity referrersOn MeetingBriefingTable.emailAccountId
    val meetings by MeetingEntity referrersOn MeetingTable.emailAccountId
    val meetingRecordings by MeetingRecordingEntity referrersOn MeetingRecordingTable.emailAccountId
    val driveConnections by DriveConnectionEntity referrersOn DriveConnectionTable.emailAccountId
    val messagingChannels by MessagingChannelEntity referrersOn MessagingChannelTable.emailAccountId
    val documentFilings by DocumentFilingEntity referrersOn DocumentFilingTable.emailAccountId
    val filingFolders by FilingFolderEntity referrersOn FilingFolderTable.emailAccountId
    val apiKeys by ApiKeyEntity optionalReferrersOn ApiKeyTable.emailAccountId
    val classificationFeedback by ClassificationFeedbackEntity referrersOn ClassificationFeedbackTable.emailAccountId
    val mailSplits by MailSplitEntity referrersOn MailSplitTable.emailAccountId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(EmailAccountTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
