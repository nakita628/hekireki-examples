package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LinkEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LinkEntity>(LinkTable)

    var document by DocumentEntity optionalReferencedOn LinkTable.documentId
    var documentId by LinkTable.documentId
    var dataroom by DataroomEntity optionalReferencedOn LinkTable.dataroomId
    var dataroomId by LinkTable.dataroomId
    var linkType by LinkTable.linkType
    var url by LinkTable.url
    var name by LinkTable.name
    var slug by LinkTable.slug
    var expiresAt by LinkTable.expiresAt
    var password by LinkTable.password
    var allowList by LinkTable.allowList
    var denyList by LinkTable.denyList
    var emailProtected by LinkTable.emailProtected
    var emailAuthenticated by LinkTable.emailAuthenticated
    var allowDownload by LinkTable.allowDownload
    var isArchived by LinkTable.isArchived
    var deletedAt by LinkTable.deletedAt
    val views by ViewEntity referrersOn ViewTable.linkId
    var domain by DomainEntity optionalReferencedOn LinkTable.domainId
    var domainId by LinkTable.domainId
    var domainSlug by LinkTable.domainSlug
    var createdAt by LinkTable.createdAt
    var updatedAt by LinkTable.updatedAt
    var enableNotification by LinkTable.enableNotification
    var enableFeedback by LinkTable.enableFeedback
    var enableQuestion by LinkTable.enableQuestion
    var enableScreenshotProtection by LinkTable.enableScreenshotProtection
    var enableConfidentialView by LinkTable.enableConfidentialView
    val feedback by FeedbackEntity optionalBackReferencedOn FeedbackTable.linkId
    var enableAgreement by LinkTable.enableAgreement
    var agreement by AgreementEntity optionalReferencedOn LinkTable.agreementId
    var agreementId by LinkTable.agreementId
    var showBanner by LinkTable.showBanner
    var enableWatermark by LinkTable.enableWatermark
    var watermarkConfig by LinkTable.watermarkConfig
    var audienceType by LinkTable.audienceType
    var groupId by LinkTable.groupId
    var group by ViewerGroupEntity optionalReferencedOn LinkTable.groupId
    var permissionGroupId by LinkTable.permissionGroupId
    var permissionGroup by PermissionGroupEntity optionalReferencedOn LinkTable.permissionGroupId
    var metaTitle by LinkTable.metaTitle
    var metaDescription by LinkTable.metaDescription
    var metaImage by LinkTable.metaImage
    var metaFavicon by LinkTable.metaFavicon
    var enableCustomMetatag by LinkTable.enableCustomMetatag
    var welcomeMessage by LinkTable.welcomeMessage
    var enableConversation by LinkTable.enableConversation
    val conversations by ConversationEntity optionalReferrersOn ConversationTable.linkId
    val questionAssignments by DataroomQuestionAssignmentEntity optionalReferrersOn DataroomQuestionAssignmentTable.linkId
    val dataroomFaqItems by DataroomFaqItemEntity optionalReferrersOn DataroomFaqItemTable.linkId
    var enableAIAgents by LinkTable.enableAIAgents
    val chats by ChatEntity optionalReferrersOn ChatTable.linkId
    var enableUpload by LinkTable.enableUpload
    var isFileRequestOnly by LinkTable.isFileRequestOnly
    var uploadFolderId by LinkTable.uploadFolderId
    var uploadFolderIds by LinkTable.uploadFolderIds
    var enableIndexFile by LinkTable.enableIndexFile
    val uploadedDocuments by DocumentUploadEntity referrersOn DocumentUploadTable.linkId
    var teamId by LinkTable.teamId
    var team by TeamEntity optionalReferencedOn LinkTable.teamId
    var brandId by LinkTable.brandId
    var brand by BrandEntity optionalReferencedOn LinkTable.brandId
    var ownerId by LinkTable.ownerId
    var owner by UserEntity optionalReferencedOn LinkTable.ownerId
    val customFields by CustomFieldEntity referrersOn CustomFieldTable.linkId
    val tags by TagItemEntity optionalReferrersOn TagItemTable.linkId
    val viewerInvitations by ViewerInvitationEntity referrersOn ViewerInvitationTable.linkId
    val visitorGroups by LinkVisitorGroupEntity referrersOn LinkVisitorGroupTable.linkId
    val taskAssignments by TaskAssignmentEntity optionalReferrersOn TaskAssignmentTable.linkId
    val workflow by WorkflowEntity optionalBackReferencedOn WorkflowTable.entryLinkId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LinkTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
