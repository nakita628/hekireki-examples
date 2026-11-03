package formbricks.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object CreativeDailyPageStatTable : CompositeIdTable("\"CreativeDailyPageStat\"") {
    val creativeId =
        reference(
            "creativeId",
            CreativeTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"CreativeDailyPageStat_creativeId_fkey\"",
        )
    val date = pgDate("date").entityId()
    val page = text("page").entityId()
    val impressions = integer("impressions").default(0)
    val clicks = integer("clicks").default(0)
    override val primaryKey = PrimaryKey(creativeId, date, page, name = "\"CreativeDailyPageStat_pkey\"")

    init {
        addIdColumn(creativeId)
    }
}
