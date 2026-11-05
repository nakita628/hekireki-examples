package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConversationCommentEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConversationCommentEntity>(ConversationCommentTable)

    var conversationId by ConversationCommentTable.conversationId
    var conversation by ConversationEntity referencedOn ConversationCommentTable.conversationId
    var authorMemberId by ConversationCommentTable.authorMemberId
    var author by MemberEntity optionalReferencedOn ConversationCommentTable.authorMemberId
    var clientMutationId by ConversationCommentTable.clientMutationId
    var body by ConversationCommentTable.body
    var revision by ConversationCommentTable.revision
    var deletedAt by ConversationCommentTable.deletedAt
    var createdAt by ConversationCommentTable.createdAt
    val activity by ConversationActivityEntity optionalReferrersOn ConversationActivityTable.commentId
}
