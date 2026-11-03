package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object MastraKnowledgeMentionsTable : CompositeIdTable("mastra_knowledge_mentions") {
    val sourceType = text("sourceType").entityId()
    val sourceId = text("sourceId").entityId()
    val recordId = text("recordId").entityId()
    override val primaryKey = PrimaryKey(sourceType, sourceId, recordId, name = "mastra_knowledge_mentions_pkey")

    init {
        index("idx_knowledge_mentions_record", false, recordId, sourceType, sourceId)
    }
}
