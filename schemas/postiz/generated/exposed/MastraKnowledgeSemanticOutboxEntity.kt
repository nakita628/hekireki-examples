package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraKnowledgeSemanticOutboxEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraKnowledgeSemanticOutboxEntity>(MastraKnowledgeSemanticOutboxTable)

    var idempotencyKey by MastraKnowledgeSemanticOutboxTable.idempotencyKey
    var documentId by MastraKnowledgeSemanticOutboxTable.documentId
    var documentType by MastraKnowledgeSemanticOutboxTable.documentType
    var operation by MastraKnowledgeSemanticOutboxTable.operation
    var scope by MastraKnowledgeSemanticOutboxTable.scope
    var scopeKey by MastraKnowledgeSemanticOutboxTable.scopeKey
    var status by MastraKnowledgeSemanticOutboxTable.status
    var attempts by MastraKnowledgeSemanticOutboxTable.attempts
    var availableAt by MastraKnowledgeSemanticOutboxTable.availableAt
    var claimedAt by MastraKnowledgeSemanticOutboxTable.claimedAt
    var claimedBy by MastraKnowledgeSemanticOutboxTable.claimedBy
    var createdAt by MastraKnowledgeSemanticOutboxTable.createdAt
    var completedAt by MastraKnowledgeSemanticOutboxTable.completedAt
    var availableAtZ by MastraKnowledgeSemanticOutboxTable.availableAtZ
    var claimedAtZ by MastraKnowledgeSemanticOutboxTable.claimedAtZ
    var createdAtZ by MastraKnowledgeSemanticOutboxTable.createdAtZ
    var completedAtZ by MastraKnowledgeSemanticOutboxTable.completedAtZ
}
