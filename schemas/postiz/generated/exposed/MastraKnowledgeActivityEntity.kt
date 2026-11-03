package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraKnowledgeActivityEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraKnowledgeActivityEntity>(MastraKnowledgeActivityTable)

    var action by MastraKnowledgeActivityTable.action
    var recordType by MastraKnowledgeActivityTable.recordType
    var recordId by MastraKnowledgeActivityTable.recordId
    var scope by MastraKnowledgeActivityTable.scope
    var scopeKey by MastraKnowledgeActivityTable.scopeKey
    var sourceThreadId by MastraKnowledgeActivityTable.sourceThreadId
    var createdAt by MastraKnowledgeActivityTable.createdAt
    var createdAtZ by MastraKnowledgeActivityTable.createdAtZ
}
