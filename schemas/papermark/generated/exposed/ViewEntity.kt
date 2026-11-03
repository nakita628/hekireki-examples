package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ViewEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ViewEntity>(ViewTable)

    var link by LinkEntity referencedOn ViewTable.linkId
    var linkId by ViewTable.linkId
    var document by DocumentEntity optionalReferencedOn ViewTable.documentId
    var documentId by ViewTable.documentId
    var dataroom by DataroomEntity optionalReferencedOn ViewTable.dataroomId
    var dataroomId by ViewTable.dataroomId
    var dataroomViewId by ViewTable.dataroomViewId
    var viewerEmail by ViewTable.viewerEmail
    var viewerName by ViewTable.viewerName
    var verified by ViewTable.verified
    var viewedAt by ViewTable.viewedAt
    var downloadedAt by ViewTable.downloadedAt
    var downloadType by ViewTable.downloadType
    var downloadMetadata by ViewTable.downloadMetadata
    val reactions by ReactionEntity referrersOn ReactionTable.viewId
    var viewType by ViewTable.viewType
    var viewerId by ViewTable.viewerId
    var viewer by ViewerEntity optionalReferencedOn ViewTable.viewerId
    var groupId by ViewTable.groupId
    var group by ViewerGroupEntity optionalReferencedOn ViewTable.groupId
    val feedbackResponse by FeedbackResponseEntity optionalBackReferencedOn FeedbackResponseTable.viewId
    val agreementResponse by AgreementResponseEntity optionalBackReferencedOn AgreementResponseTable.viewId
    val customFieldResponse by CustomFieldResponseEntity optionalBackReferencedOn CustomFieldResponseTable.viewId
    var isArchived by ViewTable.isArchived
    val conversationViews by ConversationViewEntity referrersOn ConversationViewTable.viewId
    val messages by MessageEntity optionalReferrersOn MessageTable.viewId
    val initialConversations by ConversationEntity optionalReferrersOn ConversationTable.initialViewId
    val uploadedDocuments by DocumentUploadEntity optionalReferrersOn DocumentUploadTable.viewId
    val taskActivities by TaskActivityEntity optionalReferrersOn TaskActivityTable.viewId
    val chats by ChatEntity optionalReferrersOn ChatTable.viewId
    var teamId by ViewTable.teamId
    var team by TeamEntity optionalReferencedOn ViewTable.teamId
}
