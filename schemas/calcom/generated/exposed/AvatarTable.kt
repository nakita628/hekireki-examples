package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AvatarTable : IdTable<String>("avatars") {
    val teamId = integer("teamId").default(0)
    val userId = integer("userId").default(0)
    val data = text("data")
    override val id = text("objectKey").entityId()
    val isBanner = bool("isBanner").default(false)

    init {
        uniqueIndex("\"avatars_objectKey_key\"", id)
        uniqueIndex("\"avatars_teamId_userId_isBanner_key\"", teamId, userId, isBanner)
    }
}
