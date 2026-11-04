package teable.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TemplateCategoryTable : IdTable<String>("template_category") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val name = text("name").uniqueIndex("template_category_name_key")
    val createdTime = pgTimestamp("created_time", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val createdBy = text("created_by")
    val lastModifiedTime = pgTimestamp("last_modified_time", 3).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastModifiedBy = text("last_modified_by").nullable()
    val order = double("order")
    val parentId = text("parent_id").nullable()
    val kind = text("kind").default("industry")
    val key = text("key").nullable().uniqueIndex("template_category_key_key")
    override val primaryKey = PrimaryKey(id, name = "template_category_pkey")

    init {
        index("template_category_parent_id_idx", false, parentId)
    }
}
