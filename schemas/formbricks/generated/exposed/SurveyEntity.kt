package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SurveyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SurveyEntity>(SurveyTable)

    var hiddenFields by SurveyTable.hiddenFields
    var variables by SurveyTable.variables
    var ownershipInitialized by SurveyTable.ownershipInitialized
    var createdAt by SurveyTable.createdAt
    var updatedAt by SurveyTable.updatedAt
    var name by SurveyTable.name
    var redirectUrl by SurveyTable.redirectUrl
    var type by SurveyTable.type
    var workspace by WorkspaceEntity referencedOn SurveyTable.workspaceId
    var workspaceId by SurveyTable.workspaceId
    var creator by UserEntity optionalReferencedOn SurveyTable.createdBy
    var createdBy by SurveyTable.createdBy
    var visibility by SurveyTable.visibility
    var owner by UserEntity optionalReferencedOn SurveyTable.ownerId
    var ownerId by SurveyTable.ownerId
    var visibilityVersion by SurveyTable.visibilityVersion
    var visibilityProjectedVersion by SurveyTable.visibilityProjectedVersion
    var visibilityPending by SurveyTable.visibilityPending
    var visibilityChangedAt by SurveyTable.visibilityChangedAt
    var visibilityChangedById by SurveyTable.visibilityChangedById
    var status by SurveyTable.status
    var welcomeCard by SurveyTable.welcomeCard
    var questions by SurveyTable.questions
    var blocks by SurveyTable.blocks
    var endings by SurveyTable.endings
    val responses by ResponseEntity referrersOn ResponseTable.surveyId
    val quotas by SurveyQuotaEntity referrersOn SurveyQuotaTable.surveyId
    var displayOption by SurveyTable.displayOption
    var recontactDays by SurveyTable.recontactDays
    var displayLimit by SurveyTable.displayLimit
    val triggers by SurveyTriggerEntity referrersOn SurveyTriggerTable.surveyId
    var inlineTriggers by SurveyTable.inlineTriggers
    val attributeFilters by SurveyAttributeFilterEntity referrersOn SurveyAttributeFilterTable.surveyId
    val displays by DisplayEntity referrersOn DisplayTable.surveyId
    var autoClose by SurveyTable.autoClose
    var keepEndingCardOpen by SurveyTable.keepEndingCardOpen
    var autoComplete by SurveyTable.autoComplete
    var delay by SurveyTable.delay
    var publishOn by SurveyTable.publishOn
    var closeOn by SurveyTable.closeOn
    var archivedAt by SurveyTable.archivedAt
    var surveyClosedMessage by SurveyTable.surveyClosedMessage
    var segmentId by SurveyTable.segmentId
    var segment by SegmentEntity optionalReferencedOn SurveyTable.segmentId
    var workspaceOverwrites by SurveyTable.workspaceOverwrites
    var styling by SurveyTable.styling
    var singleUse by SurveyTable.singleUse
    var isVerifyEmailEnabled by SurveyTable.isVerifyEmailEnabled
    var isBackButtonHidden by SurveyTable.isBackButtonHidden
    var isAutoProgressingEnabled by SurveyTable.isAutoProgressingEnabled
    var isCaptureIpEnabled by SurveyTable.isCaptureIpEnabled
    var isAnonymizeResponsesEnabled by SurveyTable.isAnonymizeResponsesEnabled
    var pin by SurveyTable.pin
    var displayPercentage by SurveyTable.displayPercentage
    val languages by SurveyLanguageEntity referrersOn SurveyLanguageTable.surveyId
    var showLanguageSwitch by SurveyTable.showLanguageSwitch
    var autoSelectLanguage by SurveyTable.autoSelectLanguage
    val followUps by SurveyFollowUpEntity referrersOn SurveyFollowUpTable.surveyId
    var recaptcha by SurveyTable.recaptcha
    var metadata by SurveyTable.metadata
    val retentionExemptions by RetentionExemptionEntity optionalReferrersOn RetentionExemptionTable.surveyId
    val retentionNotices by RetentionNoticeEntity optionalReferrersOn RetentionNoticeTable.surveyId
    val retentionRestore by RetentionSurveyRestoreEntity optionalBackReferencedOn RetentionSurveyRestoreTable.id
    var slug by SurveyTable.slug
    var customHeadScripts by SurveyTable.customHeadScripts
    var customHeadScriptsMode by SurveyTable.customHeadScriptsMode
    var customCss by SurveyTable.customCss
    var showConditions by SurveyTable.showConditions
    var startLogic by SurveyTable.startLogic

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SurveyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
