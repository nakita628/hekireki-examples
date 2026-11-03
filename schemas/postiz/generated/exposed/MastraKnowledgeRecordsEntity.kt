package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class MastraKnowledgeRecordsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, MastraKnowledgeRecordsEntity>(MastraKnowledgeRecordsTable)

    var node by MastraKnowledgeRecordsTable.node
    var text by MastraKnowledgeRecordsTable.text
    var scope by MastraKnowledgeRecordsTable.scope
    var scopeKey by MastraKnowledgeRecordsTable.scopeKey
    var sourceThreadId by MastraKnowledgeRecordsTable.sourceThreadId
    var capturedAt by MastraKnowledgeRecordsTable.capturedAt
    var `when` by MastraKnowledgeRecordsTable.`when`
    var maxScope by MastraKnowledgeRecordsTable.maxScope
    var metadata by MastraKnowledgeRecordsTable.metadata
    var deletedAt by MastraKnowledgeRecordsTable.deletedAt
    var deletedBy by MastraKnowledgeRecordsTable.deletedBy
    var capturedAtZ by MastraKnowledgeRecordsTable.capturedAtZ
    var whenZ by MastraKnowledgeRecordsTable.whenZ
    var deletedAtZ by MastraKnowledgeRecordsTable.deletedAtZ
}
