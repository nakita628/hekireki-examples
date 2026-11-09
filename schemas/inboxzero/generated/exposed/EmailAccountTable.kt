package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailAccountTable : IdTable<String>("\"EmailAccount\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val email = text("email").uniqueIndex("\"EmailAccount_email_key\"")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val name = text("name").nullable()
    val image = text("image").nullable()
    val sortOrder = integer("sortOrder").nullable()
    val about = text("about").nullable()
    val writingStyle = text("writingStyle").nullable()
    val signature = text("signature").nullable()
    val sentSignatureScan = jsonb<String>("sentSignatureScan", { it }, { it }).nullable()
    val sentSignatureScannedAt = pgTimestamp("sentSignatureScannedAt", 3).nullable()
    val includeReferralSignature = bool("includeReferralSignature").default(false)
    val includeSentWithSignature = bool("includeSentWithSignature").default(true)
    val sentMessageOpenTrackingEnabled = bool("sentMessageOpenTrackingEnabled").default(true)
    val watchEmailsExpirationDate = pgTimestamp("watchEmailsExpirationDate", 3).nullable()
    val watchEmailsSubscriptionId = text("watchEmailsSubscriptionId").nullable()
    val watchEmailsSubscriptionHistory = jsonb<String>("watchEmailsSubscriptionHistory", { it }, { it }).nullable()
    val lastSyncedHistoryId = text("lastSyncedHistoryId").nullable()
    val behaviorProfile = jsonb<String>("behaviorProfile", { it }, { it }).nullable()
    val personaAnalysis = jsonb<String>("personaAnalysis", { it }, { it }).nullable()
    val role = text("role").nullable()
    val timezone = text("timezone").nullable()
    val calendarBookingLink = text("calendarBookingLink").nullable()
    val draftDocsUrls = array("draftDocsUrls", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    val statsEmailFrequency = pgEnum("statsEmailFrequency", "\"Frequency\"", Frequency.entries, Frequency::dbName).default(Frequency.WEEKLY)
    val summaryEmailFrequency =
        pgEnum("summaryEmailFrequency", "\"Frequency\"", Frequency.entries, Frequency::dbName).default(Frequency.WEEKLY)
    val lastSummaryEmailAt = pgTimestamp("lastSummaryEmailAt", 3).nullable()
    val lastInboxHealthEmailAt = pgTimestamp("lastInboxHealthEmailAt", 3).nullable()
    val coldEmailBlocker = pgEnum("coldEmailBlocker", "\"ColdEmailSetting\"", ColdEmailSetting.entries, ColdEmailSetting::dbName).nullable()
    val coldEmailDigest = bool("coldEmailDigest").default(false)
    val coldEmailPrompt = text("coldEmailPrompt").nullable()
    val rulesPrompt = text("rulesPrompt").nullable()
    val autoCategorizeSenders = bool("autoCategorizeSenders").default(false)
    val multiRuleSelectionEnabled = bool("multiRuleSelectionEnabled").default(false)
    val sensitiveDataPolicy = text("sensitiveDataPolicy").nullable()
    val draftReplyConfidence =
        pgEnum("draftReplyConfidence", "\"DraftReplyConfidence\"", DraftReplyConfidence.entries, DraftReplyConfidence::dbName).default(
            DraftReplyConfidence.ALL_EMAILS,
        )
    val draftCleanupDays = integer("draftCleanupDays").nullable().default(14)
    val allowHiddenAiDraftLinks = bool("allowHiddenAiDraftLinks").default(false)
    val rulesRevision = integer("rulesRevision").default(0)
    val mailLayout = pgEnum("mailLayout", "\"MailLayout\"", MailLayout.entries, MailLayout::dbName).default(MailLayout.LIST)
    val mailExpandedPreview = bool("mailExpandedPreview").default(false)
    val mailBundleInbox = bool("mailBundleInbox").nullable()
    val mailDefaultSplitId = text("mailDefaultSplitId").nullable()
    val mailSetup =
        pgEnum("mailSetup", "\"MailSetupState\"", MailSetupState.entries, MailSetupState::dbName).default(MailSetupState.PENDING)
    val includeInAllAccounts = bool("includeInAllAccounts").default(true)
    val meetingBriefingsEnabled = bool("meetingBriefingsEnabled").default(false)
    val meetingBriefingsMinutesBefore = integer("meetingBriefingsMinutesBefore").default(240)
    val meetingBriefsSendEmail = bool("meetingBriefsSendEmail").default(true)
    val meetingRecorderEnabled = bool("meetingRecorderEnabled").default(false)
    val meetingRecorderJoinRule =
        pgEnum("meetingRecorderJoinRule", "\"MeetingJoinRule\"", MeetingJoinRule.entries, MeetingJoinRule::dbName).default(
            MeetingJoinRule.ALL,
        )
    val meetingRecorderRecapEmailEnabled = bool("meetingRecorderRecapEmailEnabled").default(true)
    val meetingRecorderFollowUpDraftEnabled = bool("meetingRecorderFollowUpDraftEnabled").default(true)
    val filingEnabled = bool("filingEnabled").default(false)
    val filingPrompt = text("filingPrompt").nullable()
    val filingConfirmationSendEmail = bool("filingConfirmationSendEmail").default(true)
    val followUpAwaitingReplyDays = double("followUpAwaitingReplyDays").nullable()
    val followUpNeedsReplyDays = double("followUpNeedsReplyDays").nullable()
    val followUpAutoDraftEnabled = bool("followUpAutoDraftEnabled").default(true)
    val digestSendEmail = bool("digestSendEmail").default(true)
    val learnedWritingStyle = text("learnedWritingStyle").nullable()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmailAccount_userId_fkey\"",
        )
    val accountId =
        reference(
            "accountId",
            AccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmailAccount_accountId_fkey\"",
        ).uniqueIndex("\"EmailAccount_accountId_key\"")
    override val primaryKey = PrimaryKey(id, name = "\"EmailAccount_pkey\"")

    init {
        index("\"EmailAccount_userId_idx\"", false, userId)
        index("\"EmailAccount_lastSummaryEmailAt_idx\"", false, lastSummaryEmailAt)
        index("\"EmailAccount_lastInboxHealthEmailAt_idx\"", false, lastInboxHealthEmailAt)
        index("\"EmailAccount_watchEmailsSubscriptionId_idx\"", false, watchEmailsSubscriptionId)
        index(
            "\"EmailAccount_watchEmailsSubscriptionHistory_idx\"",
            false,
            functions = listOf(indexExpression("\"watchEmailsSubscriptionHistory\" jsonb_path_ops")),
            indexType = "gin",
        )
    }
}
