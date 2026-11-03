package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object PluginInstallTable : IdTable<String>("plugin_install") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val pluginId =
        reference(
            "plugin_id",
            PluginTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "plugin_install_plugin_id_fkey",
        )
    val baseId = text("base_id")
    val name = text("name")
    val positionId = text("position_id")
    val position = text("position")
    val storage = text("storage").nullable()
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    override val primaryKey = PrimaryKey(id, name = "plugin_install_pkey")

    init {
        index("plugin_install_position_id_idx", false, positionId)
        index("plugin_install_base_id_idx", false, baseId)
    }
}
