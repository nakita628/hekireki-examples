use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "embed_configs")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    #[sea_orm(unique)]
    pub uuid: String,
    #[sea_orm(default_value = false)]
    pub enabled: bool,
    #[sea_orm(default_value = "query")]
    pub chat_mode: String,
    pub allowlist_domains: Option<String>,
    #[sea_orm(default_value = false)]
    pub allow_model_override: bool,
    #[sea_orm(default_value = false)]
    pub allow_temperature_override: bool,
    #[sea_orm(default_value = false)]
    pub allow_prompt_override: bool,
    pub max_chats_per_day: Option<i32>,
    pub max_chats_per_session: Option<i32>,
    #[sea_orm(default_value = 20)]
    pub message_limit: Option<i32>,
    pub workspace_id: i32,
    #[sea_orm(column_name = "createdBy")]
    pub created_by: Option<i32>,
    #[sea_orm(column_name = "usersId")]
    pub users_id: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::workspaces::Entity",
        from = "Column::WorkspaceId",
        to = "super::workspaces::Column::Id",
        on_delete = "Cascade"
    )]
    Workspace,
    #[sea_orm(
        belongs_to = "super::users::Entity",
        from = "Column::UsersId",
        to = "super::users::Column::Id"
    )]
    Users,
    #[sea_orm(has_many = "super::embed_chats::Entity")]
    EmbedChats,
}

impl Related<super::workspaces::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Workspace.def()
    }
}

impl Related<super::users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
    }
}

impl Related<super::embed_chats::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbedChats.def()
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
            self.created_at = Set(now.into());
        }
        Ok(self)
    }
}
