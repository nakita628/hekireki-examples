package typebot.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object RuntimeMediaIdCacheTable : CompositeIdTable("\"RuntimeMediaIdCache\"") {
    val provider = pgEnum("provider", "\"ChatProvider\"", ChatProvider.entries, ChatProvider::dbName).entityId()
    val url = text("url").entityId()
    val mediaId = text("mediaId")
    val expiresAt = pgTimestamp("expiresAt", 3).nullable()
    val publicTypebotId =
        reference(
            "publicTypebotId",
            PublicTypebotTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"RuntimeMediaIdCache_publicTypebotId_fkey\"",
        )

    init {
        addIdColumn(publicTypebotId)
        index("\"RuntimeMediaIdCache_expiresAt_idx\"", false, expiresAt)
        uniqueIndex("\"RuntimeMediaIdCache_publicTypebotId_provider_url_key\"", publicTypebotId, provider, url)
    }
}
