package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataroomEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataroomEntity>(DataroomTable)

    var pId by DataroomTable.pId
    var name by DataroomTable.name
    var internalName by DataroomTable.internalName
    var description by DataroomTable.description
    var teamId by DataroomTable.teamId
    var team by TeamEntity referencedOn DataroomTable.teamId
    val documents by DataroomDocumentEntity referrersOn DataroomDocumentTable.dataroomId
    val folders by DataroomFolderEntity referrersOn DataroomFolderTable.dataroomId
    val links by LinkEntity optionalReferrersOn LinkTable.dataroomId
    val views by ViewEntity optionalReferrersOn ViewTable.dataroomId
    val viewers by ViewerEntity optionalReferrersOn ViewerTable.dataroomId
    val viewerGroups by ViewerGroupEntity referrersOn ViewerGroupTable.dataroomId
    val brand by DataroomBrandEntity optionalBackReferencedOn DataroomBrandTable.dataroomId
    var brandId by DataroomTable.brandId
    var baseBrand by BrandEntity optionalReferencedOn DataroomTable.brandId
    val userDatarooms by UserDataroomEntity referrersOn UserDataroomTable.dataroomId
    val permissionGroups by PermissionGroupEntity referrersOn PermissionGroupTable.dataroomId
    var conversationsEnabled by DataroomTable.conversationsEnabled
    val conversations by ConversationEntity referrersOn ConversationTable.dataroomId
    val questions by DataroomQuestionEntity referrersOn DataroomQuestionTable.dataroomId
    val faqItems by DataroomFaqItemEntity referrersOn DataroomFaqItemTable.dataroomId
    var agentsEnabled by DataroomTable.agentsEnabled
    var vectorStoreId by DataroomTable.vectorStoreId
    val chats by ChatEntity optionalReferrersOn ChatTable.dataroomId
    val uploadedDocuments by DocumentUploadEntity optionalReferrersOn DocumentUploadTable.dataroomId
    val tags by TagItemEntity optionalReferrersOn TagItemTable.dataroomId
    var requestListEnabled by DataroomTable.requestListEnabled
    val taskLists by TaskListEntity referrersOn TaskListTable.dataroomId
    val tasks by TaskEntity referrersOn TaskTable.dataroomId
    var enableChangeNotifications by DataroomTable.enableChangeNotifications
    var enableVisitorUploadChangeNotifications by DataroomTable.enableVisitorUploadChangeNotifications
    var defaultPermissionStrategy by DataroomTable.defaultPermissionStrategy
    var defaultShowBanner by DataroomTable.defaultShowBanner
    var defaultGroupPermissionStrategy by DataroomTable.defaultGroupPermissionStrategy
    var defaultRootItemAccess by DataroomTable.defaultRootItemAccess
    var defaultGroupRootItemAccess by DataroomTable.defaultGroupRootItemAccess
    var allowBulkDownload by DataroomTable.allowBulkDownload
    var showLastUpdated by DataroomTable.showLastUpdated
    var introductionEnabled by DataroomTable.introductionEnabled
    var introductionContent by DataroomTable.introductionContent
    var isFrozen by DataroomTable.isFrozen
    var frozenAt by DataroomTable.frozenAt
    var frozenBy by DataroomTable.frozenBy
    var frozenByUser by UserEntity optionalReferencedOn DataroomTable.frozenBy
    var freezeArchiveUrl by DataroomTable.freezeArchiveUrl
    var freezeArchiveHash by DataroomTable.freezeArchiveHash
    var createdAt by DataroomTable.createdAt
    var updatedAt by DataroomTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataroomTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
