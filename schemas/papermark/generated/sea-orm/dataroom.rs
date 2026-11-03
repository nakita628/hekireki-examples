use super::default_permission_strategy::DefaultPermissionStrategy;
use super::root_item_access::RootItemAccess;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "Dataroom")]
pub struct Model {
    #[sea_orm(primary_key, auto_increment = false)]
    pub id: String,
    #[sea_orm(unique)]
    #[sea_orm(column_name = "pId")]
    pub p_id: String,
    pub name: String,
    #[sea_orm(column_name = "internalName")]
    pub internal_name: Option<String>,
    pub description: Option<String>,
    #[sea_orm(column_name = "teamId")]
    pub team_id: String,
    #[sea_orm(column_name = "brandId")]
    pub brand_id: Option<String>,
    #[sea_orm(column_name = "conversationsEnabled", default_value = false)]
    pub conversations_enabled: bool,
    #[sea_orm(column_name = "agentsEnabled", default_value = false)]
    pub agents_enabled: bool,
    #[sea_orm(column_name = "vectorStoreId")]
    pub vector_store_id: Option<String>,
    #[sea_orm(column_name = "requestListEnabled", default_value = false)]
    pub request_list_enabled: bool,
    #[sea_orm(column_name = "enableChangeNotifications", default_value = false)]
    pub enable_change_notifications: bool,
    #[sea_orm(
        column_name = "enableVisitorUploadChangeNotifications",
        default_value = false
    )]
    pub enable_visitor_upload_change_notifications: bool,
    #[sea_orm(
        column_name = "defaultPermissionStrategy",
        default_value = "INHERIT_FROM_PARENT"
    )]
    pub default_permission_strategy: DefaultPermissionStrategy,
    #[sea_orm(column_name = "defaultShowBanner", default_value = true)]
    pub default_show_banner: bool,
    #[sea_orm(
        column_name = "defaultGroupPermissionStrategy",
        default_value = "INHERIT_FROM_PARENT"
    )]
    pub default_group_permission_strategy: DefaultPermissionStrategy,
    #[sea_orm(column_name = "defaultRootItemAccess", default_value = "VIEW_ONLY")]
    pub default_root_item_access: RootItemAccess,
    #[sea_orm(
        column_name = "defaultGroupRootItemAccess",
        default_value = "VIEW_ONLY"
    )]
    pub default_group_root_item_access: RootItemAccess,
    #[sea_orm(column_name = "allowBulkDownload", default_value = true)]
    pub allow_bulk_download: bool,
    #[sea_orm(column_name = "showLastUpdated", default_value = true)]
    pub show_last_updated: bool,
    #[sea_orm(column_name = "introductionEnabled", default_value = false)]
    pub introduction_enabled: bool,
    #[sea_orm(column_name = "introductionContent")]
    pub introduction_content: Option<Json>,
    #[sea_orm(column_name = "isFrozen", default_value = false)]
    pub is_frozen: bool,
    #[sea_orm(column_name = "frozenAt")]
    pub frozen_at: Option<DateTime>,
    #[sea_orm(column_name = "frozenBy")]
    pub frozen_by: Option<String>,
    #[sea_orm(column_name = "freezeArchiveUrl")]
    pub freeze_archive_url: Option<String>,
    #[sea_orm(column_name = "freezeArchiveHash")]
    pub freeze_archive_hash: Option<String>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: DateTime,
    #[sea_orm(column_name = "updatedAt")]
    pub updated_at: DateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::team::Entity",
        from = "Column::TeamId",
        to = "super::team::Column::Id",
        on_delete = "Cascade"
    )]
    Team,
    #[sea_orm(
        belongs_to = "super::brand::Entity",
        from = "Column::BrandId",
        to = "super::brand::Column::Id",
        on_delete = "SetNull"
    )]
    BaseBrand,
    #[sea_orm(
        belongs_to = "super::user::Entity",
        from = "Column::FrozenBy",
        to = "super::user::Column::Id",
        on_delete = "SetNull"
    )]
    FrozenByUser,
    #[sea_orm(has_many = "super::dataroom_document::Entity")]
    Documents,
    #[sea_orm(has_many = "super::dataroom_folder::Entity")]
    Folders,
    #[sea_orm(has_many = "super::link::Entity")]
    Links,
    #[sea_orm(has_many = "super::view::Entity")]
    Views,
    #[sea_orm(has_many = "super::viewer::Entity")]
    Viewers,
    #[sea_orm(has_many = "super::viewer_group::Entity")]
    ViewerGroups,
    #[sea_orm(has_many = "super::user_dataroom::Entity")]
    UserDatarooms,
    #[sea_orm(has_many = "super::permission_group::Entity")]
    PermissionGroups,
    #[sea_orm(has_many = "super::conversation::Entity")]
    Conversations,
    #[sea_orm(has_many = "super::dataroom_question::Entity")]
    Questions,
    #[sea_orm(has_many = "super::dataroom_faq_item::Entity")]
    FaqItems,
    #[sea_orm(has_many = "super::chat::Entity")]
    Chats,
    #[sea_orm(has_many = "super::document_upload::Entity")]
    UploadedDocuments,
    #[sea_orm(has_many = "super::tag_item::Entity")]
    Tags,
    #[sea_orm(has_many = "super::task_list::Entity")]
    TaskLists,
    #[sea_orm(has_many = "super::task::Entity")]
    Tasks,
    #[sea_orm(has_one = "super::dataroom_brand::Entity")]
    Brand,
}

impl Related<super::team::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Team.def()
    }
}

impl Related<super::brand::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::BaseBrand.def()
    }
}

impl Related<super::user::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FrozenByUser.def()
    }
}

impl Related<super::dataroom_document::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Documents.def()
    }
}

impl Related<super::dataroom_folder::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Folders.def()
    }
}

impl Related<super::link::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Links.def()
    }
}

impl Related<super::view::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Views.def()
    }
}

impl Related<super::viewer::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Viewers.def()
    }
}

impl Related<super::viewer_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::ViewerGroups.def()
    }
}

impl Related<super::user_dataroom::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UserDatarooms.def()
    }
}

impl Related<super::permission_group::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::PermissionGroups.def()
    }
}

impl Related<super::conversation::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Conversations.def()
    }
}

impl Related<super::dataroom_question::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Questions.def()
    }
}

impl Related<super::dataroom_faq_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::FaqItems.def()
    }
}

impl Related<super::chat::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Chats.def()
    }
}

impl Related<super::document_upload::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::UploadedDocuments.def()
    }
}

impl Related<super::tag_item::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tags.def()
    }
}

impl Related<super::task_list::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::TaskLists.def()
    }
}

impl Related<super::task::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Tasks.def()
    }
}

impl Related<super::dataroom_brand::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Brand.def()
    }
}

#[async_trait::async_trait]
impl ActiveModelBehavior for ActiveModel {
    async fn before_save<C>(mut self, _db: &C, insert: bool) -> Result<Self, DbErr>
    where
        C: ConnectionTrait,
    {
        let now = chrono::Utc::now().trunc_subsecs(3);
        if insert && self.created_at.is_not_set() {
            self.created_at = Set(now.naive_utc());
        }
        if !self.updated_at.is_set() {
            self.updated_at = Set(now.naive_utc());
        }
        Ok(self)
    }
}
