package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AppSettingTable : IdTable<String>("app_setting") {
    override val id = text("key").entityId()
    val value = text("value")
    override val primaryKey = PrimaryKey(id, name = "app_setting_pkey")
}
