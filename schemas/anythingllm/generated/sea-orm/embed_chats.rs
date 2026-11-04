use super::prisma_date_time::PrismaDateTime;
use chrono::SubsecRound;
use sea_orm::entity::prelude::*;
use sea_orm::Set;
use serde::{Deserialize, Serialize};

#[derive(Clone, Debug, PartialEq, Eq, DeriveEntityModel, Serialize, Deserialize)]
#[sea_orm(table_name = "embed_chats")]
pub struct Model {
    #[sea_orm(primary_key)]
    pub id: i32,
    pub prompt: String,
    pub response: String,
    pub session_id: String,
    #[sea_orm(default_value = true)]
    pub include: bool,
    pub connection_information: Option<String>,
    pub embed_id: i32,
    #[sea_orm(column_name = "usersId")]
    pub users_id: Option<i32>,
    #[sea_orm(column_name = "createdAt")]
    pub created_at: PrismaDateTime,
}

#[derive(Copy, Clone, Debug, EnumIter, DeriveRelation)]
pub enum Relation {
    #[sea_orm(
        belongs_to = "super::embed_configs::Entity",
        from = "Column::EmbedId",
        to = "super::embed_configs::Column::Id",
        on_delete = "Cascade"
    )]
    EmbedConfig,
    #[sea_orm(
        belongs_to = "super::users::Entity",
        from = "Column::UsersId",
        to = "super::users::Column::Id"
    )]
    Users,
}

impl Related<super::embed_configs::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::EmbedConfig.def()
    }
}

impl Related<super::users::Entity> for Entity {
    fn to() -> RelationDef {
        Relation::Users.def()
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
