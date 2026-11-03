package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object InstanceSettingsTable : IdTable<Int>("instance_settings") {
    override val id = integer("id").default(1).entityId()
    val disableUserRegistration = bool("disable_user_registration").default(false)
    val instanceId = javaUUID("instance_id").databaseDefault("gen_random_uuid()").uniqueIndex("instance_settings_instance_id_key")
    val appName = text("app_name").nullable()
    val primaryColor = text("primary_color").nullable()
    val primaryColorDark = text("primary_color_dark").nullable()
    val logo = text("logo").nullable()
    val logoDark = text("logo_dark").nullable()
    val logoIcon = text("logo_icon").nullable()
    val hideAttribution = bool("hide_attribution").nullable()
    val footerLinks = jsonb<String>("footer_links", { it }, { it }).default("[]")
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "instance_settings_pkey")
}
